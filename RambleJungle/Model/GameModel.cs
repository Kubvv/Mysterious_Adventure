using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace RambleJungle.Model
{
    public class GameModel
    {
        private readonly JungleModel jungleModel;
        private readonly WeaponModel weaponModel;
        private readonly DispatcherTimer actionTimer = new();
        private readonly DispatcherTimer walkTimer = new();
        private JungleObject forgottenCity;
        private int forgottenCityBeastCount;
        private readonly MediaPlayer mediaPlayer = new();
        private bool inGame = true;
        private bool canHit = false;
        private readonly List<Point> visitedPoints = new();
        private bool isSuperRamblerMode = false;

        public JungleObject CurrentJungleObject { get; private set; }

        private bool isMagnifyingGlassMode = false;
        public bool IsMagnifyingGlassMode
        {
            get => isMagnifyingGlassMode;
            private set
            {
                isMagnifyingGlassMode = value;
                Rambler.SetVisible(!value);
                MagnifyingGlassModeChanged?.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler? MagnifyingGlassModeChanged;

        private bool isForgottenCityMode = false;
        public bool IsForgottenCityMode
        {
            get => isForgottenCityMode;
            private set
            {
                isForgottenCityMode = value;
                ForgottenCityModeChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler? ForgottenCityModeChanged;

        private bool isBattleMode = false;
        public bool IsBattleMode
        {
            get => isBattleMode;
            private set
            {
                if (isBattleMode != value)
                {
                    isBattleMode = value;
                    BattleModeChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public event EventHandler? BattleModeChanged;

        public Rambler Rambler { get; private set; }

        public GameModel(JungleModel jungleModel, WeaponModel weaponModel)
        {
            Config.Read();
            this.jungleModel = jungleModel;
            this.weaponModel = weaponModel;
            Rambler = new Rambler();
            // can't initialize with null
            CurrentJungleObject = new JungleObject(JungleObjectType.EmptyField);
            forgottenCity = CurrentJungleObject;

            actionTimer.Tick += ActionTimerTick;
            actionTimer.Interval = new TimeSpan(0, 0, 1);

            walkTimer.Tick += WalkTimerTick;
            walkTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        public void PrepareGame()
        {
            weaponModel.CollectWeapon();
            jungleModel.PrepareJungle(weaponModel);
            weaponModel.ResetRandomWeapons();
            Rambler.Reset();
        }

        public void StartGame()
        {
            jungleModel.GenerateJungle();
            // refresh Rambler image in case of config change
            Rambler.ChangeTypeTo(JungleObjectType.Rambler);
            // place rambler on a random empty field in the jungle
            JungleObject? jungleObject = jungleModel.GetRandomJungleObject(JungleObjectType.EmptyField);
            if (jungleObject != null)
            {
                Rambler.SetCoordinates(jungleObject.Coordinates);
            }

            inGame = true;
            isSuperRamblerMode = false;
            isMagnifyingGlassMode = false;
            isForgottenCityMode = false;
        }

        public void MoveRamblerTo(Point point)
        {
            if (!inGame || actionTimer.IsEnabled || (IsBattleMode && !Config.PacifistRambler)) { return; }
            IsBattleMode = false;

            if (IsMagnifyingGlassMode)
            {
                JungleObject? pointedObject = jungleModel.GetJungleObjectAt(point);
                if (pointedObject != null)
                {
                    List<Point> neighbours = JungleModel.FindNeighboursTo(pointedObject.Coordinates, 1).ToList();
                    neighbours.Add(point);
                    jungleModel.SetPointedAt(neighbours);
                    Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
                    IsMagnifyingGlassMode = false;
                }
            }
            else
            {
                JungleObject? jungleObject = jungleModel.GetJungleObjectAt(point);
                if (jungleObject != null)
                {
                    CurrentJungleObject = jungleObject;
                    if (CurrentJungleObject.JungleObjectType != JungleObjectType.DenseJungle)
                    {
                        if (point.X >= Rambler.Coordinates.X - 1 &&
                            point.X <= Rambler.Coordinates.X + 1 &&
                            point.Y >= Rambler.Coordinates.Y - 1 &&
                            point.Y <= Rambler.Coordinates.Y + 1)
                        {
                            if (CurrentJungleObject.JungleObjectType == JungleObjectType.EmptyField ||
                                CurrentJungleObject.Status == Statuses.Visited)
                            {
                                Rambler.SetCoordinates(point);
                                CheckIfGameIsOver();
                            }
                            else
                            {
                                if (Config.Beasts.Contains(CurrentJungleObject.JungleObjectType))
                                {
                                    IsBattleMode = true;
                                }
                                else
                                {
                                    PlaySound(CurrentJungleObject.Name);
                                }
                                CurrentJungleObject.SetStatus(Statuses.Shown);
                                actionTimer.Start();
                            }
                        }
                        else if (CurrentJungleObject.Status.HasFlag(Statuses.Visited))
                        {
                            // walk the rambler to the point over visited cells
                            visitedPoints.Clear();
                            visitedPoints.Add(Rambler.Coordinates);
                            walkTimer.Start();
                        }
                    }
                }
            }
        }

        private void WalkTimerTick(object? sender, EventArgs e)
        {
            walkTimer.Stop();
            JungleObject? nextStepObject = FindNextStepJungleObject(Rambler.Coordinates, CurrentJungleObject.Coordinates);
            if (nextStepObject != null)
            {
                Rambler.SetCoordinates(nextStepObject.Coordinates);
                visitedPoints.Add(nextStepObject.Coordinates);
                bool endOfWalk = Rambler.Coordinates.X == CurrentJungleObject.Coordinates.X &&
                    Rambler.Coordinates.Y == CurrentJungleObject.Coordinates.Y;
                if (!endOfWalk)
                {
                    walkTimer.Start();
                }
            }
        }

        private JungleObject? FindNextStepJungleObject(Point from, Point to)
        {
            JungleObject? result = jungleModel.GetJungleObjectAt(FindNextStep(from, to));
            if (!CanMakeStepTo(result))
            {
                result = null;
                double minDistance = Config.JungleWidth * Config.JungleHeight;
                foreach (Point neighbour in JungleModel.FindNeighboursTo(from, 1))
                {
                    JungleObject? nextStepObject = jungleModel.GetJungleObjectAt(neighbour);
                    double nextStepDistance = Point.Subtract(neighbour, to).Length;
                    if (nextStepDistance < minDistance && CanMakeStepTo(nextStepObject))
                    {
                        result = nextStepObject;
                        minDistance = nextStepDistance;
                    }
                }
            }
            return result;
        }

        private bool CanMakeStepTo(JungleObject? jungleObject)
        {
            return jungleObject != null && jungleObject.Status.HasFlag(Statuses.Visited) && !visitedPoints.Contains(jungleObject.Coordinates);
        }

        private static Point FindNextStep(Point from, Point to)
        {
            int deltaX = 0, deltaY = 0;
            double angle = from.X == to.X ? 90 : Math.Abs(Math.Atan((from.Y - to.Y) / (from.X - to.X)) * (180 / Math.PI));
            if (angle < 67.5)
            {
                deltaX = from.X > to.X ? -1 : 1;
            }
            if (angle > 22.5)
            {
                deltaY = from.Y > to.Y ? -1 : 1;
            }
            return new Point(from.X + deltaX, from.Y + deltaY);
        }

        private void PlaySound(string soundName)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Sounds\{soundName}.wav");
            if (!File.Exists(fileName))
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Sounds\{soundName}.mp3");
            }
            if (File.Exists(fileName))
            {
                mediaPlayer.Stop();
                mediaPlayer.Open(new Uri(fileName));
                mediaPlayer.Play();
            }
        }

        public void HitBeastWith(Weapon weapon)
        {
            if (!inGame || !canHit || weapon == null) return;

            if (weapon.Count != 0 && CurrentJungleObject is Beast beast)
            {
                canHit = false;
                bool canDoubleAttack = weapon.DoubleAttack && weapon.Count > 1;
                PlaySound(weapon.Name);
                beast.ChangeHealth((int)Math.Round(-Config.WeaponStrenght[new Tuple<WeaponType, JungleObjectType>(weapon.WeaponType, beast.JungleObjectType)].RandomValue *
                    Rambler.Strength * (canDoubleAttack ? 2 : 1)));
                weapon.ChangeCount(canDoubleAttack ? -2 : -1);
                weapon.SetDoubleAttack(false);
                actionTimer.Start();
            }
        }

        private void ActionTimerTick(object? sender, EventArgs e)
        {
            actionTimer.Stop();
            if (CurrentJungleObject.JungleObjectType == JungleObjectType.ForgottenCity)
            {
                forgottenCity = CurrentJungleObject;
                CurrentJungleObject = new Beast(Config.Beasts[Config.Random.Next(Config.Beasts.Count)]);
                IsForgottenCityMode = true;
                forgottenCityBeastCount = 2;
            }
            if (CurrentJungleObject is Beast beast)
            {
                if (IsForgottenCityMode && beast.Health <= 0)
                {
                    Rambler.SetStrength(1);
                    forgottenCityBeastCount--;
                    if (forgottenCityBeastCount > 0)
                    {
                        beast = new Beast(Config.Beasts[Config.Random.Next(Config.Beasts.Count)]);
                        CurrentJungleObject = beast;
                        ForgottenCityModeChanged?.Invoke(this, new EventArgs());
                    }
                }
                if (beast.Health > 0)
                {
                    PlaySound(beast.Name);
                    _ = Action();
                    canHit = true;
                }
                else
                {
                    if (IsForgottenCityMode)
                    {
                        CurrentJungleObject = forgottenCity;
                        IsForgottenCityMode = false;
                        Action();
                        Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
                    }
                    else
                    {
                        Rambler.SetCoordinates(beast.Coordinates);
                        Rambler.SetStrength(1);
                        IsBattleMode = false;
                    }
                }
            }
            else
            {
                if (CurrentJungleObject.JungleObjectType == JungleObjectType.MagnifyingGlass)
                {
                    CurrentJungleObject.SetStatus(Statuses.Visited);
                    IsMagnifyingGlassMode = true;
                }
                else
                {
                    Point ramblerTarget = Action();
                    if (CurrentJungleObject.JungleObjectType != JungleObjectType.Camp && Rambler.Health > 0)
                    {
                        Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
                        if (!ramblerTarget.Equals(CurrentJungleObject.Coordinates))
                        {
                            Rambler.SetCoordinates(ramblerTarget);
                        }
                    }
                }
            }

            CheckIfGameIsOver();
        }

        private void CheckIfGameIsOver()
        {
            bool gameOver = false, gameSuccess = true;
            if (Rambler.Health <= 0)
            {
                gameOver = true;
                gameSuccess = false;
                CurrentJungleObject.SetStatus(Statuses.Visited);
            }
            else if (jungleModel.CountOf(JungleObjectType.Treasure) == 0)
            {
                if (!Config.SuperRambler)
                {
                    gameOver = true;
                    jungleModel.MarkHiddenObjects();
                    inGame = false;
                }
                else if (!isSuperRamblerMode)
                {
                    jungleModel.PointHiddenGoodItems();
                    isSuperRamblerMode = true;
                }
                else
                {
                    gameOver = jungleModel.CountOf(JungleObjectType.EmptyField) == 0;
                }
            }
            if (gameOver)
            {
                PlaySound(gameSuccess ? "Success" : "Fail");
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
        }

        public Point Action()
        {
            Point result = CurrentJungleObject.Coordinates;

            if (Config.Beasts.Contains(CurrentJungleObject.JungleObjectType))
            {
                // Hit rambler
                Rambler.ChangeHealth(-Config.BeastStrenght[CurrentJungleObject.JungleObjectType].RandomValue);
            }
            else if (Config.Arsenals.Contains(CurrentJungleObject.JungleObjectType))
            {
                // Give weapon from arsenal and 25-35% health
                if (CurrentJungleObject is JungleArsenal jungleArsenal)
                {
                    foreach (Weapon weapon in jungleArsenal.Weapons)
                    {
                        weapon.ChangeCount(1);
                    }
                    int healthAdded = Config.Random.Next(6) + 35;
                    Rambler.ChangeHealth(healthAdded);
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Elixir)
            {
                // Increase health by 25%-35%
                int healthAdded = Config.Random.Next(11) + 25;
                Rambler.ChangeHealth(healthAdded);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Radar)
            {
                // Point all monsters in a 5x5 square around the radar
                Point point = new(CurrentJungleObject.Coordinates.X - 2, CurrentJungleObject.Coordinates.Y - 2);
                for (int column = 0; column < 5; column++)
                {
                    for (int row = 0; row < 5; row++)
                    {
                        point.X = CurrentJungleObject.Coordinates.X + column - 2;
                        point.Y = CurrentJungleObject.Coordinates.Y + row - 2;
                        jungleModel.SetPointedAt(point, true);
                    }
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Map)
            {
                // point nearest treasure
                jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Treasure, Statuses.NotVisited)?.SetStatus(Statuses.Pointed);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Talisman)
            {
                // mark nearest hydra
                jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Hydra, Statuses.NotVisited)?.SetStatus(Statuses.Marked);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.LostWeapon)
            {
                // add found weapon
                if (CurrentJungleObject.BackingObject is Weapon weapon)
                {
                    weapon.ChangeCount(1);
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Camp)
            {
                // add all weapons and 25-35% health
                weaponModel.ChangeAllWeaponsCount(1);
                int healthAdded = Config.Random.Next(6) + 40;
                Rambler.ChangeHealth(healthAdded);
                // choose one option:
                //  30% more strength in next battle
                //  check fields adjacent to camp
                //  additional 15% health
                //  double attack with random weapon
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.ForgottenCity)
            {
                // after fight with two random beasts, one after another
                // give random weapon and two treasures pointed as a reward

                // add random weapon
                weaponModel.ChangeRandomWeaponCount(1);

                // point first treasure
                jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Treasure, Statuses.Hidden)?.SetStatus(Statuses.Pointed);
                // point second treasure
                jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Treasure, Statuses.Hidden)?.SetStatus(Statuses.Pointed);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Natives)
            {
                // if rambler has some treasure
                // return one treasure to random empty field in the jungle
                if (jungleModel.CountOf(JungleObjectType.Treasure) < Config.JungleObjectsCount[JungleObjectType.Treasure])
                {
                    JungleObject? emptyField = jungleModel.GetRandomJungleObject(JungleObjectType.EmptyField);
                    emptyField?.ChangeTypeTo(JungleObjectType.Treasure);
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Trap)
            {
                // decrease health by 25%-35%
                int healthSubtracted = Config.Random.Next(11) + 25;
                Rambler.ChangeHealth(-healthSubtracted);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Quicksand)
            {
                // jump to random hidden empty field
                JungleObject? emptyField = jungleModel.GetRandomJungleObject(JungleObjectType.EmptyField);
                if (emptyField != null)
                {
                    result = emptyField.Coordinates;
                }
                // decrease health by 10
                Rambler.ChangeHealth(-10);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Treasure)
            {
                // do nothing
            }
            return result;
        }

        internal void CampBonus(CampBonus bonus)
        {
            switch (bonus)
            {
                case Model.CampBonus.Strenght:
                    Rambler.SetStrength(1.3);
                    break;
                case Model.CampBonus.Health:
                    Rambler.ChangeHealth(15);
                    break;
                case Model.CampBonus.Adjacency:
                    jungleModel.SetPointedAt(JungleModel.FindNeighboursTo(CurrentJungleObject.Coordinates, 1).ToList());
                    break;
                case Model.CampBonus.DoubleAttack:
                    weaponModel.SetDoubleAttack();
                    break;
            }
            Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace RumbleJungle.Model
{
    public class GameModel
    {
        private readonly JungleModel jungleModel;
        private readonly WeaponModel weaponModel;
        private readonly DispatcherTimer actionTimer = new DispatcherTimer();
        private JungleObject forgottenCity = null;
        private int forgottenCityBeastCount;
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private bool inGame = true;
        private bool canHit = false;

        public JungleObject CurrentJungleObject { get; private set; }

        private bool isMagnifyingGlassMode = false;
        public bool IsMagnifyingGlassMode
        {
            get => isMagnifyingGlassMode;
            private set
            {
                isMagnifyingGlassMode = value;
                Rambler.SetVisible(!value);
                MagnifyingGlassModeChanged?.Invoke(this, null);
            }
        }
        public event EventHandler MagnifyingGlassModeChanged;

        private bool isForgottenCityMode = false;
        public bool IsForgottenCityMode
        {
            get => isForgottenCityMode;
            private set
            {
                isForgottenCityMode = value;
                ForgottenCityModeChanged?.Invoke(this, null);
            }
        }

        public event EventHandler ForgottenCityModeChanged;

        public Rambler Rambler { get; private set; } = null;

        public GameModel(JungleModel jungleModel, WeaponModel weaponModel)
        {
            this.jungleModel = jungleModel;
            this.weaponModel = weaponModel;
            Rambler = new Rambler();

            actionTimer.Tick += ActionTimerTick;
            actionTimer.Interval = new TimeSpan(0, 0, 1);
        }

        public void StartGame()
        {
            weaponModel.CollectWeapon();
            jungleModel.GenerateJungle();
            Rambler.Reset();
            // place rambler on a random empty field in the jungle
            Rambler.SetCoordinates(jungleModel.GetRandomJungleObject(JungleObjectType.EmptyField).Coordinates);
            
            inGame = true;
            isMagnifyingGlassMode = false;
            isForgottenCityMode = false;
        }

        public void MoveRamblerTo(Point point)
        {
            if (!inGame) return;

            if (IsMagnifyingGlassMode)
            {
                JungleObject pointedObject = jungleModel.GetJungleObjectAt(point);
                List<Point> pointNeighbours = JungleModel.FindNeighboursTo(pointedObject.Coordinates, 1).ToList();
                jungleModel.SetPointedAt(pointNeighbours);
                Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
                IsMagnifyingGlassMode = false;
            }
            else
            {
                CurrentJungleObject = jungleModel.GetJungleObjectAt(point);
                if (point.X >= Rambler.Coordinates.X - 1 && point.X <= Rambler.Coordinates.X + 1 && point.Y >= Rambler.Coordinates.Y - 1 && point.Y <= Rambler.Coordinates.Y + 1
                    && CurrentJungleObject.JungleObjectType != JungleObjectType.DenseJungle)
                {
                    if (CurrentJungleObject.JungleObjectType == JungleObjectType.EmptyField ||
                        CurrentJungleObject.Status == Statuses.Visited)
                    {
                        Rambler.SetCoordinates(point);
                    }
                    else
                    {
                        if (!Config.Beasts.Contains(CurrentJungleObject.JungleObjectType))
                        {
                            PlaySound(CurrentJungleObject.Name);
                        }
                        CurrentJungleObject.SetStatus(Statuses.Shown);
                        actionTimer.Start();
                    }
                }
            }
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

        private void ActionTimerTick(object sender, EventArgs e)
        {
            actionTimer.Stop();
            if (CurrentJungleObject.JungleObjectType == JungleObjectType.ForgottenCity)
            {
                forgottenCity = CurrentJungleObject;
                CurrentJungleObject = new Beast(Config.Beasts[Config.Random.Next(Config.Beasts.Count)]);
                IsForgottenCityMode = true;
                forgottenCityBeastCount = 2;
            }
            if (CurrentJungleObject is Beast)
            {
                Beast beast = CurrentJungleObject as Beast;
                if (IsForgottenCityMode && beast.Health <= 0)
                {
                    Rambler.SetStrength(1);
                    forgottenCityBeastCount--;
                    if (forgottenCityBeastCount > 0)
                    {
                        CurrentJungleObject = new Beast(Config.Beasts[Config.Random.Next(Config.Beasts.Count)]);
                        beast = CurrentJungleObject as Beast;
                        ForgottenCityModeChanged?.Invoke(this, null);
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
            if (Rambler.Health <= 0)
            {
                // game over (fail)
                PlaySound("Fail");
                CurrentJungleObject.SetStatus(Statuses.Visited);
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
            else if (jungleModel.CountOf(JungleObjectType.Treasure) == 0)
            {
                // game over (success)
                PlaySound("Success");
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
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Elixir)
            {
                // Increase health by 25%-35%
                int healthAdded = Config.Random.Next(11) + 25;
                Rambler.ChangeHealth(healthAdded);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Compass)
            {
                // Point all monsters in a 5x5 square around the compass
                Point point = new Point(CurrentJungleObject.Coordinates.X - 2, CurrentJungleObject.Coordinates.Y - 2);
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
                JungleObject treasure = jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Treasure, Statuses.NotVisited);
                if (treasure != null)
                {
                    treasure.SetStatus(Statuses.Pointed);
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Talisman)
            {
                // mark nearest hydra
                JungleObject hydra = jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Hydra, Statuses.NotVisited);
                if (hydra != null)
                {
                    hydra.SetStatus(Statuses.Marked);
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.LostWeapon)
            {
                // add random weapon
                weaponModel.ChangeRandomWeaponCount(1);
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Camp)
            {
                // add all weapons and 25-35% health
                weaponModel.ChangeAllWeaponsCount(1);
                int healthAdded = Config.Random.Next(6) + 40;
                Rambler.ChangeHealth(healthAdded);
                // choose one option:
                //  30% more strength in next battle
                //  check four fields adjacent to camp
                //  additional 15% health
                //  double attack with random weapon
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Tent)
            {
                // add all weapons and 25-35% health
                weaponModel.ChangeAllWeaponsCount(1);
                int healthAdded = Config.Random.Next(6) + 35;
                Rambler.ChangeHealth(healthAdded);

            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.ForgottenCity)
            {
                // after fight with two random beasts, one after another
                // give random weapon and two treasures pointed as a reward

                // add random weapon
                weaponModel.ChangeRandomWeaponCount(1);

                // point first treasure
                JungleObject treasure = jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Treasure, Statuses.Hidden);
                if (treasure != null)
                {
                    treasure.SetStatus(Statuses.Pointed);
                    // point second treasure
                    treasure = jungleModel.FindNearestTo(CurrentJungleObject.Coordinates, JungleObjectType.Treasure, Statuses.Hidden);
                    if (treasure != null)
                    {
                        treasure.SetStatus(Statuses.Pointed);
                    }
                }
            }
            else if (CurrentJungleObject.JungleObjectType == JungleObjectType.Natives)
            {
                // if rambler has some treasure
                // return one treasure to random empty field in the jungle
                if (jungleModel.CountOf(JungleObjectType.Treasure) < Config.JungleObjectsCount[JungleObjectType.Treasure])
                {
                    JungleObject emptyField = jungleModel.GetRandomJungleObject(JungleObjectType.EmptyField);
                    if (emptyField != null)
                    {
                        emptyField.ChangeTypeTo(JungleObjectType.Treasure);
                    }
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
                JungleObject emptyField = jungleModel.GetRandomJungleObject(JungleObjectType.EmptyField);
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
                    CheckAdjacent(CurrentJungleObject);
                    break;
                case Model.CampBonus.DoubleAttack:
                    weaponModel.SetDoubleAttack();
                    break;
            }
            Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
        }

        private void CheckAdjacent(JungleObject jungleObject)
        {
            jungleModel.SetPointedAt(new Point(jungleObject.Coordinates.X - 1, jungleObject.Coordinates.Y), false);
            jungleModel.SetPointedAt(new Point(jungleObject.Coordinates.X + 1, jungleObject.Coordinates.Y), false);
            jungleModel.SetPointedAt(new Point(jungleObject.Coordinates.X, jungleObject.Coordinates.Y - 1), false);
            jungleModel.SetPointedAt(new Point(jungleObject.Coordinates.X, jungleObject.Coordinates.Y + 1), false);
        }
    }
}
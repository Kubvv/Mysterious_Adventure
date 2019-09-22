using CommonServiceLocator;
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
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly WeaponModel weaponModel = ServiceLocator.Current.GetInstance<WeaponModel>();

        private readonly DispatcherTimer actionTimer = new DispatcherTimer();
        private JungleObject jungleObject = null;
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private bool inGame = true;
        private bool canHit = false;

        private bool isMagnifyingGlassMode = false;
        public bool IsMagnifyingGlassMode
        {
            get => isMagnifyingGlassMode;
            private set
            {
                isMagnifyingGlassMode = value;
                Rambler.SetVisible(!value);
                ModeChanged?.Invoke(this, null);
            }
        }

        public event EventHandler ModeChanged;

        public Rambler Rambler { get; private set; } = null;

        public GameModel()
        {
            actionTimer.Tick += ActionTimerTick;
            actionTimer.Interval = new TimeSpan(0, 0, 1);
        }

        public void StartGame()
        {
            Rambler = new Rambler();
            jungleModel.GenerateJungle();
            weaponModel.CollectWeapon();
            jungleModel.ReleaseRambler(Rambler);
            inGame = true;
        }

        public void MoveRamblerTo(Point point)
        {
            if (!inGame) return;

            if (IsMagnifyingGlassMode)
            {
                JungleObject pointedObject = jungleModel.GetJungleObjectAt(point);
                List<Point> pointNeighbours = jungleModel.FindNeighboursTo(pointedObject.Coordinates, 1).ToList();
                jungleModel.SetPointedAt(pointNeighbours);
                Rambler.SetCoordinates(jungleObject.Coordinates);
                IsMagnifyingGlassMode = false;
            }
            else
            {
                jungleObject = jungleModel.GetJungleObjectAt(point);
                if (point.X >= Rambler.Coordinates.X - 1 && point.X <= Rambler.Coordinates.X + 1 && point.Y >= Rambler.Coordinates.Y - 1 && point.Y <= Rambler.Coordinates.Y + 1
                    && jungleObject.JungleObjectType != JungleObjectTypes.DenseJungle)
                {
                    if (jungleObject.JungleObjectType == JungleObjectTypes.EmptyField ||
                        jungleObject.Status == Statuses.Visited)
                    {
                        Rambler.SetCoordinates(point);
                    }
                    else
                    {
                        jungleObject.SetStatus(Statuses.Shown);
                        PlaySound(jungleObject.Name);
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
            if (!inGame || !canHit) return;

            if (weapon.Count != 0 && jungleObject is Beast beast)
            {
                canHit = false;
                bool canDoubleAttack = weapon.DoubleAttack && weapon.Count > 1;
                PlaySound(weapon.Name);
                beast.ChangeHealth((int)Math.Round(-Configuration.WeaponStrenght[new Tuple<WeaponTypes, JungleObjectTypes>(weapon.WeaponType, beast.JungleObjectType)].RandomValue * 
                    Rambler.Strength * (canDoubleAttack ? 2 : 1)));
                weapon.ChangeCount(canDoubleAttack ? -2 : -1);
                weapon.SetDoubleAttack(false);
                actionTimer.Start();
            }
        }

        private void ActionTimerTick(object sender, EventArgs e)
        {
            actionTimer.Stop();
            Beast beast = jungleObject as Beast;
            if (beast != null)
            {
                if (beast.Health > 0)
                {
                    PlaySound(beast.Name);
                    beast.Action();
                    canHit = true;
                }
                else
                {
                    Rambler.SetCoordinates(beast.Coordinates);
                    Rambler.SetStrength(1);
                }
            }
            else
            {
                if (jungleObject.JungleObjectType == JungleObjectTypes.MagnifyingGlass)
                {
                    jungleObject.SetStatus(Statuses.Visited);
                    IsMagnifyingGlassMode = true;
                }
                else
                {
                    Point ramblerTarget = jungleObject.Action();
                    if (jungleObject.JungleObjectType != JungleObjectTypes.Camp && Rambler.Health > 0)
                    {
                        Rambler.SetCoordinates(jungleObject.Coordinates);
                        if (!ramblerTarget.Equals(jungleObject.Coordinates))
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
                jungleObject.SetStatus(Statuses.Visited);
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
            else if (jungleModel.CountOf(JungleObjectTypes.Treasure) == 0)
            {
                // game over (success)
                PlaySound("Sucess");
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
        }
    }
}
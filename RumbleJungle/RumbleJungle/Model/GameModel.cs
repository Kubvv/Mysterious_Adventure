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
                Rambler.SetCoordinates(CurrentJungleObject.Coordinates);
                IsMagnifyingGlassMode = false;
            }
            else
            {
                CurrentJungleObject = jungleModel.GetJungleObjectAt(point);
                if (point.X >= Rambler.Coordinates.X - 1 && point.X <= Rambler.Coordinates.X + 1 && point.Y >= Rambler.Coordinates.Y - 1 && point.Y <= Rambler.Coordinates.Y + 1
                    && CurrentJungleObject.JungleObjectType != JungleObjectTypes.DenseJungle)
                {
                    if (CurrentJungleObject.JungleObjectType == JungleObjectTypes.EmptyField ||
                        CurrentJungleObject.Status == Statuses.Visited)
                    {
                        Rambler.SetCoordinates(point);
                    }
                    else
                    {
                        if (!Configuration.Beasts.Contains(CurrentJungleObject.JungleObjectType))
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
            if (!inGame || !canHit) return;

            if (weapon.Count != 0 && CurrentJungleObject is Beast beast)
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
            if (CurrentJungleObject.JungleObjectType == JungleObjectTypes.ForgottenCity)
            {
                forgottenCity = CurrentJungleObject;
                CurrentJungleObject = new Beast(Configuration.Beasts[Configuration.Random.Next(Configuration.Beasts.Count)]);
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
                        CurrentJungleObject = new Beast(Configuration.Beasts[Configuration.Random.Next(Configuration.Beasts.Count)]);
                        beast = CurrentJungleObject as Beast;
                        ForgottenCityModeChanged?.Invoke(this, null);
                    }
                }
                if (beast.Health > 0)
                {
                    PlaySound(beast.Name);
                    _ = beast.Action();
                    canHit = true;
                }
                else
                {
                    if (IsForgottenCityMode)
                    {
                        CurrentJungleObject = forgottenCity;
                        IsForgottenCityMode = false;
                        CurrentJungleObject.Action();
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
                if (CurrentJungleObject.JungleObjectType == JungleObjectTypes.MagnifyingGlass)
                {
                    CurrentJungleObject.SetStatus(Statuses.Visited);
                    IsMagnifyingGlassMode = true;
                }
                else
                {
                    Point ramblerTarget = CurrentJungleObject.Action();
                    if (CurrentJungleObject.JungleObjectType != JungleObjectTypes.Camp && Rambler.Health > 0)
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
            else if (jungleModel.CountOf(JungleObjectTypes.Treasure) == 0)
            {
                // game over (success)
                PlaySound("Success");
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
        }
    }
}
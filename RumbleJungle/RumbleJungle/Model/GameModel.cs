using CommonServiceLocator;
using System;
using System.Windows;
using System.Windows.Threading;

namespace RumbleJungle.Model
{
    public class GameModel
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly WeaponModel weaponModel = ServiceLocator.Current.GetInstance<WeaponModel>();

        private readonly DispatcherTimer actionTimer = new DispatcherTimer();
        private JungleObject jungleObject = null;

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
        }

        public void MoveRamblerTo(Point point)
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
                    actionTimer.Start();
                }
            }
        }

        public void HitBeastWith(Weapon weapon)
        {
            if (weapon.Count != 0 && jungleObject is Beast beast)
            {
                // TODO: weapon on beast strength configuration
                int healthSubtracted = Configuration.Random.Next(11) + 25;
                beast.ChangeHealth(-healthSubtracted);
                weapon.ChangeCount(-1);
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
                    beast.Action();
                }
                else
                {
                    Rambler.SetCoordinates(beast.Coordinates);
                }
            }
            else
            {
                Point ramblerTarget = jungleObject.Action();
                if (Rambler.Health > 0)
                { 
                    Rambler.SetCoordinates(jungleObject.Coordinates);
                    if (!ramblerTarget.Equals(jungleObject.Coordinates))
                    {
                        Rambler.SetCoordinates(ramblerTarget);
                    }
                }
            }
            if (Rambler.Health <= 0)
            {
                // TODO: game over (fail)
                jungleObject.SetStatus(Statuses.Visited);
                jungleModel.MarkHiddenObjects();
            }
            else if (jungleModel.CountOf(JungleObjectTypes.Treasure) == 0)
            {
                // TODO: game over (success)
                jungleModel.MarkHiddenObjects();
            }
        }
    }
}
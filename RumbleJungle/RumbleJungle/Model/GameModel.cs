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

        private DispatcherTimer actionTimer = new DispatcherTimer();
        private JungleObject jungleObject;
               
        public Rambler Rambler { get; private set; } = new Rambler();

        public GameModel()
        {
            actionTimer.Tick += ActionTimerTick;
            actionTimer.Interval = new TimeSpan(0, 0, 1);
        }

        public void StartGame()
        {
            jungleModel.GenerateJungle();
            Rambler.Reset();
            weaponModel.CollectWeapon();
            jungleModel.ReleaseRambler(Rambler);
        }

        public void MoveRambler(Point point)
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

        private void ActionTimerTick(object sender, EventArgs e)
        {
            actionTimer.Stop();
            Point ramblerTarget = jungleObject.Action();
            if (Rambler.Health > 0)
            {
                Rambler.SetCoordinates(ramblerTarget);
            }
        }
    }
}

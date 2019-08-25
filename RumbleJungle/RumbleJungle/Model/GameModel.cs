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
        private bool inGame = true;
        private int hitCount = 0;

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
            if (!inGame) return;
            if (hitCount <= 0) return;

            if (weapon.Count != 0 && jungleObject is Beast beast)
            {
                hitCount--;
                beast.ChangeHealth((int)Math.Round(-Configuration.WeaponStrenght[new Tuple<WeaponTypes, JungleObjectTypes>(weapon.WeaponType, beast.JungleObjectType)].RandomValue * 
                    Rambler.Strength));
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
                    hitCount = 1;
                }
                else
                {
                    Rambler.SetCoordinates(beast.Coordinates);
                    Rambler.SetStrength(1);
                }
            }
            else if (jungleObject.JungleObjectType == JungleObjectTypes.Camp)
            {
                Point ramblerTarget = jungleObject.Action();
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
                // game over (fail)
                jungleObject.SetStatus(Statuses.Visited);
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
            else if (jungleModel.CountOf(JungleObjectTypes.Treasure) == 0)
            {
                // game over (success)
                jungleModel.MarkHiddenObjects();
                inGame = false;
            }
        }
    }
}
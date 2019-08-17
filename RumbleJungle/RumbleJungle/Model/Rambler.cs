using CommonServiceLocator;
using System;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Rambler : JungleObject
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        public int Health { get; private set; }

        public Rambler() : base(JungleObjectTypes.Rambler)
        {
            Health = Configuration.DebugMode ? 50 : 100;
        }

        public event EventHandler Moved;
        public event EventHandler HealthChanged;

        public override void SetCoordinates(Point point)
        {
            base.SetCoordinates(point);
            Moved?.Invoke(this, null);
            JungleObject jungleObject = jungleModel.GetJungleObjectAt(point);
            jungleObject.SetStatus(Statuses.Visited);
        }

        internal void ChangeHealth(int healthChange)
        {
            Health += healthChange;
            if (Health > 100)
            {
                Health = 100;
            }
            else if (Health < 0)
            {
                Health = 0;
            }
            HealthChanged?.Invoke(this, null);
        }
    }
}

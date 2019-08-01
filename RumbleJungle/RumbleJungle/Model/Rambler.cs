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
        }

        public void Reset()
        {
            Health = 100;
        }

        public event EventHandler Moved;

        public override void SetCoordinates(Point point)
        {
            base.SetCoordinates(point);
            Moved?.Invoke(this, null);
            JungleObject jungleObject = jungleModel.GetJungleObjectAt(point);
            jungleObject.SetStatus(Statuses.Visited);
        }
    }
}

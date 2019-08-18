using CommonServiceLocator;
using System;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Rambler : LivingJungleObject
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        public Rambler() : base(JungleObjectTypes.Rambler)
        {
            ChangeHealth(Configuration.DebugMode ? 50 : 100);
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

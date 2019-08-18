using CommonServiceLocator;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Beast : LivingJungleObject
    {
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        public Beast(JungleObjectTypes beastType) : base(beastType)
        {
            ChangeHealth(66);
        }

        public new Point Action()
        {
            // battle

            return Coordinates;
        }
    }
}
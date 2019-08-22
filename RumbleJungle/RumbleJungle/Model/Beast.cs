using CommonServiceLocator;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Beast : LivingJungleObject
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();
        
        public Beast(JungleObjectTypes beastType) : base(beastType)
        {
            ChangeHealth(Configuration.BeastsInitialHealth[beastType] + Configuration.Random.Next(11) - 5);
        }

        public new Point Action()
        {
            // hit rambler
            // TODO: beasts strength configuration
            int healthSubtracted = Configuration.Random.Next(11) + 4;
            gameModel.Rambler.ChangeHealth(-healthSubtracted);

            return Coordinates;
        }
    }
}
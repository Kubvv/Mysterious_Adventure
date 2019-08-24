using CommonServiceLocator;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Beast : LivingJungleObject
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        
        public Beast(JungleObjectTypes beastType) : base(beastType)
        {
            ChangeHealth(Configuration.BeastsInitialHealth[beastType].RandomValue);
        }

        public new Point Action()
        {
            // hit rambler
            gameModel.Rambler.ChangeHealth(-Configuration.BeastStrenght[JungleObjectType].RandomValue);

            return Coordinates;
        }
    }
}
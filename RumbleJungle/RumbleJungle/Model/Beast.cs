namespace RumbleJungle.Model
{
    public class Beast : LivingJungleObject
    {
        public Beast(JungleObjectTypes beastType) : base(beastType)
        {
            ChangeHealth(Configuration.BeastsInitialHealth[beastType].RandomValue);
        }
    }
}
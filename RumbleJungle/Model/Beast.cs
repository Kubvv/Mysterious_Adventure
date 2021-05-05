namespace RambleJungle.Model
{
    public class Beast : LivingJungleObject
    {
        private readonly JungleObjectType beastType;

        public Beast(JungleObjectType beastType) : base(beastType)
        {
            this.beastType = beastType;
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            SetHealth(Config.BeastsInitialHealth[beastType].RandomValue);
        }
    }
}
using CommonServiceLocator;

namespace RumbleJungle.Model
{
    public class Beast : JungleObject
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();

        public int Count { get; private set; }

        public Beast(JungleObjectTypes beastType) : base(beastType)
        {
            Count = jungleManager.CountOf(beastType);
        }
    }
}

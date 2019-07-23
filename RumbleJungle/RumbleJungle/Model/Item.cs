using CommonServiceLocator;

namespace RumbleJungle.Model
{
    public class Item : JungleObject
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();

        public int Count { get; private set; }

        public Item(JungleObjectTypes itemType) : base(itemType)
        {
            Count = jungleManager.CountOf(itemType);
        }
    }
}

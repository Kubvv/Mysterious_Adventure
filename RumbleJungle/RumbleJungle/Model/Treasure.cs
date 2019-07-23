using CommonServiceLocator;

namespace RumbleJungle.Model
{
    public class Treasure : JungleObject
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();

        public int Found { get; private set; }
        public int Count { get; private set; }

        public Treasure() : base(JungleObjectTypes.Treasure)
        {
        }

        public void Reset()
        {
            Found = 0;
            Count = jungleManager.CountOf(JungleObjectTypes.Treasure);
        }
    }
}

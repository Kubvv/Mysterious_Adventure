using CommonServiceLocator;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class BeastViewModel : JungleObjectViewModel
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();
        private JungleObject beast;

        public int Count => jungleManager.CountOf(beast.JungleObjectType);

        public BeastViewModel(JungleObject beast) : base(beast)
        {
        }
    }
}

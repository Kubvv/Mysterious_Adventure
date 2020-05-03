using CommonServiceLocator;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class ItemsViewModel : JungleObjectViewModel
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();
        private JungleObject item;

        public int Count => jungleManager.CountOf(item.JungleObjectType);

        public ItemsViewModel(JungleObject item) : base(item)
        {
        }
    }
}

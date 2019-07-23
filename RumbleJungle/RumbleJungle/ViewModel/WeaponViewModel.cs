using GalaSoft.MvvmLight;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class WeaponViewModel : ViewModelBase
    {
        private Weapon weapon;

        public string Name => weapon.Name;
        public string Shape => $"/RumbleJungle;component/Images/{weapon.Shape}.svg";
        public int Count => weapon.Count;

        public WeaponViewModel(Weapon weapon)
        {
            this.weapon = weapon;
        }
    }
}

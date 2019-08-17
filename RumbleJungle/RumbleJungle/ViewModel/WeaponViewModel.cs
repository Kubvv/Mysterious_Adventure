using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class WeaponViewModel : ViewModelBase
    {
        private readonly Weapon weapon;

        public string Name => weapon.Name;
        public string Shape => $"/RumbleJungle;component/Images/{weapon.Name}.svg";
        public int Count => weapon.Count;

        public WeaponViewModel(Weapon weapon)
        {
            this.weapon = weapon;
            weapon.CountChanged += CountChanged;
        }

        private void CountChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Count");
        }
    }
}

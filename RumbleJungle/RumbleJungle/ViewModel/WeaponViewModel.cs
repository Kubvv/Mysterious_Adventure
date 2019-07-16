using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class WeaponViewModel : ViewModelBase
    {
        private WeaponTypes weaponType;

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public string Shape => $"/RumbleJungle;component/Images/{Configuration.ShapeOf(weaponType)}.svg";

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        public WeaponViewModel(WeaponTypes weaponType)
        {
            this.weaponType = weaponType;
            Name = Enum.GetName(typeof(WeaponTypes), weaponType);
            Quantity = weaponType == WeaponTypes.Dagger ? -1 : 1;
        }
    }
}

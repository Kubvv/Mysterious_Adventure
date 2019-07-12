using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumbleJungle.ViewModel
{
    public class WeaponViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();
        private WeaponTypes weaponType;

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public string Shape => Configuration.ShapeOf(weaponType);

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

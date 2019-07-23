using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        private readonly WeaponManager weaponManager = ServiceLocator.Current.GetInstance<WeaponManager>();
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();

        public RamblerViewModel Rambler { get; } = ServiceLocator.Current.GetInstance<RamblerViewModel>();
        public TreasureViewModel Treasure { get; } = ServiceLocator.Current.GetInstance<TreasureViewModel>();
        public ObservableCollection<WeaponViewModel> Weapons { get; set; } = new ObservableCollection<WeaponViewModel>();
        public ObservableCollection<JungleObjectViewModel> Beasts { get; set; } = new ObservableCollection<JungleObjectViewModel>();
        public ObservableCollection<JungleObjectViewModel> Items { get; set; } = new ObservableCollection<JungleObjectViewModel>();

        public StatusBarViewModel()
        {
            foreach (Weapon weapon in weaponManager.Weapons)
            {
                Weapons.Add(new WeaponViewModel(weapon));
            }
            foreach (JungleObject beast in jungleManager.GetJungleObjects(Configuration.BEAST))
            {
                Beasts.Add(new JungleObjectViewModel(beast));
            }
            foreach (JungleObject item in jungleManager.GetJungleObjects(Configuration.ITEM))
            {
                Items.Add(new JungleObjectViewModel(item));
            }
        }
    }
}

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        private readonly WeaponModel weaponModel = ServiceLocator.Current.GetInstance<WeaponModel>();
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        public RamblerViewModel Rambler { get; } = ServiceLocator.Current.GetInstance<RamblerViewModel>();
        public TreasureStatusViewModel Treasure { get; } = ServiceLocator.Current.GetInstance<TreasureStatusViewModel>();
        public ObservableCollection<WeaponViewModel> Weapons { get; private set; } = new ObservableCollection<WeaponViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Beasts { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Items { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();

        public StatusBarViewModel()
        {
            foreach (Weapon weapon in weaponModel.Weapons)
            {
                Weapons.Add(new WeaponViewModel(weapon));
            }
            foreach (JungleObject beast in jungleModel.GetJungleObjects(Configuration.Beasts))
            {
                Beasts.Add(new JungleObjectStatusViewModel(beast));
            }
            foreach (JungleObject item in jungleModel.GetJungleObjects(Configuration.HiddenItems))
            {
                Items.Add(new JungleObjectStatusViewModel(item));
            }
        }
    }
}

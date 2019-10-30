using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        public RamblerViewModel Rambler { get; private set; }
        public TreasureViewModel Treasure { get; private set; }
        public ObservableCollection<WeaponViewModel> Weapons { get; private set; } = new ObservableCollection<WeaponViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Beasts { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Items { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();

        public StatusBarViewModel(JungleModel jungleModel, WeaponModel weaponModel, RamblerViewModel ramblerViewModel, TreasureViewModel treasureViewModel)
        {
            Rambler = ramblerViewModel;
            Treasure = treasureViewModel;

            foreach (Weapon weapon in weaponModel.Weapons)
            {
                Weapons.Add(new WeaponViewModel(weapon));
            }
            foreach (JungleObject beast in jungleModel.GetJungleObjects(Configuration.Beasts))
            {
                Beasts.Add(new JungleObjectStatusViewModel(beast));
            }
            foreach (JungleObject item in jungleModel.GetJungleObjects(Configuration.VisibleItems))
            {
                Items.Add(new JungleObjectStatusViewModel(item));
            }
            foreach (JungleObject item in jungleModel.GetJungleObjects(Configuration.HiddenItems))
            {
                Items.Add(new JungleObjectStatusViewModel(item));
            }
        }
    }
}

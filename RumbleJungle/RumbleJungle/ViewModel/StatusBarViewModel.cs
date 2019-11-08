using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

            if (weaponModel != null)
            {
                foreach (Weapon weapon in weaponModel.Weapons)
                {
                    Weapons.Add(new WeaponViewModel(weapon));
                }
            }

            if (jungleModel != null)
            {
                foreach (JungleObject beast in jungleModel.GetJungleObjects(Config.Beasts))
                {
                    Beasts.Add(new JungleObjectStatusViewModel(beast));
                }
                foreach (JungleObject item in jungleModel.GetJungleObjects(Config.VisibleItems))
                {
                    Items.Add(new JungleObjectStatusViewModel(item));
                }
                foreach (JungleObject item in jungleModel.GetJungleObjects(Config.HiddenItems))
                {
                    Items.Add(new JungleObjectStatusViewModel(item));
                }
            }
        }

        private RelayCommand saveGame;
        public RelayCommand SaveGame => saveGame ?? (saveGame = new RelayCommand(() => ExecuteSaveGame(), () => CanSaveGame));

        private static void ExecuteSaveGame()
        {
            // TODO: save game
        }

        private bool canSaveGame = true;
        public bool CanSaveGame
        {
            get => canSaveGame;
            set
            {
                canSaveGame = value;
                SaveGame.RaiseCanExecuteChanged();
            }
        }
    }
}

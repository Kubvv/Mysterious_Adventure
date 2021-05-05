using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RambleJungle.Model;
using System;
using System.Collections.ObjectModel;

namespace RambleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel;

        public RamblerViewModel Rambler { get; private set; }
        public double ExplorationProgress => jungleModel.ExplorationProgress();
        public TreasureViewModel Treasure { get; private set; }
        public ObservableCollection<WeaponViewModel> Weapons { get; private set; } = new ObservableCollection<WeaponViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Beasts { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();
        public string BeastsCount => $"{Beasts.Count}*";
        public ObservableCollection<JungleObjectStatusViewModel> Items { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();
        public string ItemsCount => $"{Items.Count}*";

        public StatusBarViewModel(JungleModel jungleModel, WeaponModel weaponModel, RamblerViewModel ramblerViewModel, TreasureViewModel treasureViewModel)
        {
            Rambler = ramblerViewModel;
            if (Rambler != null)
            {
                Rambler.Moved += RamblerMoved;
            }

            Treasure = treasureViewModel;

            this.jungleModel = jungleModel;
            if (jungleModel != null)
            {
                jungleModel.JungleGenerated += JungleGenerated;
                Load();
            }

            if (weaponModel != null)
            {
                foreach (Weapon weapon in weaponModel.Weapons)
                {
                    Weapons.Add(new WeaponViewModel(weapon));
                }
            }
        }

        private void RamblerMoved(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(ExplorationProgress));
        }

        private RelayCommand saveGame;
        public RelayCommand SaveGame => saveGame ??= new RelayCommand(() => ExecuteSaveGame(), () => CanSaveGame);

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

        private void JungleGenerated(object sender, EventArgs e)
        {
            Load();
            RaisePropertyChanged(nameof(ExplorationProgress));
        }

        private void Load()
        {
            if (jungleModel == null) return;

            Beasts.Clear();
            foreach (JungleObject beast in jungleModel.GetJungleObjects(Config.Beasts))
            {
                Beasts.Add(new JungleObjectStatusViewModel(beast));
            }

            Items.Clear();
            foreach (JungleObject item in jungleModel.GetJungleObjects(Config.VisibleItems))
            {
                Items.Add(new JungleObjectStatusViewModel(item));
            }
            foreach (JungleObject item in jungleModel.GetJungleObjects(Config.HiddenItems))
            {
                Items.Add(new JungleObjectStatusViewModel(item));
            }
            RaisePropertyChanged(nameof(ItemsCount));
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RambleJungle.Model;
using System;
using System.Collections.ObjectModel;

namespace RambleJungle.ViewModel
{
    public class StatusBarViewModel : ObservableRecipient
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
            Rambler.Moved += RamblerMoved;

            Treasure = treasureViewModel;

            this.jungleModel = jungleModel;
            this.jungleModel.JungleGenerated += JungleGenerated;
            Load();

            foreach (Weapon weapon in weaponModel.Weapons)
            {
                Weapons.Add(new WeaponViewModel(weapon));
            }

            SaveGame = new RelayCommand(() => ExecuteSaveGame(), () => CanSaveGame);
        }

        public RelayCommand SaveGame { get; private set; }

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
                SaveGame.NotifyCanExecuteChanged();
            }
        }

        private void RamblerMoved(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ExplorationProgress));
        }

        private void JungleGenerated(object? sender, EventArgs e)
        {
            Load();
            OnPropertyChanged(nameof(ExplorationProgress));
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
            OnPropertyChanged(nameof(ItemsCount));
        }
    }
}

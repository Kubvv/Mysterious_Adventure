﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel;

        public RamblerViewModel Rambler { get; private set; }
        public TreasureViewModel Treasure { get; private set; }
        public ObservableCollection<WeaponViewModel> Weapons { get; private set; } = new ObservableCollection<WeaponViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Beasts { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();
        public ObservableCollection<JungleObjectStatusViewModel> Items { get; private set; } = new ObservableCollection<JungleObjectStatusViewModel>();

        public StatusBarViewModel(JungleModel jungleModel, WeaponModel weaponModel, RamblerViewModel ramblerViewModel, TreasureViewModel treasureViewModel)
        {
            Rambler = ramblerViewModel;
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

        private void JungleGenerated(object sender, EventArgs e)
        {
            Load();
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
        }
    }
}

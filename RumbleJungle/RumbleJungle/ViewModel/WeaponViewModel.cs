﻿using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class WeaponViewModel : ViewModelBase
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();

        private readonly Weapon weapon;

        public string Name => weapon.Name;
        public string Shape => $"/RumbleJungle;component/Images/{weapon.Name}.svg";
        public int Count => weapon.Count;
        public bool DoubleAttack => weapon.DoubleAttack;

        public WeaponViewModel(Weapon weapon)
        {
            this.weapon = weapon;
            weapon.CountChanged += CountChanged;
            weapon.DoubleAttackChanged += DoubleAttackChanged;
        }

        private RelayCommand hitBeastCommand;
        public RelayCommand HitBeastCommand => hitBeastCommand ?? (hitBeastCommand = new RelayCommand(() => gameModel.HitBeastWith(weapon), () => Count != 0));

        private void CountChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Count");
        }

        private void DoubleAttackChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("DoubleAttack");
        }
    }
}

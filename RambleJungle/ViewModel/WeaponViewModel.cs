using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using RambleJungle.Model;
using System;
using System.Windows;

namespace RambleJungle.ViewModel
{
    public class WeaponViewModel : ObservableRecipient
    {
        private readonly GameModel gameModel = Ioc.Default.GetService<GameModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(GameModel)));

        private readonly Weapon weapon;

        public string Name => weapon.Name;
        public FrameworkElement Shape => ShapesModel.GetWeaponShape(weapon.WeaponType);
        public int Count => weapon.Count;
        public bool DoubleAttack => weapon.DoubleAttack;

        public WeaponViewModel(Weapon weapon)
        {
            this.weapon = weapon;
            this.weapon.CountChanged += CountChanged;
            this.weapon.DoubleAttackChanged += DoubleAttackChanged;
            HitBeast = new RelayCommand(() => gameModel.HitBeastWith(this.weapon), () => Count != 0);
        }

        public RelayCommand HitBeast { get; private set; }

        private void CountChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Count));
            HitBeast.NotifyCanExecuteChanged();
        }

        private void DoubleAttackChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(DoubleAttack));
        }
    }
}

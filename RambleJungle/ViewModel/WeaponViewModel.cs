using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using RambleJungle.Model;
using System;
using System.Windows;

namespace RambleJungle.ViewModel
{
    public class WeaponViewModel : ViewModelBase
    {
        private readonly GameModel gameModel = SimpleIoc.Default.GetInstance<GameModel>();
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
            RaisePropertyChanged(nameof(Count));
            HitBeast.RaiseCanExecuteChanged();
        }

        private void DoubleAttackChanged(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(DoubleAttack));
        }
    }
}

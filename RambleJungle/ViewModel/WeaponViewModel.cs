namespace RambleJungle.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using CommunityToolkit.Mvvm.Input;
    using RambleJungle.Base;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    public class WeaponViewModel : ObservableRecipient
    {
        private readonly GameModel gameModel = Ioc.Default.GetService<GameModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(GameModel)));
        private readonly SoundsHelper soundsHelper = Ioc.Default.GetService<SoundsHelper>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(SoundsHelper)));

        private readonly Weapon weapon;

        public string Name => weapon.Name;
        public FrameworkElement Shape => ShapesHelper.GetShape(weapon.WeaponType);
        public int Count => weapon.Count;
        public bool DoubleAttack => weapon.DoubleAttack;
        public RelayCommand HitBeast { get; private set; }


        public WeaponViewModel(Weapon weapon)
        {
            this.weapon = weapon;
            this.weapon.CountChanged += CountChanged;
            this.weapon.DoubleAttackChanged += DoubleAttackChanged;
            HitBeast = new RelayCommand(ExecuteHitBeast, () => Count != 0);
        }

        private async void ExecuteHitBeast()
        {
            if (gameModel.CanUseWeapon(weapon))
            {
                soundsHelper.PlaySound(Name);
                gameModel.HitBeastWith(weapon);
                await Task.Run(() => Thread.Sleep(1000));
                gameModel.FinishAction();
            }
        }

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

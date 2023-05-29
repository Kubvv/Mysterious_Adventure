namespace RambleJungle.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using CommunityToolkit.Mvvm.Input;
    using RambleJungle.Base;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    public class JungleObjectViewModel : ObservableRecipient, IDisposable
    {
        private readonly GameModel gameModel = Ioc.Default.GetService<GameModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(GameModel)));
        private readonly ActionViewModel actionViewModel = Ioc.Default.GetService<ActionViewModel>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(ActionViewModel)));
        private readonly SoundsHelper soundsHelper = Ioc.Default.GetService<SoundsHelper>() ??
            throw new Exception(string.Format(Consts.ServiceNotFound, nameof(SoundsHelper)));

        private readonly JungleObject jungleObject;

        public JungleObjectViewModel Self => this;
        public JungleObjectType JungleObjectType => jungleObject.JungleObjectType;
        public string Name => jungleObject.Name;
        public FrameworkElement Shape => ShapesHelper.GetShape(jungleObject);
        public Statuses Status => jungleObject.Status;
        public bool IsLivingJungleObject => jungleObject is LivingJungleObject;
        public bool IsCamp => jungleObject.JungleObjectType == JungleObjectType.Camp;
        public int Health => jungleObject is LivingJungleObject livingJungleObject ? livingJungleObject.Health : 0;
        public Visibility HealthVisibility => IsLivingJungleObject && Health > 0 ? Visibility.Visible : Visibility.Hidden;
        public bool IsMagnifyingGlassMode => gameModel.IsMagnifyingGlassMode;
        public bool IsArsenal => jungleObject is JungleArsenal;
        public ObservableCollection<WeaponViewModel> ArsenalWeapons { get; } = new ObservableCollection<WeaponViewModel>();

        Thickness margin = new(0);
        public Thickness Margin
        {
            get
            {
                margin.Left = jungleObject.Coordinates.X * Width;
                margin.Top = jungleObject.Coordinates.Y * Height;
                return margin;
            }
        }

        public double Width { get; private set; }
        public double Height { get; private set; }

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            this.jungleObject = jungleObject;
            jungleObject.TypeChanged += TypeChanged;
            jungleObject.StatusChanged += StatusChanged;
            if (jungleObject is LivingJungleObject livingJungleObject)
            {
                livingJungleObject.HealthChanged += HealthChanged;
            }

            if (jungleObject is JungleArsenal jungleArsenal)
            {
                ArsenalWeapons.Clear();
                foreach (Weapon weapon in jungleArsenal.Weapons)
                {
                    ArsenalWeapons.Add(new WeaponViewModel(weapon));
                }
            }
            gameModel.MagnifyingGlassModeChanged += MagnifyingGlassModeChanged;

            MoveRamblerCommand = new RelayCommand(ExecuteMoveRambler);
            AddStrenghtCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.Strenght));
            CheckAdjacentCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.Adjacency));
            AddHealthCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.Health));
            AddDoubleAttackCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.DoubleAttack));
        }

        public RelayCommand MoveRamblerCommand { get; private set; }
        public RelayCommand AddStrenghtCommand { get; private set; }
        public RelayCommand CheckAdjacentCommand { get; private set; }
        public RelayCommand AddHealthCommand { get; private set; }
        public RelayCommand AddDoubleAttackCommand { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (jungleObject != null)
                {
                    jungleObject.TypeChanged -= TypeChanged;
                    jungleObject.StatusChanged -= StatusChanged;
                    if (jungleObject is LivingJungleObject livingJungleObject)
                    {
                        livingJungleObject.HealthChanged -= HealthChanged;
                    }
                }
                gameModel.MagnifyingGlassModeChanged -= MagnifyingGlassModeChanged;
            }
        }

        public void SetSize(double width, double height)
        {
            Width = width;
            Height = height;
            Update();
        }

        public virtual void Update()
        {
            OnPropertyChanged(nameof(Margin));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Health));
        }

        private void TypeChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Shape));
            OnPropertyChanged(nameof(JungleObjectType));
            OnPropertyChanged(nameof(Name));
        }

        private void StatusChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Self));
            if (Status == Statuses.Shown)
            {
                actionViewModel.CurrentJungleObject = this;
                if (JungleObjectType == JungleObjectType.Camp || JungleObjectType == JungleObjectType.ForgottenCity)
                {
                    actionViewModel.ActionVisibility = Visibility.Visible;
                }
            }
            else if (Status == Statuses.Visited)
            {
                actionViewModel.ActionVisibility = Visibility.Hidden;
            }
        }

        private void HealthChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Health));
            OnPropertyChanged(nameof(HealthVisibility));
        }

        private void MagnifyingGlassModeChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsMagnifyingGlassMode));
        }

        private async void ExecuteMoveRambler()
        {
            if (!gameModel.IsActionMode)
            {
                gameModel.MoveRamblerTo(jungleObject.Coordinates);
                if (gameModel.IsActionMode)
                {
                    if (!Config.Beasts.Contains(JungleObjectType))
                    {
                        soundsHelper.PlaySound(Name);
                    }
                    await Task.Run(() => Thread.Sleep(1000));
                    gameModel.FinishAction();
                }
            }
        }
    }
}
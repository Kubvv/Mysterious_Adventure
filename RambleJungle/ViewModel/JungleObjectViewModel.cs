using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using RambleJungle.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace RambleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase, IDisposable
    {
        private readonly GameModel gameModel = SimpleIoc.Default.GetInstance<GameModel>();
        private readonly ActionViewModel actionViewModel = SimpleIoc.Default.GetInstance<ActionViewModel>();

        private readonly JungleObject jungleObject;

        public JungleObjectViewModel Self => this;
        public JungleObjectType JungleObjectType => jungleObject.JungleObjectType;
        public string Name => jungleObject.Name;
        public FrameworkElement Shape => ShapesModel.GetJungleShape(jungleObject.JungleObjectType, jungleObject.BackingObject);
        public Statuses Status => jungleObject.Status;
        public bool IsLivingJungleObject => jungleObject is LivingJungleObject;
        public bool IsCamp => jungleObject.JungleObjectType == JungleObjectType.Camp;
        public int Health => jungleObject is LivingJungleObject livingJungleObject ? livingJungleObject.Health : 0;
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

            MoveRamblerCommand = new RelayCommand(() => gameModel.MoveRamblerTo(this.jungleObject.Coordinates));
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
            RaisePropertyChanged(nameof(Margin));
            RaisePropertyChanged(nameof(Width));
            RaisePropertyChanged(nameof(Height));
            RaisePropertyChanged(nameof(Health));
        }

        private void TypeChanged(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Shape));
            RaisePropertyChanged(nameof(JungleObjectType));
            RaisePropertyChanged(nameof(Name));
        }

        private void StatusChanged(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Self));
            if (Status == Statuses.Shown)
            {
                actionViewModel.CurrentJungleObject = this;
                actionViewModel.ActionVisibility = Visibility.Visible;
            }
            else if (Status == Statuses.Visited)
            {
                actionViewModel.ActionVisibility = Visibility.Hidden;
            }
        }

        private void HealthChanged(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Health));
        }

        private void MagnifyingGlassModeChanged(object? sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(IsMagnifyingGlassMode));
        }
    }
}
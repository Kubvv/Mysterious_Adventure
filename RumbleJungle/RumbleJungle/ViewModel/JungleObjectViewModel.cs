using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase, IDisposable
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly ActionViewModel actionViewModel = ServiceLocator.Current.GetInstance<ActionViewModel>();

        private readonly JungleObject jungleObject;

        public JungleObjectViewModel Self => this;
        public JungleObjectType JungleObjectType => jungleObject.JungleObjectType;
        public string Name => jungleObject.Name;
        public string Shape => $"/RumbleJungle;component/Images/{jungleObject.Name}.svg";
        public Statuses Status => jungleObject.Status;
        public bool IsLivingJungleObject => jungleObject is LivingJungleObject;
        public bool IsCamp => jungleObject.JungleObjectType == JungleObjectType.Camp;
        public int Health => IsLivingJungleObject ? (jungleObject as LivingJungleObject).Health : 0;
        public bool IsMagnifyingGlassMode => gameModel.IsMagnifyingGlassMode;

        Thickness margin = new Thickness(0);
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

        private RelayCommand moveRamblerCommand;
        public RelayCommand MoveRamblerCommand => moveRamblerCommand ?? (moveRamblerCommand = new RelayCommand(() => gameModel.MoveRamblerTo(jungleObject.Coordinates)));

        private RelayCommand addStrenghtCommand;
        public RelayCommand AddStrenghtCommand => addStrenghtCommand ?? (addStrenghtCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.Strenght)));

        private RelayCommand checkAdjacentCommand;
        public RelayCommand CheckAdjacentCommand => checkAdjacentCommand ?? (checkAdjacentCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.Adjacency)));

        private RelayCommand addHealthCommand;
        public RelayCommand AddHealthCommand => addHealthCommand ?? (addHealthCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.Health)));

        private RelayCommand addDoubleAttackCommand;
        public RelayCommand AddDoubleAttackCommand => addDoubleAttackCommand ?? (addDoubleAttackCommand = new RelayCommand(() => gameModel.CampBonus(CampBonus.DoubleAttack)));

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            this.jungleObject = jungleObject;
            if (jungleObject != null)
            {
                jungleObject.TypeChanged += TypeChanged;
                jungleObject.StatusChanged += StatusChanged;
                if (IsLivingJungleObject) (jungleObject as LivingJungleObject).HealthChanged += HealthChanged;
            }
            gameModel.MagnifyingGlassModeChanged += MagnifyingGlassModeChanged;
        }

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
                    if (IsLivingJungleObject) (jungleObject as LivingJungleObject).HealthChanged -= HealthChanged;
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

        private void TypeChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Shape));
            RaisePropertyChanged(nameof(JungleObjectType));
            RaisePropertyChanged(nameof(Name));
        }

        private void StatusChanged(object sender, EventArgs e)
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

        private void HealthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Health));
        }

        private void MagnifyingGlassModeChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(IsMagnifyingGlassMode));
        }
    }
}
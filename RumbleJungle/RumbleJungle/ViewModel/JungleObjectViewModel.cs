using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();
        private readonly ActionViewModel actionViewModel = ServiceLocator.Current.GetInstance<ActionViewModel>();

        private JungleObject jungleObject;

        public JungleObjectViewModel Self => this;
        public JungleObjectTypes JungleObjectType => jungleObject.JungleObjectType;
        public string Name => jungleObject.Name;
        public string Shape => $"/RumbleJungle;component/Images/{jungleObject.Name}.svg";
        public Statuses Status => jungleObject.Status;
        public bool IsLivingJungleObject => jungleObject is LivingJungleObject;
        public bool IsCamp => jungleObject.JungleObjectType == JungleObjectTypes.Camp;
        public int Health => IsLivingJungleObject ? (jungleObject as LivingJungleObject).Health : 0;

        Thickness margin = new Thickness(0);
        public Thickness Margin
        {
            get
            {
                margin.Left = jungleObject.Coordinates.X * jungleViewModel.CellWidth;
                margin.Top = jungleObject.Coordinates.Y * jungleViewModel.CellHeight;
                return margin;
            }
        }

        public double Width => jungleViewModel.CellWidth;
        public double Height => jungleViewModel.CellHeight;

        private RelayCommand moveRamblerCommand;
        public RelayCommand MoveRamblerCommand => moveRamblerCommand ?? (moveRamblerCommand = new RelayCommand(() => gameModel.MoveRamblerTo(jungleObject.Coordinates)));

        private RelayCommand addStrenghtCommand;
        public RelayCommand AddStrenghtCommand => addStrenghtCommand ?? (addStrenghtCommand = new RelayCommand(() =>
        {
            gameModel.Rambler.SetStrength(1.3);
            gameModel.Rambler.SetCoordinates(jungleObject.Coordinates);
        }));

        private RelayCommand checkAdjacentCommand;
        public RelayCommand CheckAdjacentCommand => checkAdjacentCommand ?? (checkAdjacentCommand = new RelayCommand(() =>
        {
            jungleObject.CheckAdjacent();
            gameModel.Rambler.SetCoordinates(jungleObject.Coordinates);
        }));

        private RelayCommand addHealthCommand;
        public RelayCommand AddHealthCommand => addHealthCommand ?? (addHealthCommand = new RelayCommand(() =>
        {
            gameModel.Rambler.ChangeHealth(15);
            gameModel.Rambler.SetCoordinates(jungleObject.Coordinates);
        }));

        private RelayCommand addDoubleAttackCommand;
        public RelayCommand AddDoubleAttackCommand => addDoubleAttackCommand ?? (addDoubleAttackCommand = new RelayCommand(() =>
        {
            //TODO: Add double attack to random weapon
            gameModel.Rambler.ChangeHealth(0);
            gameModel.Rambler.SetCoordinates(jungleObject.Coordinates);
        }));

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            this.jungleObject = jungleObject;
            jungleObject.TypeChanged += TypeChanged;
            jungleObject.StatusChanged += StatusChanged;
            if (IsLivingJungleObject) (jungleObject as LivingJungleObject).HealthChanged += HealthChanged;
        }

        public void Update()
        {
            RaisePropertyChanged("Margin");
            RaisePropertyChanged("Width");
            RaisePropertyChanged("Height");
            RaisePropertyChanged("Health");
        }

        private void TypeChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Shape");
            RaisePropertyChanged("JungleObjectType");
            RaisePropertyChanged("Name");
        }

        private void StatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Self");
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
            RaisePropertyChanged("Health");
        }
    }
}
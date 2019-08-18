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
        public RelayCommand MoveRamblerCommand => moveRamblerCommand ?? (moveRamblerCommand = new RelayCommand(() => gameModel.MoveRambler(jungleObject.Coordinates)));

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            this.jungleObject = jungleObject;
            jungleObject.StatusChanged += StatusChanged;
            jungleObject.TypeChanged += TypeChanged;
        }

        private void TypeChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Shape");
            RaisePropertyChanged("JungleObjectType");
            RaisePropertyChanged("Name");
        }

        public void Update()
        {
            RaisePropertyChanged("Margin");
            RaisePropertyChanged("Width");
            RaisePropertyChanged("Height");
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
    }
}
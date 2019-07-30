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
        private readonly GameModel gameManager = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly JungleModel jungleManager = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        private JungleObject jungleObject;

        public JungleObjectViewModel Self => this;
        public JungleObjectTypes JungleObjectType => jungleObject.JungleObjectType;
        public string Name => jungleObject.Name;
        public string Shape => $"/RumbleJungle;component/Images/{jungleObject.Name}.svg";
        public int Count => jungleManager.CountOf(jungleObject.JungleObjectType);
        public Statuses Status => jungleObject.Status;

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

        private RelayCommand moveRamblerCommand;
        public RelayCommand MoveRamblerCommand => moveRamblerCommand ?? (moveRamblerCommand = new RelayCommand(() => gameManager.MoveRambler(jungleObject.Coordinates)));

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            this.jungleObject = jungleObject;
            jungleObject.StatusChanged += StatusChanged;
        }

        public void Update()
        {
            RaisePropertyChanged("Margin");
        }
        private void StatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Count");
            RaisePropertyChanged("Found");
            RaisePropertyChanged("Self");
        }
    }
}
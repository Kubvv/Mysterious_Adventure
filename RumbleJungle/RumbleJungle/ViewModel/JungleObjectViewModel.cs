using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase
    {
        private readonly GameManager gameManager = ServiceLocator.Current.GetInstance<GameManager>();
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();
        private readonly JungleObject jungleObject;

        public string Name => jungleObject.Name;
        public string Shape => $"/RumbleJungle;component/Images/{jungleObject.Shape}.svg";
        public int Count => jungleManager.CountOf(jungleObject.JungleObjectType);

        private string coordinates = "";
        public string Coordinates
        {
            get => coordinates;
            set => Set(ref coordinates, value);
        }

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
            Coordinates = $"{jungleObject.Coordinates.Y}.{jungleObject.Coordinates.X}";
        }

        public void Update()
        {
            Coordinates = $"{jungleObject.Coordinates.Y}.{jungleObject.Coordinates.X}";
            RaisePropertyChanged("Margin");
        }
    }
}
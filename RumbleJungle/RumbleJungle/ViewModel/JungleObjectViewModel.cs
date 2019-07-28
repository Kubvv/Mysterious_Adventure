using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase
    {
        private readonly GameModel gameManager = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly JungleModel jungleManager = ServiceLocator.Current.GetInstance<JungleModel>();
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        public JungleObject JungleObject { get; private set; }
        public string Name => JungleObject.Name;
        public string Shape => $"/RumbleJungle;component/Images/{JungleObject.Name}.svg";
        public int Count => jungleManager.CountOf(JungleObject.JungleObjectType);

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
                margin.Left = JungleObject.Coordinates.X * jungleViewModel.CellWidth;
                margin.Top = JungleObject.Coordinates.Y * jungleViewModel.CellHeight;
                return margin;
            }
        }

        private RelayCommand moveRamblerCommand;
        public RelayCommand MoveRamblerCommand => moveRamblerCommand ?? (moveRamblerCommand = new RelayCommand(() => gameManager.MoveRambler(JungleObject.Coordinates)));

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            JungleObject = jungleObject;
            Coordinates = $"{jungleObject.Coordinates.Y}.{jungleObject.Coordinates.X}";
        }

        public void Update()
        {
            Coordinates = $"{JungleObject.Coordinates.Y}.{JungleObject.Coordinates.X}";
            RaisePropertyChanged("Margin");
        }
    }
}
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();
        private JungleObject jungleObject;

        private string name = "";
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private string coordinates = "";
        public string Coordinates
        {
            get => coordinates;
            set => Set(ref coordinates, value);
        }

        private RelayCommand moveRamblerCommand;
        public RelayCommand MoveRamblerCommand => moveRamblerCommand ?? (moveRamblerCommand = new RelayCommand(() =>
        {
            jungleViewModel.MoveRambler(jungleObject.Coordinates);
        }));

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            this.jungleObject = jungleObject;

            if (jungleObject != null)
            {
                string[] splittedName = jungleObject.ToString().Split('.');
                Name = splittedName[splittedName.Length - 1];

                Coordinates = $"{jungleObject.Coordinates.Y}.{jungleObject.Coordinates.X}";
            }
        }
    }
}
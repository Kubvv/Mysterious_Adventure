using GalaSoft.MvvmLight;
using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class JungleObjectViewModel : ViewModelBase
    {
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

        public JungleObjectViewModel(JungleObject jungleObject)
        {
            if (jungleObject != null)
            {
                string[] splittedName = jungleObject.ToString().Split('.');
                Name = splittedName[splittedName.Length - 1];

                Coordinates = $"{jungleObject.Coordinates.Y}.{jungleObject.Coordinates.X}";
            }
        }
    }
}
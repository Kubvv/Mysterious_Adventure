using GalaSoft.MvvmLight;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class ActionViewModel : ViewModelBase
    {
        public JungleObjectViewModel JungleObjectViewModel { get; set; }
        public string Shape => JungleObjectViewModel?.Shape;

        private Visibility actionVisibility = Visibility.Hidden;
        public Visibility ActionVisibility
        {
            get => actionVisibility;
            set
            {
                Set(ref actionVisibility, value);
                if (value == Visibility.Visible)
                {
                    RaisePropertyChanged("Shape");
                }
            }
        }
    }
}

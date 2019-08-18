using GalaSoft.MvvmLight;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class ActionViewModel : ViewModelBase
    {
        public JungleObjectViewModel CurrentJungleObject { get; set; }

        private Visibility actionVisibility = Visibility.Hidden;
        public Visibility ActionVisibility
        {
            get => actionVisibility;
            set
            {
                Set(ref actionVisibility, value);
                if (value == Visibility.Visible)
                {
                    RaisePropertyChanged("CurrentJungleObject");
                }
            }
        }
    }
}

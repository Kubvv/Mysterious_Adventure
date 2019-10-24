using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class ActionViewModel : ViewModelBase
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        private JungleObjectViewModel forgottenCityViewModel = null;

        private JungleObjectViewModel currentJungleObject;
        public JungleObjectViewModel CurrentJungleObject
        {
            get => currentJungleObject;
            set => Set(ref currentJungleObject, value);
        }

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

        public ActionViewModel()
        {
            gameModel.ForgottenCityModeChanged += ForgottenCityModeChanged;
        }

        private void ForgottenCityModeChanged(object sender, EventArgs e)
        {
            if (gameModel.IsForgottenCityMode)
            {
                if (forgottenCityViewModel == null)
                {
                    forgottenCityViewModel = CurrentJungleObject;
                }
                CurrentJungleObject = new JungleObjectViewModel(gameModel.CurrentJungleObject);
            }
            else
            {
                CurrentJungleObject = forgottenCityViewModel;
                forgottenCityViewModel = null;
            }
        }
    }
}

using GalaSoft.MvvmLight;
using RambleJungle.Model;
using System;
using System.Windows;

namespace RambleJungle.ViewModel
{
    public class ActionViewModel : ViewModelBase
    {
        private readonly GameModel gameModel;
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
                    RaisePropertyChanged(nameof(CurrentJungleObject));
                }
            }
        }

        public ActionViewModel(GameModel gameModel)
        {
            this.gameModel = gameModel;
            if (gameModel != null)
            {
                gameModel.ForgottenCityModeChanged += ForgottenCityModeChanged;
            }
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

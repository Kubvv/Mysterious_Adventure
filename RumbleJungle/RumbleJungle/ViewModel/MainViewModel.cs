using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        public MainViewModel()
        {

        }

        private RelayCommand startNewGame;

        public RelayCommand StartNewGame => startNewGame ?? (startNewGame = new RelayCommand(() => ExecuteStartNewGame()));

        private void ExecuteStartNewGame()
        {
            jungleViewModel.StartGame();

        }

        private RelayCommand openConfigurationOptions;

        public RelayCommand OpenConfigurationOptions => openConfigurationOptions ?? (openConfigurationOptions = new RelayCommand(() => ExecuteOpenConfigurationOptions()));

        private void ExecuteOpenConfigurationOptions()
        {
            throw new NotImplementedException();
        }

        private RelayCommand closeAppCommand;
        public RelayCommand CloseAppCommand => closeAppCommand ?? (closeAppCommand = new RelayCommand(() => Application.Current.Shutdown()));

        
    }

}
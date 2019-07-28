using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.View;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        public MainViewModel()
        {
        }

        private RelayCommand startNewGame;

        public RelayCommand StartNewGame => startNewGame ?? (startNewGame = new RelayCommand(() => ExecuteStartNewGame()));

        private void ExecuteStartNewGame()
        {
            jungleViewModel.StartGame();
            JungleView jungleView = new JungleView();
            jungleView.ShowDialog();
        }

        private RelayCommand openConfigurationOptions;

        public RelayCommand OpenConfigurationOptions => openConfigurationOptions ?? (openConfigurationOptions = new RelayCommand(() => ExecuteOpenConfigurationOptions()));

        private void ExecuteOpenConfigurationOptions()
        {
            OptionsView optionsView = new OptionsView();
            optionsView.ShowDialog();
        }

        private RelayCommand closeAppCommand;
        public RelayCommand CloseAppCommand => closeAppCommand ?? (closeAppCommand = new RelayCommand(() => Application.Current.Shutdown()));

        
    }

}
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using RumbleJungle.View;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        public MainViewModel()
        {
        }

        private RelayCommand startNewGame;
        public RelayCommand StartNewGame => startNewGame ?? (startNewGame = new RelayCommand(() => ExecuteStartNewGame(), () => CanStartNewGame));

        private void ExecuteStartNewGame()
        {
            gameModel.StartGame();
            jungleViewModel.StartGame();
            JungleView jungleView = new JungleView();
            jungleView.ShowDialog();
        }

        private bool canStartNewGame = true;
        public bool CanStartNewGame
        {
            get => canStartNewGame;
            set
            {
                canStartNewGame = value;
                StartNewGame.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand loadGame;
        public RelayCommand LoadGame => loadGame ?? (loadGame = new RelayCommand(() => ExecuteLoadGame(), () => CanLoadGame));

        private void ExecuteLoadGame()
        {
            // TODO: load game
        }

        private bool canLoadGame = true;
        public bool CanLoadGame
        {
            get => canLoadGame;
            set
            {
                canLoadGame = value;
                LoadGame.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand saveGame;
        public RelayCommand SaveGame => saveGame ?? (saveGame = new RelayCommand(() => ExecuteSaveGame(), () => CanSaveGame));

        private void ExecuteSaveGame()
        {
            // TODO: save game
        }

        private bool canSaveGame = true;
        public bool CanSaveGame
        {
            get => canSaveGame;
            set
            {
                canSaveGame = value;
                SaveGame.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand openOptions;
        public RelayCommand OpenOptions => openOptions ?? (openOptions = new RelayCommand(() => ExecuteOpenOptions(), () => CanOpenOptions));

        private void ExecuteOpenOptions()
        {
            OptionsView optionsView = new OptionsView();
            optionsView.ShowDialog();
        }

        private bool canOpenOptions = true;
        public bool CanOpenOptions
        {
            get => canOpenOptions;
            set
            {
                canOpenOptions = value;
                OpenOptions.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand closeAppCommand;
        public RelayCommand CloseAppCommand => closeAppCommand ?? (closeAppCommand = new RelayCommand(() => Application.Current.Shutdown()));
    }
}
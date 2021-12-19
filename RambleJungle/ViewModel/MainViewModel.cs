using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RambleJungle.Model;
using RambleJungle.View;
using System.Windows;

namespace RambleJungle.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly GameModel gameModel;

        public MainViewModel(GameModel gameModel)
        {
            this.gameModel = gameModel;
        }

        private RelayCommand startNewGame;
        public RelayCommand StartNewGame => startNewGame ??= new RelayCommand(() => ExecuteStartNewGame(), () => CanStartNewGame);

        private void ExecuteStartNewGame()
        {
            gameModel.PrepareGame();
            //Task startGameTask = Task.Run(() => gameModel.StartGame());
            gameModel.StartGame();
            JungleView jungleView = new();
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
        public RelayCommand LoadGame => loadGame ??= new RelayCommand(() => ExecuteLoadGame(), () => CanLoadGame);

        private static void ExecuteLoadGame()
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

        private RelayCommand openOptions;
        public RelayCommand OpenOptions => openOptions ??= new RelayCommand(() => ExecuteOpenOptions(), () => CanOpenOptions);

        private static void ExecuteOpenOptions()
        {
            OptionsView optionsView = new();
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

        private RelayCommand closeApp;
        public RelayCommand CloseApp => closeApp ??= new RelayCommand(() => Application.Current.Shutdown());
    }
}
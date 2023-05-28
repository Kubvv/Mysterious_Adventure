namespace RambleJungle.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using RambleJungle.Base;
    using RambleJungle.View;
    using System.Windows;

    public class MainViewModel : ObservableRecipient
    {
        private readonly GameModel gameModel;

        public MainViewModel(GameModel gameModel)
        {
            this.gameModel = gameModel;

            StartNewGame = new RelayCommand(() => ExecuteStartNewGame(), () => CanStartNewGame);
            LoadGame = new RelayCommand(() => ExecuteLoadGame(), () => CanLoadGame);
            OpenOptions = new RelayCommand(() => ExecuteOpenOptions(), () => CanOpenOptions);
            CloseApp = new RelayCommand(() => Application.Current.Shutdown());
        }

        public RelayCommand StartNewGame { get; private set; }
        public RelayCommand LoadGame { get; private set; }
        public RelayCommand OpenOptions { get; private set; }
        public RelayCommand CloseApp { get; private set; }

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
                StartNewGame.NotifyCanExecuteChanged();
            }
        }

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
                LoadGame.NotifyCanExecuteChanged();
            }
        }

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
                OpenOptions.NotifyCanExecuteChanged();
            }
        }
    }
}
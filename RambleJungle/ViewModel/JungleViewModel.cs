namespace RambleJungle.ViewModel
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using RambleJungle.Base;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Threading;

    public class JungleViewModel : ObservableRecipient
    {
        private readonly JungleModel jungleModel;
        private readonly SoundsHelper soundsHelper;

        private double cellWidth, cellHeight;
        private readonly DispatcherTimer updateTimer = new();

        private double canvasWidth;
        public double CanvasWidth
        {
            get => canvasWidth;
            set
            {
                SetProperty(ref canvasWidth, value);
                cellWidth = Math.Floor(value / Config.JungleWidth);
                updateTimer.Stop();
                updateTimer.Start();
            }
        }

        private double canvasHeight;
        public double CanvasHeight
        {
            get => canvasHeight;
            set
            {
                SetProperty(ref canvasHeight, value);
                cellHeight = Math.Floor(value / Config.JungleHeight);
                updateTimer.Stop();
                updateTimer.Start();
            }
        }

        public ObservableCollection<JungleObjectViewModel> JungleObjectsViewModel { get; } = new ObservableCollection<JungleObjectViewModel>();

        public RamblerViewModel RamblerViewModel { get; private set; }

        public JungleViewModel(JungleModel jungleModel, GameModel gameModel, RamblerViewModel ramblerViewModel, SoundsHelper soundsHelper)
        {
            this.jungleModel = jungleModel;
            if (jungleModel != null)
            {
                jungleModel.JungleGenerated += JungleGenerated;
                Load();
            }
            gameModel.BeastBites += BeastBites;
            gameModel.GameOver += GameOver;
            RamblerViewModel = ramblerViewModel;
            this.soundsHelper = soundsHelper;

            updateTimer.Tick += UpdateTimerTick;
            updateTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
        }

        private void UpdateTimerTick(object? sender, EventArgs e)
        {
            updateTimer.Stop();
            foreach (JungleObjectViewModel jungleObjectViewModel in JungleObjectsViewModel)
            {
                jungleObjectViewModel.SetSize(cellWidth, cellHeight);
            }
            RamblerViewModel.SetSize(cellWidth, cellHeight);
        }

        private void JungleGenerated(object? sender, EventArgs e)
        {
            Load();
        }

        private void Load()
        {
            if (jungleModel == null) return;

            JungleObjectsViewModel.Clear();
            foreach (JungleObject jungleObject in jungleModel.Jungle)
            {
                JungleObjectsViewModel.Add(new JungleObjectViewModel(jungleObject));
            }
        }
        private void BeastBites(object? sender, string nameOfTheBeast)
        {
            soundsHelper.PlaySound(nameOfTheBeast);
        }


        private void GameOver(object? sender, bool success)
        {
            soundsHelper.PlaySound(success ? "Success" : "Fail");
        }
    }
}

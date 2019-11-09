using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private readonly JungleModel jungleModel;
        private double cellWidth, cellHeight;
        private readonly DispatcherTimer updateTimer = new DispatcherTimer();

        private double canvasWidth;
        public double CanvasWidth
        {
            get => canvasWidth;
            set
            {
                Set(ref canvasWidth, value);
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
                Set(ref canvasHeight, value);
                cellHeight = Math.Floor(value / Config.JungleHeight);
                updateTimer.Stop();
                updateTimer.Start();
            }
        }

        public ObservableCollection<JungleObjectViewModel> JungleObjectsViewModel { get; } = new ObservableCollection<JungleObjectViewModel>();

        public RamblerViewModel RamblerViewModel { get; private set; }

        public JungleViewModel(JungleModel jungleModel, RamblerViewModel ramblerViewModel)
        {
            this.jungleModel = jungleModel;
            if (jungleModel != null)
            {
                jungleModel.JungleGenerated += JungleGenerated;
                Load();
            }
            RamblerViewModel = ramblerViewModel;

            updateTimer.Tick += UpdateTimerTick;
            updateTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
        }

        private void UpdateTimerTick(object sender, EventArgs e)
        {
            updateTimer.Stop();
            foreach (JungleObjectViewModel jungleObjectViewModel in JungleObjectsViewModel)
            {
                jungleObjectViewModel.SetSize(cellWidth, cellHeight);
            }
            RamblerViewModel.SetSize(cellWidth, cellHeight);
        }

        private void JungleGenerated(object sender, EventArgs e)
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
    }
}

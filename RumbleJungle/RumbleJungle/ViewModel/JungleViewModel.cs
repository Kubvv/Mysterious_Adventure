using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private readonly GameModel gameModel = ServiceLocator.Current.GetInstance<GameModel>();
        private readonly JungleModel jungleModel = ServiceLocator.Current.GetInstance<JungleModel>();

        public int JungleHeight => Configuration.JungleHeight;
        public int JungleWidth => Configuration.JungleWidth;

        private double canvasWidth;
        public double CanvasWidth
        {
            get => canvasWidth;
            set
            {
                Set(ref canvasWidth, value);
                CellWidth = Math.Floor(value / Configuration.JungleWidth);
                UpdateJungle();
            }
        }

        private double canvasHeight;

        public double CanvasHeight
        {
            get => canvasHeight;
            set
            {
                Set(ref canvasHeight, value);
                CellHeight = Math.Floor(value / Configuration.JungleHeight);
                UpdateJungle();
            }
        }

        private double cellWidth;
        public double CellWidth
        {
            get => cellWidth;
            set => Set(ref cellWidth, value);
        }

        private double cellHeight;
        public double CellHeight
        {
            get => cellHeight;
            set => Set(ref cellHeight, value);
        }

        private ObservableCollection<JungleObjectViewModel> jungleObjectsViewModel = new ObservableCollection<JungleObjectViewModel>();
        public ObservableCollection<JungleObjectViewModel> JungleObjectsViewModel
        {
            get => jungleObjectsViewModel;
            set => Set(ref jungleObjectsViewModel, value);
        }

        public RamblerViewModel RamblerViewModel { get; private set; }

        public JungleViewModel()
        {
        }

        internal void StartGame()
        {
            RamblerViewModel = ServiceLocator.Current.GetInstance<RamblerViewModel>();
            foreach (JungleObject jungleObject in jungleModel.Jungle)
            {
                JungleObjectsViewModel.Add(new JungleObjectViewModel(jungleObject));
            }
        }

        private void UpdateJungle()
        {
            foreach (JungleObjectViewModel jungleObjectViewModel in jungleObjectsViewModel)
            {
                jungleObjectViewModel.Update();
            }
            RamblerViewModel.Update();
        }
    }
}

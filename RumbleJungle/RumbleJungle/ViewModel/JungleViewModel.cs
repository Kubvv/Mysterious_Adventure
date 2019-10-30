using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private double cellWidth, cellHeight;

        private double canvasWidth;
        public double CanvasWidth
        {
            get => canvasWidth;
            set
            {
                Set(ref canvasWidth, value);
                cellWidth = Math.Floor(value / Configuration.JungleWidth);
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
                cellHeight = Math.Floor(value / Configuration.JungleHeight);
                UpdateJungle();
            }
        }

        private ObservableCollection<JungleObjectViewModel> jungleObjectsViewModel = new ObservableCollection<JungleObjectViewModel>();
        public ObservableCollection<JungleObjectViewModel> JungleObjectsViewModel
        {
            get => jungleObjectsViewModel;
            set => Set(ref jungleObjectsViewModel, value);
        }

        public RamblerViewModel RamblerViewModel { get; private set; }

        public JungleViewModel(JungleModel jungleModel, RamblerViewModel ramblerViewModel)
        {
            RamblerViewModel = ramblerViewModel;
            foreach (JungleObject jungleObject in jungleModel.Jungle)
            {
                JungleObjectsViewModel.Add(new JungleObjectViewModel(jungleObject));
            }
        }

        private void UpdateJungle()
        {
            foreach (JungleObjectViewModel jungleObjectViewModel in jungleObjectsViewModel)
            {
                jungleObjectViewModel.SetSize(cellWidth, cellHeight);
            }
            RamblerViewModel.SetSize(cellWidth, cellHeight);
        }
    }
}

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private readonly GameManager gameManager = ServiceLocator.Current.GetInstance<GameManager>();
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();

        public int JungleHeight => Configuration.JungleHeight;
        public int JungleWidth => Configuration.JungleWidth;

        private double canvasWidth;
        public double CanvasWidth
        {
            get => canvasWidth;
            set
            {
                Set(ref canvasWidth, value);
                CellWidth = value / Configuration.JungleWidth;
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
                CellHeight = value / Configuration.JungleHeight;
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

        public RamblerViewModel Rambler { get; private set; }

        public JungleViewModel()
        {
        }

        internal void StartGame()
        {
            Rambler = ServiceLocator.Current.GetInstance<RamblerViewModel>();
            gameManager.StartGame();
            foreach (JungleObject jungleObject in jungleManager.JungleObjects)
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
            Rambler.Update();
        }
    }
}

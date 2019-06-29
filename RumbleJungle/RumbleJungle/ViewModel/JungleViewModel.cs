using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private Jungle jungle = new Jungle();

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
                foreach (JungleObjectViewModel jungleObjectViewModel in jungleObjectsViewModel)
                    jungleObjectViewModel.Update();
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
                foreach (JungleObjectViewModel jungleObjectViewModel in jungleObjectsViewModel)
                    jungleObjectViewModel.Update();
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

        public JungleViewModel()
        {
            
        }

        internal void StartGame()
        {
            jungle.Generate();

            foreach (JungleObject jungleObject in jungle.JungleObjects)
            {
                JungleObjectsViewModel.Add(new JungleObjectViewModel(jungleObject));
            }
            //for (int row = 0; row < configuration.jungleheight; row++)
            //{
            //    for (int col = 0; col < configuration.junglewidth; col++)
            //    {
            //        jungleobject jungleobject = jungle.jungleobjects.firstordefault(jo => jo.coordinates.y == row && jo.coordinates.x == col);
            //        jungleobjectsviewmodel.add(new jungleobjectviewmodel(jungleobject));
            //    }
            //}
        }

        internal void MoveRambler(Point coordinates)
        {
            jungle.MoveRambler(coordinates);
        }
    }
}

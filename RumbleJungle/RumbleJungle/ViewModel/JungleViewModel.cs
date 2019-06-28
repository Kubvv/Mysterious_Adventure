using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System;
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

            for (int row = 0; row < Configuration.JungleHeight; row++)
            {
                for (int col = 0; col < Configuration.JungleWidth; col++)
                {
                    JungleObject jungleObject = jungle.JungleObjects.FirstOrDefault(jo => jo.Coordinates.Y == row && jo.Coordinates.X == col);
                    JungleObjectsViewModel.Add(new JungleObjectViewModel(jungleObject));
                }
            }
        }

        internal void MoveRambler(Point coordinates)
        {
            jungle.MoveRambler(coordinates);
        }
    }
}

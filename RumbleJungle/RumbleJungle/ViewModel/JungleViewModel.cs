using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class JungleViewModel : ViewModelBase
    {
        private Jungle jungle = new Jungle();

        public int JungleHeight => Configuration.JungleHeight;
        public int JungleWidth => Configuration.JungleWidth;

        private ObservableCollection<JungleObjectViewModel> jungleObjects = new ObservableCollection<JungleObjectViewModel>();
        public ObservableCollection<JungleObjectViewModel> JungleObjects
        {
            get => jungleObjects;
            set => Set(ref jungleObjects, value);
        }

        public JungleViewModel()
        {
            for (int row = 0; row < JungleHeight; row++)
            {
                for (int col = 0; col < JungleWidth; col++)
                {
                    jungleObjects.Add(new JungleObjectViewModel(row, col));
                }
            }
        }

        internal void StartGame()
        {
            jungle.Generate();
        }
    }
}

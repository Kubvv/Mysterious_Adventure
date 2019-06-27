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
                JungleObjectsViewModel.Add(new JungleObjectViewModel(jungleObject.Coordinates));
            }
        }
    }
}

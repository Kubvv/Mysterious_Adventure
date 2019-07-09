using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        public ObservableCollection<BeastViewModel> Beasts { get; set; }

        public StatusBarViewModel()
        {
            Beasts = new ObservableCollection<BeastViewModel>();
            Beasts.Add(new BeastViewModel(JungleObjectTypes.CarnivorousPlant));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.DragonflySwarm));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.Hydra));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.Minotaur));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.Snakes));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.WildPig));
        }
    }
}

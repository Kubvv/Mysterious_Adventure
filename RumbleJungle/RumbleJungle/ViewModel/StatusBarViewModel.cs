using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        public ObservableCollection<BeastViewModel> Beasts { get; set; }

        public ObservableCollection<ItemsViewModel> Items { get; set; }

        public TreasureViewModel Treasure { get; set; }

        public StatusBarViewModel()
        {
            Beasts = new ObservableCollection<BeastViewModel>();
            Beasts.Add(new BeastViewModel(JungleObjectTypes.DragonflySwarm));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.WildPig));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.Snakes));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.CarnivorousPlant));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.Minotaur));
            Beasts.Add(new BeastViewModel(JungleObjectTypes.Hydra));

            Items = new ObservableCollection<ItemsViewModel>();
            Items.Add(new ItemsViewModel(JungleObjectTypes.Camp));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Tent));
            Items.Add(new ItemsViewModel(JungleObjectTypes.ForgottenCity));
            Items.Add(new ItemsViewModel(JungleObjectTypes.DenseJungle));
            Items.Add(new ItemsViewModel(JungleObjectTypes.LostWeapon));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Elixir));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Map));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Compass));
            Items.Add(new ItemsViewModel(JungleObjectTypes.MagnifyingGlass));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Talisman));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Natives));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Quicksand));
            Items.Add(new ItemsViewModel(JungleObjectTypes.Trap));

            Treasure = new TreasureViewModel(JungleObjectTypes.Treasure);

        }
    }
}

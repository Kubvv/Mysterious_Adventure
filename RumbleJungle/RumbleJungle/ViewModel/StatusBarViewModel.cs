using CommonServiceLocator;
using GalaSoft.MvvmLight;
using RumbleJungle.Model;
using System.Collections.ObjectModel;

namespace RumbleJungle.ViewModel
{
    public class StatusBarViewModel : ViewModelBase
    {
        private JungleViewModel jungleViewModel = ServiceLocator.Current.GetInstance<JungleViewModel>();

        public ObservableCollection<BeastViewModel> Beasts { get; set; }

        public ObservableCollection<ItemsViewModel> Items { get; set; }

        public ObservableCollection<WeaponViewModel> Weapons { get; set; }

        public TreasureViewModel Treasure { get; set; }

        public int RamblerHealth => jungleViewModel.RamblerHealth;

        public StatusBarViewModel()
        {
            Weapons = new ObservableCollection<WeaponViewModel>();
            Weapons.Add(new WeaponViewModel(WeaponTypes.Dagger));
            Weapons.Add(new WeaponViewModel(WeaponTypes.Torch));
            Weapons.Add(new WeaponViewModel(WeaponTypes.Spear));
            Weapons.Add(new WeaponViewModel(WeaponTypes.Machete));
            Weapons.Add(new WeaponViewModel(WeaponTypes.Bow));
            Weapons.Add(new WeaponViewModel(WeaponTypes.Battleaxe));
            Weapons.Add(new WeaponViewModel(WeaponTypes.Sword));

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

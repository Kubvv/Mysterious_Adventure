namespace RambleJungle.ViewModel
{
    using System.Windows;
    using RambleJungle.Base;

    public static class ShapesHelper
    {
        public static FrameworkElement GetShape(JungleObject jungleItem) => jungleItem.BackingObject is Weapon w
            ? NewWeaponShape(w.WeaponType)
            : NewJungleShape(jungleItem.BackingObject is JungleObject o ? o.JungleObjectType : jungleItem.JungleObjectType);

        public static FrameworkElement GetShape(JungleObjectType type) => NewJungleShape(type);

        public static FrameworkElement GetShape(WeaponType type) => NewWeaponShape(type);

        private static FrameworkElement NewJungleShape(JungleObjectType jungleObjectType)
        {
            FrameworkElement result = jungleObjectType switch
            {
                JungleObjectType.Rambler => Config.SuperRambler ? new Images.SuperRambler() : new Images.Rambler(),
                JungleObjectType.DragonflySwarm => new Images.DragonflySwarm(),
                JungleObjectType.WildPig => new Images.WildPig(),
                JungleObjectType.Snakes => new Images.Snakes(),
                JungleObjectType.CarnivorousPlant => new Images.CarnivorousPlant(),
                JungleObjectType.Minotaur => new Images.Minotaur(),
                JungleObjectType.Hydra => new Images.Hydra(),
                JungleObjectType.LostWeapon => new Images.LostWeapon(),
                JungleObjectType.Elixir => new Images.Elixir(),
                JungleObjectType.Map => new Images.Map(),
                JungleObjectType.Radar => new Images.Radar(),
                JungleObjectType.MagnifyingGlass => new Images.MagnifyingGlass(),
                JungleObjectType.Talisman => new Images.Talisman(),
                JungleObjectType.Natives => new Images.Natives(),
                JungleObjectType.Quicksand => new Images.QuickSand(),
                JungleObjectType.Trap => new Images.Trap(),
                JungleObjectType.Treasure => new Images.Treasure(),
                JungleObjectType.Camp => new Images.Camp(),
                JungleObjectType.Tent => new Images.Tent(),
                JungleObjectType.ForgottenCity => new Images.ForgottenCity(),
                JungleObjectType.DenseJungle => new Images.DenseJungle(),
                _ => new Images.EmptyField(),
            };
            return result;
        }

        private static FrameworkElement NewWeaponShape(WeaponType weaponType)
        {
            FrameworkElement result = weaponType switch
            {
                WeaponType.Dagger => new Images.Dagger(),
                WeaponType.Torch => new Images.Torch(),
                WeaponType.Spear => new Images.Spear(),
                WeaponType.Machete => new Images.Machete(),
                WeaponType.Bow => new Images.Bow(),
                WeaponType.Battleaxe => new Images.BattleAxe(),
                WeaponType.Sword => new Images.Sword(),
                _ => new Images.EmptyField(),
            };
            return result;
        }
    }
}

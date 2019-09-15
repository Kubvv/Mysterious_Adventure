namespace RumbleJungle.Model
{
    public enum JungleObjectTypes
    {
        Rambler,

        // beasts
        DragonflySwarm,
        WildPig,
        Snakes,
        CarnivorousPlant,
        Minotaur,
        Hydra,

        // hidden items
        LostWeapon,
        Elixir,
        Map,
        Compass,
        MagnifyingGlass,
        Talisman,
        Natives,
        Quicksand,
        Trap,
        Treasure,
        EmptyField,

        // visible items
        Camp,
        Tent,
        ForgottenCity,
        DenseJungle
    }

    public enum WeaponTypes
    {
        Dagger,
        Torch,
        Spear,
        Machete,
        Bow,
        Battleaxe,
        Sword
    }

    public enum Statuses
    {
        Hidden,
        Shown,
        Visible,
        Visited,
        Pointed,
        Marked
    }
}

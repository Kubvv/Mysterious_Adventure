using System;

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

    [Flags]
    public enum Statuses
    {
        Hidden = 1,
        Shown = 2,
        Visible = 4,
        Visited = 8,
        Pointed = 16,
        Marked = 32,
        NotVisited = Hidden | Shown | Visible | Pointed | Marked
    }

    public enum CampBonuses
    {
        Strenght,
        Health,
        Adjacency,
        DoubleAttack
    }
}

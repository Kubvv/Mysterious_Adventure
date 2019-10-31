using System;

namespace RumbleJungle.Model
{
    public enum JungleObjectType
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

    public enum WeaponType
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming",
        "CA1717:Tylko wyliczenia z atrybutem FlagsAttribute powinny mieć nazwy w liczbie mnogiej",
        Justification = "Mylnie zidentyfikowana liczba mnoga")]
    public enum CampBonus
    {
        Strenght,
        Health,
        Adjacency,
        DoubleAttack
    }
}

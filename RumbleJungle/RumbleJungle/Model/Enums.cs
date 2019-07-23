namespace RumbleJungle.Model
{
    public enum JungleObjectTypes
    {
        Rambler = 0x00,
        EmptyField = 0x01,
        DenseJungle = 0x02,
        Treasure = 0x03,

        // beasts
        DragonflySwarm = 0x11,
        WildPig = 0x12,
        Snakes = 0x13,
        CarnivorousPlant = 0x14,
        Minotaur = 0x15,
        Hydra = 0x16,

        // items
        Camp = 0x21,
        Tent = 0x22,
        ForgottenCity = 0x23,
        LostWeapon = 0x24,
        Elixir = 0x25,
        Map = 0x26,
        Compass = 0x27,
        MagnifyingGlass = 0x28,
        Talisman = 0x29,
        Natives = 0x2A,
        Quicksand = 0x2B,
        Trap = 0x2C
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
}

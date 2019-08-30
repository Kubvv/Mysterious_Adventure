using System;
using System.Collections.Generic;
using System.Configuration;

namespace RumbleJungle.Model
{
    public static class Configuration
    {
        private static readonly int defaultJungleHeight = 10, defaultJungleWidth = 16;
        private static readonly Dictionary<JungleObjectTypes, int> defaultJungleObjectsCount = new Dictionary<JungleObjectTypes, int>
        {
            [JungleObjectTypes.DragonflySwarm] = 5,
            [JungleObjectTypes.WildPig] = 5,
            [JungleObjectTypes.Snakes] = 5,
            [JungleObjectTypes.CarnivorousPlant] = 5,
            [JungleObjectTypes.Hydra] = 5,
            [JungleObjectTypes.Minotaur] = 5,
            [JungleObjectTypes.ForgottenCity] = 2,
            [JungleObjectTypes.LostWeapon] = 3,
            [JungleObjectTypes.Elixir] = 3,
            [JungleObjectTypes.Map] = 3,
            [JungleObjectTypes.Compass] = 3,
            [JungleObjectTypes.MagnifyingGlass] = 3,
            [JungleObjectTypes.Talisman] = 2,
            [JungleObjectTypes.Natives] = 2,
            [JungleObjectTypes.Quicksand] = 2,
            [JungleObjectTypes.Treasure] = 10,
            [JungleObjectTypes.Trap] = 2,
            [JungleObjectTypes.Camp] = 2,
            [JungleObjectTypes.Tent] = 6,
            [JungleObjectTypes.DenseJungle] = (int)Math.Round(defaultJungleHeight * defaultJungleWidth * 0.1)
        };

        public static Random Random { get; } = new Random();
        public static bool DebugMode { get; } = true;
        public static int JungleHeight { get; private set; }
        public static int JungleWidth { get; private set; }
        public static Dictionary<JungleObjectTypes, int> JungleObjectsCount { get; private set; }
        public static List<JungleObjectTypes> Beasts { get; } = new List<JungleObjectTypes>() {
            JungleObjectTypes.DragonflySwarm,
            JungleObjectTypes.WildPig,
            JungleObjectTypes.Snakes,
            JungleObjectTypes.CarnivorousPlant,
            JungleObjectTypes.Minotaur,
            JungleObjectTypes.Hydra
        };
        public static List<JungleObjectTypes> HiddenItems { get; } = new List<JungleObjectTypes>() {
            JungleObjectTypes.LostWeapon,
            JungleObjectTypes.Elixir,
            JungleObjectTypes.Map,
            JungleObjectTypes.Compass,
            JungleObjectTypes.MagnifyingGlass,
            JungleObjectTypes.Talisman,
            JungleObjectTypes.Natives,
            JungleObjectTypes.Quicksand,
            JungleObjectTypes.Trap
        };
        public static List<JungleObjectTypes> VisibleItems { get; } = new List<JungleObjectTypes>() {
            JungleObjectTypes.Camp,
            JungleObjectTypes.Tent,
            JungleObjectTypes.ForgottenCity,
            JungleObjectTypes.DenseJungle,
        };
        public static List<JungleObjectTypes> BadItems { get; } = new List<JungleObjectTypes>() {
            JungleObjectTypes.Natives,
            JungleObjectTypes.Trap,
            JungleObjectTypes.Quicksand
        };
        public static List<JungleObjectTypes> GoodItems { get; } = new List<JungleObjectTypes>() {
            JungleObjectTypes.LostWeapon,
            JungleObjectTypes.Elixir,
            JungleObjectTypes.Map,
            JungleObjectTypes.Compass,
            JungleObjectTypes.MagnifyingGlass,
            JungleObjectTypes.Talisman,
        };
        public static Dictionary<JungleObjectTypes, BaseDev> BeastsInitialHealth { get; } = new Dictionary<JungleObjectTypes, BaseDev>
        {
            [JungleObjectTypes.DragonflySwarm] = new BaseDev(35, 5),
            [JungleObjectTypes.WildPig] = new BaseDev(40, 5),
            [JungleObjectTypes.Snakes] = new BaseDev(50, 5),
            [JungleObjectTypes.CarnivorousPlant] = new BaseDev(60, 5),
            [JungleObjectTypes.Minotaur] = new BaseDev(75, 5),
            [JungleObjectTypes.Hydra] = new BaseDev(80, 5)
        };
        public static Dictionary<JungleObjectTypes, BaseDev> BeastStrenght { get; } = new Dictionary<JungleObjectTypes, BaseDev>
        {
            [JungleObjectTypes.DragonflySwarm] = new BaseDev(6, 2),
            [JungleObjectTypes.WildPig] = new BaseDev(11, 3),
            [JungleObjectTypes.Snakes] = new BaseDev(14, 1),
            [JungleObjectTypes.CarnivorousPlant] = new BaseDev(15, 3),
            [JungleObjectTypes.Minotaur] = new BaseDev(18, 6),
            [JungleObjectTypes.Hydra] = new BaseDev(22, 7)
        };
        public static Dictionary<Tuple<WeaponTypes, JungleObjectTypes>, BaseDev> WeaponStrenght { get; } = new Dictionary<Tuple<WeaponTypes, JungleObjectTypes>, BaseDev>
        {
            // TODO: weapon strength values
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Dagger, JungleObjectTypes.DragonflySwarm)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Dagger, JungleObjectTypes.WildPig)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Dagger, JungleObjectTypes.Snakes)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Dagger, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Dagger, JungleObjectTypes.Minotaur)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Dagger, JungleObjectTypes.Hydra)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Torch, JungleObjectTypes.DragonflySwarm)] = new BaseDev(33, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Torch, JungleObjectTypes.WildPig)] = new BaseDev(18, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Torch, JungleObjectTypes.Snakes)] = new BaseDev(16, 2),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Torch, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(25, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Torch, JungleObjectTypes.Minotaur)] = new BaseDev(10, 2),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Torch, JungleObjectTypes.Hydra)] = new BaseDev(10, 2),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Spear, JungleObjectTypes.DragonflySwarm)] = new BaseDev(11, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Spear, JungleObjectTypes.WildPig)] = new BaseDev(35, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Spear, JungleObjectTypes.Snakes)] = new BaseDev(20, 4),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Spear, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(27, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Spear, JungleObjectTypes.Minotaur)] = new BaseDev(15, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Spear, JungleObjectTypes.Hydra)] = new BaseDev(17, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Machete, JungleObjectTypes.DragonflySwarm)] = new BaseDev(13, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Machete, JungleObjectTypes.WildPig)] = new BaseDev(22, 4),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Machete, JungleObjectTypes.Snakes)] = new BaseDev(40, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Machete, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(22, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Machete, JungleObjectTypes.Minotaur)] = new BaseDev(16, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Machete, JungleObjectTypes.Hydra)] = new BaseDev(18, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Bow, JungleObjectTypes.DragonflySwarm)] = new BaseDev(7, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Bow, JungleObjectTypes.WildPig)] = new BaseDev(15, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Bow, JungleObjectTypes.Snakes)] = new BaseDev(15, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Bow, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(49, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Bow, JungleObjectTypes.Minotaur)] = new BaseDev(30, 6),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Bow, JungleObjectTypes.Hydra)] = new BaseDev(33, 7),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Battleaxe, JungleObjectTypes.DragonflySwarm)] = new BaseDev(20, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Battleaxe, JungleObjectTypes.WildPig)] = new BaseDev(22, 4),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Battleaxe, JungleObjectTypes.Snakes)] = new BaseDev(27, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Battleaxe, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(27, 5),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Battleaxe, JungleObjectTypes.Minotaur)] = new BaseDev(55, 6),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Battleaxe, JungleObjectTypes.Hydra)] = new BaseDev(40, 3),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Sword, JungleObjectTypes.DragonflySwarm)] = new BaseDev(20, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Sword, JungleObjectTypes.WildPig)] = new BaseDev(24, 4),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Sword, JungleObjectTypes.Snakes)] = new BaseDev(30, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Sword, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(25, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Sword, JungleObjectTypes.Minotaur)] = new BaseDev(48, 1),
            [new Tuple<WeaponTypes, JungleObjectTypes>(WeaponTypes.Sword, JungleObjectTypes.Hydra)] = new BaseDev(57, 3)
        };

        public static void Read()
        {
            JungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings["JungleWidth"]);
            JungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings["JungleHeight"]);

            double factor = (double) (JungleHeight * JungleWidth) / (defaultJungleHeight * defaultJungleWidth);

            JungleObjectsCount = new Dictionary<JungleObjectTypes, int>();
            foreach (KeyValuePair<JungleObjectTypes, int> jungleObjectsCount in defaultJungleObjectsCount)
            {
                JungleObjectsCount.Add(jungleObjectsCount.Key, (int) Math.Floor(jungleObjectsCount.Value * factor));
            }
        }
    }
}

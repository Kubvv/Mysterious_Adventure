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
            [JungleObjectTypes.LostWeapon] = 4,
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

        public static Random Random = new Random();
        public static bool DebugMode = false;
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
            [JungleObjectTypes.Minotaur] = new BaseDev(18, 8),
            [JungleObjectTypes.Hydra] = new BaseDev(25, 7)
        };
        public static Dictionary<WeaponToBeast, BaseDev> WeaponStrenght { get; } = new Dictionary<WeaponToBeast, BaseDev>
        {
            [new WeaponToBeast(WeaponTypes.Dagger, JungleObjectTypes.DragonflySwarm)] = new BaseDev(5, 2),
            [new WeaponToBeast(WeaponTypes.Dagger, JungleObjectTypes.WildPig)] = new BaseDev(5, 2),
            [new WeaponToBeast(WeaponTypes.Dagger, JungleObjectTypes.Snakes)] = new BaseDev(5, 2),
            [new WeaponToBeast(WeaponTypes.Dagger, JungleObjectTypes.CarnivorousPlant)] = new BaseDev(5, 2),
            [new WeaponToBeast(WeaponTypes.Dagger, JungleObjectTypes.Minotaur)] = new BaseDev(5, 2),
            [new WeaponToBeast(WeaponTypes.Dagger, JungleObjectTypes.Hydra)] = new BaseDev(5, 2)
            // TODO: other weapon strength on every beast
        };

        public static void Read()
        {
            JungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings["JungleWidth"]);
            JungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings["JungleHeight"]);

            double factor = (double) (JungleHeight * JungleWidth) / (double) (defaultJungleHeight * defaultJungleWidth);

            JungleObjectsCount = new Dictionary<JungleObjectTypes, int>();
            foreach (KeyValuePair<JungleObjectTypes, int> jungleObjectsCount in defaultJungleObjectsCount)
            {
                JungleObjectsCount.Add(jungleObjectsCount.Key, (int) Math.Round(jungleObjectsCount.Value * factor));
            }
        }
    }
}

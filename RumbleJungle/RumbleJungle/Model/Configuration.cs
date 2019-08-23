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

        public static Dictionary<JungleObjectTypes, int> BeastsInitialHealth { get; private set; } = new Dictionary<JungleObjectTypes, int>
        {
            [JungleObjectTypes.DragonflySwarm] = 35,
            [JungleObjectTypes.WildPig] = 40,
            [JungleObjectTypes.Snakes] = 50,
            [JungleObjectTypes.CarnivorousPlant] = 60,
            [JungleObjectTypes.Minotaur] = 75,
            [JungleObjectTypes.Hydra] = 80
        };

        public static Dictionary<JungleObjectTypes, BaseDev> BeastStrenght { get; private set; } = new Dictionary<JungleObjectTypes, BaseDev>
        {
            [JungleObjectTypes.DragonflySwarm] = new BaseDev { BaseValue = 5, Deviation = 2 },
            [JungleObjectTypes.WildPig] = new BaseDev { BaseValue = 5, Deviation = 2 },
            [JungleObjectTypes.Snakes] = new BaseDev { BaseValue = 5, Deviation = 2 },
            [JungleObjectTypes.CarnivorousPlant] = new BaseDev { BaseValue = 5, Deviation = 2 },
            [JungleObjectTypes.Minotaur] = new BaseDev { BaseValue = 5, Deviation = 2 },
            [JungleObjectTypes.Hydra] = new BaseDev { BaseValue = 5, Deviation = 2 }
        };
        public static bool DebugMode = true;
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

        public static void Read()
        {
            JungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings["JungleHeight"]);
            JungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings["JungleWidth"]);

            double factor = (JungleHeight * JungleWidth) / (defaultJungleHeight * defaultJungleWidth);

            JungleObjectsCount = new Dictionary<JungleObjectTypes, int>();
            foreach (KeyValuePair<JungleObjectTypes, int> jungleObjectsCount in defaultJungleObjectsCount)
            {
                JungleObjectsCount.Add(jungleObjectsCount.Key, (int) Math.Round(jungleObjectsCount.Value * factor));
            }
        }
    }

    public class BaseDev
    {
        public int BaseValue = 0;
        public int Deviation = 0;
    }
}

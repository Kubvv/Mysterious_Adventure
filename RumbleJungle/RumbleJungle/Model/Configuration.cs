using System;
using System.Collections.Generic;
using System.Configuration;

namespace RumbleJungle.Model
{
    public static class Configuration
    {
        private static int defaultJungleHeight = 10, defaultJungleWidth = 16;
        private static Dictionary<JungleObjectTypes, int> defaultJungleObjectsCount = new Dictionary<JungleObjectTypes, int>
        {
            [JungleObjectTypes.Camp] = 2,
            [JungleObjectTypes.Tent] = 6,
            [JungleObjectTypes.ForgottenCity] = 2,
            [JungleObjectTypes.DenseJungle] = (int)Math.Round(defaultJungleHeight * defaultJungleWidth * 0.1),
            [JungleObjectTypes.Treasure] = 10,
            [JungleObjectTypes.CarnivorousPlant] = 5,
            [JungleObjectTypes.DragonflySwarm] = 5,
            [JungleObjectTypes.Hydra] = 5,
            [JungleObjectTypes.Minotaur] = 5,
            [JungleObjectTypes.Snakes] = 5,
            [JungleObjectTypes.WildPig] = 5,
            [JungleObjectTypes.Natives] = 2,
            [JungleObjectTypes.Quicksand] = 2,
            [JungleObjectTypes.Trap] = 2,
            [JungleObjectTypes.Compass] = 3,
            [JungleObjectTypes.Elixir] = 3,
            [JungleObjectTypes.LostWeapon] = 4,
            [JungleObjectTypes.MagnifyingGlass] = 3,
            [JungleObjectTypes.Map] = 3,
            [JungleObjectTypes.Talisman] = 2
        };

        public static int JungleHeight { get; private set; }
        public static int JungleWidth { get; private set; }

        public static Dictionary<JungleObjectTypes, int> JungleObjectsCount { get; private set; }

        public static void Read()
        {
            JungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings["JungleHeight"]);
            JungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings["JungleWidth"]);

            double factor = (JungleHeight * JungleWidth) / (defaultJungleHeight * defaultJungleWidth);

            JungleObjectsCount = new Dictionary<JungleObjectTypes, int>();
            foreach (KeyValuePair<JungleObjectTypes, int> objectCount in defaultJungleObjectsCount)
            {
                JungleObjectsCount.Add(objectCount.Key, (int) Math.Round(objectCount.Value * factor));
            }
        }
    }
}

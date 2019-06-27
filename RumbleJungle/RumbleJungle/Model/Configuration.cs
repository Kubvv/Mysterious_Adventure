using System;
using System.Collections.Generic;
using System.Configuration;

namespace RumbleJungle.Model
{
    public static class Configuration
    {
        public static Dictionary<JungleObjectTypes, int> JungleObjectsCount = new Dictionary<JungleObjectTypes, int>
        {
            [JungleObjectTypes.Camp] = 2,
            [JungleObjectTypes.Tent] = 6,
            [JungleObjectTypes.ForgottenCity] = 2,
            [JungleObjectTypes.DenseJungle] = (int)Math.Round(JungleHeight * JungleWidth * 0.1),
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

        public static int JungleHeight = 10;
        public static int JungleWidth = 16;

        public static void ReadConfiguration()
        {
            JungleObjectsCount.Add(JungleObjectTypes.Camp, 2);

            int jungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings["JungleHeight"]);
            int jungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings["JungleWidth"]);

            double factor = (jungleHeight * jungleWidth) / (JungleHeight * JungleWidth);

            foreach (KeyValuePair<JungleObjectTypes, int> objectCount in JungleObjectsCount)
            {
                JungleObjectsCount[objectCount.Key] = (int) Math.Round(objectCount.Value * factor);
            }
        }
    }
}

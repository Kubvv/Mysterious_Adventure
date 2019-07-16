using RumbleJungle.Properties;
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

        private static Dictionary<JungleObjectTypes, string> jungleObjectsShape = new Dictionary<JungleObjectTypes, string>
        {
            [JungleObjectTypes.Camp] = "Camp",
            [JungleObjectTypes.Tent] = "",
            [JungleObjectTypes.ForgottenCity] = "Temple",
            [JungleObjectTypes.Treasure] = "",
            [JungleObjectTypes.CarnivorousPlant] = "CarnivorousPlant",
            [JungleObjectTypes.DragonflySwarm] = "Dragonfly",
            [JungleObjectTypes.Hydra] = "Hydra",
            [JungleObjectTypes.Minotaur] = "Minotaur",
            [JungleObjectTypes.Snakes] = "Snake",
            [JungleObjectTypes.WildPig] = "Boar",
            [JungleObjectTypes.Natives] = "Natives",
            [JungleObjectTypes.Quicksand] = "Quicksand",
            [JungleObjectTypes.Trap] = "Trap",
            [JungleObjectTypes.Compass] = "Compass",
            [JungleObjectTypes.Elixir] = "Elixir",
            [JungleObjectTypes.LostWeapon] = "",
            [JungleObjectTypes.MagnifyingGlass] = "MagnifyingGlass",
            [JungleObjectTypes.Map] = "Map",
            [JungleObjectTypes.Talisman] = "Talisman"
        };

        private static Dictionary<WeaponTypes, string> weaponShape = new Dictionary<WeaponTypes, string>
        {
            [WeaponTypes.Dagger] = "Dagger",
            [WeaponTypes.Torch] = "Torch",
            [WeaponTypes.Spear] = "Spear",
            [WeaponTypes.Machete] = "Machete",
            [WeaponTypes.Bow] = "Bow",
            [WeaponTypes.Battleaxe] = "Axe",
            [WeaponTypes.Sword] = "Sword"
        };

        public static int JungleHeight { get; private set; }
        public static int JungleWidth { get; private set; }
        public static int TreasureCount { get; private set; }

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

            TreasureCount = (int)(Math.Round(10 * factor));
        }

        internal static string ShapeOf(JungleObjectTypes jungleObjectType)
        {
            string result = "";
            if (jungleObjectsShape.ContainsKey(jungleObjectType))
            {
                result = jungleObjectsShape[jungleObjectType];
            }
            return string.IsNullOrEmpty(result) ? "Axe" : result;
        }

        internal static string ShapeOf(WeaponTypes weaponType)
        {
            string result = "";
            if (weaponShape.ContainsKey(weaponType))
            {
                result = weaponShape[weaponType];
            }
            return string.IsNullOrEmpty(result) ? "Axe" : result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace RambleJungle.Model
{
    public static class Config
    {
        private const int defaultJungleHeight = 10, defaultJungleWidth = 16;
        private static readonly Dictionary<JungleObjectType, int> defaultJungleObjectsCount = new()
        {
            [JungleObjectType.DragonflySwarm] = 5,
            [JungleObjectType.WildPig] = 5,
            [JungleObjectType.Snakes] = 5,
            [JungleObjectType.CarnivorousPlant] = 5,
            [JungleObjectType.Hydra] = 5,
            [JungleObjectType.Minotaur] = 5,
            [JungleObjectType.ForgottenCity] = 2,
            [JungleObjectType.LostWeapon] = 3,
            [JungleObjectType.Elixir] = 3,
            [JungleObjectType.Map] = 4,
            [JungleObjectType.Radar] = 4,
            [JungleObjectType.MagnifyingGlass] = 4,
            [JungleObjectType.Talisman] = 2,
            [JungleObjectType.Natives] = 2,
            [JungleObjectType.Quicksand] = 2,
            [JungleObjectType.Treasure] = 10,
            [JungleObjectType.Trap] = 2,
            [JungleObjectType.Camp] = 2,
            [JungleObjectType.Tent] = 6,
            [JungleObjectType.DenseJungle] = (int)Math.Round(defaultJungleHeight * defaultJungleWidth * 0.1)
        };

        public static List<JungleObjectType> Beasts { get; } = new List<JungleObjectType>() {
            JungleObjectType.DragonflySwarm,
            JungleObjectType.WildPig,
            JungleObjectType.Snakes,
            JungleObjectType.CarnivorousPlant,
            JungleObjectType.Minotaur,
            JungleObjectType.Hydra
        };
        public static List<JungleObjectType> HiddenItems { get; } = new List<JungleObjectType>() {
            JungleObjectType.LostWeapon,
            JungleObjectType.Elixir,
            JungleObjectType.Map,
            JungleObjectType.Radar,
            JungleObjectType.MagnifyingGlass,
            JungleObjectType.Talisman,
            JungleObjectType.Natives,
            JungleObjectType.Quicksand,
            JungleObjectType.Trap
        };
        public static List<JungleObjectType> VisibleItems { get; } = new List<JungleObjectType>() {
            JungleObjectType.ForgottenCity,
            JungleObjectType.DenseJungle,
            JungleObjectType.Camp,
            JungleObjectType.Tent
        };
        public static List<JungleObjectType> BadItems { get; } = new List<JungleObjectType>() {
            JungleObjectType.Natives,
            JungleObjectType.Trap,
            JungleObjectType.Quicksand
        };
        public static List<JungleObjectType> GoodItems { get; } = new List<JungleObjectType>() {
            JungleObjectType.LostWeapon,
            JungleObjectType.Elixir,
            JungleObjectType.Map,
            JungleObjectType.Radar,
            JungleObjectType.MagnifyingGlass,
            JungleObjectType.Talisman
        };

        public static List<JungleObjectType> Arsenals { get; } = new List<JungleObjectType>() {
            JungleObjectType.Tent
        };
        public static Dictionary<JungleObjectType, BaseDev> BeastsInitialHealth { get; } = new Dictionary<JungleObjectType, BaseDev>
        {
            [JungleObjectType.DragonflySwarm] = new BaseDev(35, 5),
            [JungleObjectType.WildPig] = new BaseDev(40, 5),
            [JungleObjectType.Snakes] = new BaseDev(50, 5),
            [JungleObjectType.CarnivorousPlant] = new BaseDev(60, 5),
            [JungleObjectType.Minotaur] = new BaseDev(75, 5),
            [JungleObjectType.Hydra] = new BaseDev(80, 5)
        };
        public static Dictionary<JungleObjectType, BaseDev> BeastStrenght { get; } = new Dictionary<JungleObjectType, BaseDev>
        {
            [JungleObjectType.DragonflySwarm] = new BaseDev(6, 2),
            [JungleObjectType.WildPig] = new BaseDev(11, 3),
            [JungleObjectType.Snakes] = new BaseDev(14, 1),
            [JungleObjectType.CarnivorousPlant] = new BaseDev(15, 3),
            [JungleObjectType.Minotaur] = new BaseDev(18, 6),
            [JungleObjectType.Hydra] = new BaseDev(22, 7)
        };
        public static Dictionary<Tuple<WeaponType, JungleObjectType>, BaseDev> WeaponStrenght { get; } = new Dictionary<Tuple<WeaponType, JungleObjectType>, BaseDev>
        {
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Dagger, JungleObjectType.DragonflySwarm)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Dagger, JungleObjectType.WildPig)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Dagger, JungleObjectType.Snakes)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Dagger, JungleObjectType.CarnivorousPlant)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Dagger, JungleObjectType.Minotaur)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Dagger, JungleObjectType.Hydra)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Torch, JungleObjectType.DragonflySwarm)] = new BaseDev(33, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Torch, JungleObjectType.WildPig)] = new BaseDev(18, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Torch, JungleObjectType.Snakes)] = new BaseDev(16, 2),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Torch, JungleObjectType.CarnivorousPlant)] = new BaseDev(25, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Torch, JungleObjectType.Minotaur)] = new BaseDev(10, 2),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Torch, JungleObjectType.Hydra)] = new BaseDev(10, 2),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Spear, JungleObjectType.DragonflySwarm)] = new BaseDev(11, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Spear, JungleObjectType.WildPig)] = new BaseDev(35, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Spear, JungleObjectType.Snakes)] = new BaseDev(20, 4),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Spear, JungleObjectType.CarnivorousPlant)] = new BaseDev(27, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Spear, JungleObjectType.Minotaur)] = new BaseDev(15, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Spear, JungleObjectType.Hydra)] = new BaseDev(17, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Machete, JungleObjectType.DragonflySwarm)] = new BaseDev(13, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Machete, JungleObjectType.WildPig)] = new BaseDev(22, 4),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Machete, JungleObjectType.Snakes)] = new BaseDev(40, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Machete, JungleObjectType.CarnivorousPlant)] = new BaseDev(22, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Machete, JungleObjectType.Minotaur)] = new BaseDev(16, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Machete, JungleObjectType.Hydra)] = new BaseDev(18, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Bow, JungleObjectType.DragonflySwarm)] = new BaseDev(7, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Bow, JungleObjectType.WildPig)] = new BaseDev(15, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Bow, JungleObjectType.Snakes)] = new BaseDev(15, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Bow, JungleObjectType.CarnivorousPlant)] = new BaseDev(49, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Bow, JungleObjectType.Minotaur)] = new BaseDev(30, 6),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Bow, JungleObjectType.Hydra)] = new BaseDev(33, 7),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Battleaxe, JungleObjectType.DragonflySwarm)] = new BaseDev(20, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Battleaxe, JungleObjectType.WildPig)] = new BaseDev(22, 4),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Battleaxe, JungleObjectType.Snakes)] = new BaseDev(27, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Battleaxe, JungleObjectType.CarnivorousPlant)] = new BaseDev(27, 5),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Battleaxe, JungleObjectType.Minotaur)] = new BaseDev(55, 6),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Battleaxe, JungleObjectType.Hydra)] = new BaseDev(40, 3),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Sword, JungleObjectType.DragonflySwarm)] = new BaseDev(20, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Sword, JungleObjectType.WildPig)] = new BaseDev(24, 4),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Sword, JungleObjectType.Snakes)] = new BaseDev(30, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Sword, JungleObjectType.CarnivorousPlant)] = new BaseDev(25, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Sword, JungleObjectType.Minotaur)] = new BaseDev(48, 1),
            [new Tuple<WeaponType, JungleObjectType>(WeaponType.Sword, JungleObjectType.Hydra)] = new BaseDev(57, 3)
        };

        public static Random Random { get; } = new Random();
        public static bool DebugMode { get; } = false;

        public const int MINJUNGLEWIDTH = 4, MAXJUNGLEWIDTH = 50, MINJUNGLEHEIGHT = 4, MAXJUNGLEHEIGHT = 50;
        public const int JUNGLEARSENALWEAPONCOUNT = 3;

        private const string JUNGLEWIDTH = "JungleWidth";
        public static int JungleHeight { get; private set; }

        private const string JUNGLEHEIGHT = "JungleHeight";
        public static int JungleWidth { get; private set; }

        private const string KEEPRATIO = "KeepRatio";
        public static bool KeepRatio { get; private set; } = true;

        private const string SUPERRAMBLER = "SuperRambler";
        public static bool SuperRambler { get; private set; } = false;

        private const string PACIFISTRAMBLER = "PacifistRambler";
        public static bool PacifistRambler { get; private set; } = false;

        public static Dictionary<JungleObjectType, int> JungleObjectsCount { get; private set; } = new Dictionary<JungleObjectType, int>();

        public static void Read()
        {
            if (ConfigurationManager.AppSettings[JUNGLEWIDTH] != null)
            {
                JungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings[JUNGLEWIDTH], CultureInfo.CurrentCulture);
            }
            if (ConfigurationManager.AppSettings[JUNGLEHEIGHT] != null)
            {
                JungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings[JUNGLEHEIGHT], CultureInfo.CurrentCulture);
            }
            if (ConfigurationManager.AppSettings[KEEPRATIO] != null)
            {
                KeepRatio = Convert.ToBoolean(ConfigurationManager.AppSettings[KEEPRATIO], CultureInfo.CurrentCulture);
            }
            if (ConfigurationManager.AppSettings[SUPERRAMBLER] != null)
            {
                SuperRambler = Convert.ToBoolean(ConfigurationManager.AppSettings[SUPERRAMBLER], CultureInfo.CurrentCulture);
            }
            if (ConfigurationManager.AppSettings[PACIFISTRAMBLER] != null)
            {
                PacifistRambler = Convert.ToBoolean(ConfigurationManager.AppSettings[PACIFISTRAMBLER], CultureInfo.CurrentCulture);
            }
            RecalculateJungle();
        }

        public static void SetJungleSize(int newWidth, int newHeight, bool save = true)
        {
            if (JungleWidth == newWidth && JungleHeight == newHeight) return;

            JungleWidth = newWidth;
            JungleHeight = newHeight;
            if (save)
            {
                UpdateSetting(JUNGLEWIDTH, JungleWidth.ToString(CultureInfo.CurrentCulture));
                UpdateSetting(JUNGLEHEIGHT, JungleHeight.ToString(CultureInfo.CurrentCulture));
            }
            RecalculateJungle();
        }

        public static void SetKeepRatio(bool newKeepRatio)
        {
            if (KeepRatio == newKeepRatio) return;

            KeepRatio = newKeepRatio;
            UpdateSetting(KEEPRATIO, KeepRatio.ToString(CultureInfo.CurrentCulture));
        }

        public static void SetSuperRambler(bool newSuperRambler)
        {
            if (SuperRambler == newSuperRambler) return;

            SuperRambler = newSuperRambler;
            UpdateSetting(SUPERRAMBLER, SuperRambler.ToString(CultureInfo.CurrentCulture));
        }

        public static void SetPacifistRambler(bool newPacifistRambler)
        {
            if (PacifistRambler == newPacifistRambler) return;

            PacifistRambler = newPacifistRambler;
            UpdateSetting(PACIFISTRAMBLER, PacifistRambler.ToString(CultureInfo.CurrentCulture));
        }

        private static void RecalculateJungle()
        {
            double factor = (double)(JungleHeight * JungleWidth) / (defaultJungleHeight * defaultJungleWidth);

            JungleObjectsCount.Clear();
            foreach (KeyValuePair<JungleObjectType, int> jungleObjectsCount in defaultJungleObjectsCount)
            {
                JungleObjectsCount.Add(jungleObjectsCount.Key, (int)Math.Floor(jungleObjectsCount.Value * factor));
            }
        }

        private static bool UpdateSetting(string key, string value)
        {
            bool result = true;
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                result = false;
            }
            return result;
        }
    }
}

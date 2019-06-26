using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumbleJungle.Model
{
    public static class Configuration
    {
        public static Dictionary<JungleObjects, int> JungleObjectsCount = new Dictionary<JungleObjects, int>
        {
            [JungleObjects.Camp] = 2,
            [JungleObjects.Tent] = 6,
            [JungleObjects.ForgottenCity] = 2,
            [JungleObjects.DenseJungle] = (int)Math.Round(JungleHeight * JungleWidth * 0.1),
            [JungleObjects.Treasure] = 10,
            [JungleObjects.CarnivorousPlant] = 5,
            [JungleObjects.DragonflySwarm] = 5,
            [JungleObjects.Hydra] = 5,
            [JungleObjects.Minotaur] = 5,
            [JungleObjects.Snakes] = 5,
            [JungleObjects.WildPig] = 5,
            [JungleObjects.Natives] = 2,
            [JungleObjects.Quicksand] = 2,
            [JungleObjects.Trap] = 2,
            [JungleObjects.Compass] = 3,
            [JungleObjects.Elixir] = 3,
            [JungleObjects.LostWeapon] = 4,
            [JungleObjects.MagnifyingGlass] = 3,
            [JungleObjects.Map] = 3,
            [JungleObjects.Talisman] = 2
        };

        public static int JungleHeight = 10;
        public static int JungleWidth = 16;


        public static void ReadConfiguration()
        {
            JungleObjectsCount.Add(JungleObjects.Camp, 2);

            int jungleHeight = Convert.ToInt32(ConfigurationManager.AppSettings["JungleHeight"]);
            int jungleWidth = Convert.ToInt32(ConfigurationManager.AppSettings["JungleWidth"]);

            double factor = (jungleHeight * jungleWidth) / (JungleHeight * JungleWidth);

            foreach (KeyValuePair<JungleObjects, int> objectCount in JungleObjectsCount)
            {
                JungleObjectsCount[objectCount.Key] = (int) Math.Round(objectCount.Value * factor);
            }
        }


    }


    public enum JungleObjects
    {
        Camp,
        Tent,
        ForgottenCity,
        DenseJungle,
        Treasure,
        CarnivorousPlant,
        DragonflySwarm,
        Hydra,
        Minotaur,
        Snakes,
        WildPig,
        Natives,
        Quicksand,
        Trap,
        Compass,
        Elixir,
        LostWeapon,
        MagnifyingGlass,
        Map,
        Talisman
    }
}

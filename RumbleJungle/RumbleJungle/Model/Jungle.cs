using RumbleJungle.Model.BeastModel;
using RumbleJungle.Model.ObstacleModel;
using RumbleJungle.Model.UtilityModel;
using System;
using System.Collections.Generic;

namespace RumbleJungle.Model
{
    public class Jungle
    {
        private List<JungleObject> jungleObjects = new List<JungleObject>();
        internal void Generate()
        {
            Random random = new Random();
            int denseJungleCount = random.Next(Configuration.JungleObjectsCount[JungleObjects.DenseJungle]) + 1;
            for (int i = 0; i < denseJungleCount; i++)
            {
                jungleObjects.Add(new DenseJungle());
            }
            for (int i = 0; i < (int) JungleObjects.Camp; i++)
            {
                jungleObjects.Add(new Camp());
            }
            for (int i = 0; i < (int)JungleObjects.Tent; i++)
            {
                jungleObjects.Add(new Tent());
            }
            for (int i = 0; i < (int)JungleObjects.ForgottenCity; i++)
            {
                jungleObjects.Add(new ForgottenCity());
            }
            for (int i = 0; i < (int)JungleObjects.CarnivorousPlant; i++)
            {
                jungleObjects.Add(new CarnivorousPlant());
            }
            for (int i = 0; i < (int)JungleObjects.DragonflySwarm; i++)
            {
                jungleObjects.Add(new DragonflySwarm());
            }
            for (int i = 0; i < (int)JungleObjects.Hydra; i++)
            {
                jungleObjects.Add(new Hydra());
            }
            for (int i = 0; i < (int)JungleObjects.Minotaur; i++)
            {
                jungleObjects.Add(new Minotaur());
            }
            for (int i = 0; i < (int)JungleObjects.Snakes; i++)
            {
                jungleObjects.Add(new Snakes());
            }
            for (int i = 0; i < (int)JungleObjects.WildPig; i++)
            {
                jungleObjects.Add(new WildPig());
            }
            for (int i = 0; i < (int)JungleObjects.Compass; i++)
            {
                jungleObjects.Add(new Compass());
            }
            for (int i = 0; i < (int)JungleObjects.Elixir; i++)
            {
                jungleObjects.Add(new Elixir());
            }
            for (int i = 0; i < (int)JungleObjects.LostWeapon; i++)
            {
                jungleObjects.Add(new LostWeapon());
            }
            for (int i = 0; i < (int)JungleObjects.MagnifyingGlass; i++)
            {
                jungleObjects.Add(new MagnifyingGlass());
            }
            for (int i = 0; i < (int)JungleObjects.Map; i++)
            {
                jungleObjects.Add(new Map());
            }
            for (int i = 0; i < (int)JungleObjects.Talisman; i++)
            {
                jungleObjects.Add(new Talisman());
            }
            for (int i = 0; i < (int)JungleObjects.Natives; i++)
            {
                jungleObjects.Add(new Natives());
            }
            for (int i = 0; i < (int)JungleObjects.Quicksand; i++)
            {
                jungleObjects.Add(new Quicksand());
            }
            for (int i = 0; i < (int)JungleObjects.Trap; i++)
            {
                jungleObjects.Add(new Trap());
            }
        }
    }
}

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
            int denseJungleCount = random.Next(Configuration.JungleObjectsCount[JungleObjectTypes.DenseJungle]) + 1;
            for (int i = 0; i < denseJungleCount; i++)
            {
                jungleObjects.Add(new DenseJungle());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Camp]; i++)
            {
                jungleObjects.Add(new Camp());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Tent]; i++)
            {
                jungleObjects.Add(new Tent());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.ForgottenCity]; i++)
            {
                jungleObjects.Add(new ForgottenCity());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.CarnivorousPlant]; i++)
            {
                jungleObjects.Add(new CarnivorousPlant());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.DragonflySwarm]; i++)
            {
                jungleObjects.Add(new DragonflySwarm());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Hydra]; i++)
            {
                jungleObjects.Add(new Hydra());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Minotaur]; i++)
            {
                jungleObjects.Add(new Minotaur());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Snakes]; i++)
            {
                jungleObjects.Add(new Snakes());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.WildPig]; i++)
            {
                jungleObjects.Add(new WildPig());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Compass]; i++)
            {
                jungleObjects.Add(new Compass());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Elixir]; i++)
            {
                jungleObjects.Add(new Elixir());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.LostWeapon]; i++)
            {
                jungleObjects.Add(new LostWeapon());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.MagnifyingGlass]; i++)
            {
                jungleObjects.Add(new MagnifyingGlass());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Map]; i++)
            {
                jungleObjects.Add(new Map());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Talisman]; i++)
            {
                jungleObjects.Add(new Talisman());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Natives]; i++)
            {
                jungleObjects.Add(new Natives());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Quicksand]; i++)
            {
                jungleObjects.Add(new Quicksand());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Trap]; i++)
            {
                jungleObjects.Add(new Trap());
            }
        }
    }
}

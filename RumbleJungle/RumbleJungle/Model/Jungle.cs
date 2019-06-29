using RumbleJungle.Model.BeastModel;
using RumbleJungle.Model.ObstacleModel;
using RumbleJungle.Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Jungle
    {
        public List<JungleObject> JungleObjects { get; private set; } = new List<JungleObject>();
        internal void Generate()
        {
            Configuration.Read();

            JungleObjects.Add(new Rambler());

            Random random = new Random();
            int denseJungleCount = random.Next(Configuration.JungleObjectsCount[JungleObjectTypes.DenseJungle]) + 1;
            for (int i = 0; i < denseJungleCount; i++)
            {
                JungleObjects.Add(new DenseJungle());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Camp]; i++)
            {
                JungleObjects.Add(new Camp());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Tent]; i++)
            {
                JungleObjects.Add(new Tent());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.ForgottenCity]; i++)
            {
                JungleObjects.Add(new ForgottenCity());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.CarnivorousPlant]; i++)
            {
                JungleObjects.Add(new CarnivorousPlant());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.DragonflySwarm]; i++)
            {
                JungleObjects.Add(new DragonflySwarm());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Hydra]; i++)
            {
                JungleObjects.Add(new Hydra());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Minotaur]; i++)
            {
                JungleObjects.Add(new Minotaur());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Snakes]; i++)
            {
                JungleObjects.Add(new Snakes());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.WildPig]; i++)
            {
                JungleObjects.Add(new WildPig());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Compass]; i++)
            {
                JungleObjects.Add(new Compass());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Elixir]; i++)
            {
                JungleObjects.Add(new Elixir());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.LostWeapon]; i++)
            {
                JungleObjects.Add(new LostWeapon());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.MagnifyingGlass]; i++)
            {
                JungleObjects.Add(new MagnifyingGlass());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Map]; i++)
            {
                JungleObjects.Add(new Map());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Talisman]; i++)
            {
                JungleObjects.Add(new Talisman());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Natives]; i++)
            {
                JungleObjects.Add(new Natives());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Quicksand]; i++)
            {
                JungleObjects.Add(new Quicksand());
            }
            for (int i = 0; i < Configuration.JungleObjectsCount[JungleObjectTypes.Trap]; i++)
            {
                JungleObjects.Add(new Trap());
            }
            for (int i = JungleObjects.Count; i < Configuration.JungleHeight * Configuration.JungleWidth; i++)
            {
                JungleObjects.Add(new EmptyField());
            }

            List<Point> coordinates = new List<Point>();

            for(int row = 0; row < Configuration.JungleHeight; row++)
            {
                for (int col = 0; col < Configuration.JungleWidth; col++)
                {
                    coordinates.Add(new Point(col, row));
                }
            }

            foreach (JungleObject jungleObject in JungleObjects)
            {
                int coordinate = random.Next(coordinates.Count);
                jungleObject.SetCoordinates(coordinates[coordinate]);
                coordinates.RemoveAt(coordinate);
            }
        }

        internal void MoveRambler(Point point)
        {
            JungleObjects[0].SetCoordinates(point);
        }
    }
}

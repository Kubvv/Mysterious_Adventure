using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleModel
    {
        private int emptyFieldsCount;

        public List<JungleObject> Jungle { get; private set; } = new List<JungleObject>();

        /// <summary>
        /// losowe ustawienie obiektów w dżungli
        /// </summary>
        internal void GenerateJungle()
        {
            Configuration.Read();
            Random random = new Random();

            Jungle.Clear();
            // wstawienie wszystkich obiektów
            foreach (JungleObjectTypes jungleObjectType in Enum.GetValues(typeof(JungleObjectTypes)))
            {
                if (jungleObjectType == JungleObjectTypes.DenseJungle)
                {
                    // liczba pól dla gęstej dżungli jest losowa
                    int denseJungleCount = random.Next(Configuration.JungleObjectsCount[JungleObjectTypes.DenseJungle]) + 1;
                    for (int i = 0; i < denseJungleCount; i++)
                    {
                        Jungle.Add(new JungleObject(JungleObjectTypes.DenseJungle));
                    }
                }
                else if (jungleObjectType == JungleObjectTypes.EmptyField || jungleObjectType == JungleObjectTypes.Rambler)
                {
                    // puste pola i wędrowiec na końcu
                }
                else
                {
                    for (int i = 0; i < Configuration.JungleObjectsCount[jungleObjectType]; i++)
                    {
                        Jungle.Add(new JungleObject(jungleObjectType));
                    }
                }
            }

            // wstawienie pustych pól
            emptyFieldsCount = Configuration.JungleHeight * Configuration.JungleWidth - Jungle.Count;
            for (int i = Jungle.Count; i < Configuration.JungleHeight * Configuration.JungleWidth; i++)
            {
                Jungle.Add(new JungleObject(JungleObjectTypes.EmptyField));
            }

            // wygenerowanie wszystkich możliwych pozycji
            List<Point> coordinates = new List<Point>();
            for(int row = 0; row < Configuration.JungleHeight; row++)
            {
                for (int col = 0; col < Configuration.JungleWidth; col++)
                {
                    coordinates.Add(new Point(col, row));
                }
            }

            // wylosowanie pozycji każdego obiektu w dżungli
            foreach (JungleObject jungleObject in Jungle)
            {
                int coordinate = random.Next(coordinates.Count);
                jungleObject.SetCoordinates(coordinates[coordinate]);
                coordinates.RemoveAt(coordinate);
            }
        }

        internal JungleObject GetJungleObjectAt(Point point)
        {
            return Jungle.First(jo => jo.Coordinates.X == point.X && jo.Coordinates.Y == point.Y);
        }

        /// <summary>
        /// losowe ustawienie wędrowca w dżungli
        /// </summary>
        /// <param name="rambler">wędrowiec</param>
        internal void ReleaseRambler(Rambler rambler)
        {
            Random random = new Random();
            int ramblerPosition = random.Next(emptyFieldsCount) + 1;
            JungleObject ramblerJungleObject = null;
            foreach (JungleObject jungleObject in Jungle)
            {
                if (jungleObject.JungleObjectType == JungleObjectTypes.EmptyField)
                {
                    ramblerPosition--;
                    if (ramblerPosition == 0)
                    {
                        ramblerJungleObject = jungleObject;
                        break;
                    }
                }
            }
            rambler.SetCoordinates(ramblerJungleObject.Coordinates);
        }

        internal int CountOf(JungleObjectTypes jungleObjectType)
        {
            return Jungle.Count(jo => jo.JungleObjectType == jungleObjectType);
        }

        public List<JungleObject> GetJungleObjects(List<JungleObjectTypes> jungleObjectTypes)
        {
            List<JungleObject> result = new List<JungleObject>();
            foreach (JungleObjectTypes jungleObjectType in jungleObjectTypes)
            {
                result.Add(Jungle.FirstOrDefault(jo => jo.JungleObjectType == jungleObjectType));
            }
            return result;
        }
    }
}

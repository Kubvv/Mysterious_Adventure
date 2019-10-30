using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleModel
    {
        public List<JungleObject> Jungle { get; private set; } = new List<JungleObject>();

        public JungleModel()
        {
            Configuration.Read();

            // wstawienie wszystkich obiektów
            foreach (JungleObjectTypes jungleObjectType in Enum.GetValues(typeof(JungleObjectTypes)))
            {
                if (jungleObjectType == JungleObjectTypes.DenseJungle)
                {
                    // liczba pól dla gęstej dżungli jest losowa
                    int denseJungleCount = Configuration.Random.Next(Configuration.JungleObjectsCount[JungleObjectTypes.DenseJungle]) + 1;
                    for (int i = 0; i < denseJungleCount; i++)
                    {
                        Jungle.Add(new JungleObject(JungleObjectTypes.DenseJungle));
                    }
                }
                else if (jungleObjectType == JungleObjectTypes.EmptyField || jungleObjectType == JungleObjectTypes.Rambler)
                {
                    // puste pola i wędrowiec na końcu
                }
                else if (Configuration.Beasts.Contains(jungleObjectType))
                {
                    for (int i = 0; i < Configuration.JungleObjectsCount[jungleObjectType]; i++)
                    {
                        Jungle.Add(new Beast(jungleObjectType));
                    }
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
            for (int i = Jungle.Count; i < Configuration.JungleHeight * Configuration.JungleWidth; i++)
            {
                Jungle.Add(new JungleObject(JungleObjectTypes.EmptyField));
            }
        }

        /// <summary>
        /// Puts jungle objects at random positions
        /// </summary>
        public void GenerateJungle()
        {
            // TODO: przebudowa dżungli po wylosowaniu liczby gęstwin
            // TODO: przebudowa dżungli po zmianie wymiarów

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
                jungleObject.Reset();
                int coordinate = Configuration.Random.Next(coordinates.Count);
                jungleObject.SetCoordinates(coordinates[coordinate]);
                coordinates.RemoveAt(coordinate);
            }
        }

        /// <summary>
        /// Finds object of given type nearest to given coordinates
        /// </summary>
        /// <param name="coordinates">coordinates</param>
        /// <param name="jungleObjectType">Jungle object type</param>
        /// <returns>Found object or null</returns>
        internal JungleObject FindNearestTo(Point coordinates, JungleObjectTypes jungleObjectType, Statuses statuses)
        {
            int distance = 1;
            JungleObject jungleObject = null;
            while ((distance < Configuration.JungleHeight || distance < Configuration.JungleWidth) && jungleObject == null) 
            {
                jungleObject = FindObjectInVector(coordinates, -distance, jungleObjectType, statuses, true);
                if (jungleObject == null) jungleObject = FindObjectInVector(coordinates, distance, jungleObjectType, statuses, true);
                if (jungleObject == null) jungleObject = FindObjectInVector(coordinates, -distance, jungleObjectType, statuses, false);
                if (jungleObject == null) jungleObject = FindObjectInVector(coordinates, distance, jungleObjectType, statuses, false);
                distance++;
            }
            return jungleObject;
        }

        internal void MarkHiddenObjects()
        {
            foreach (JungleObject jungleObject in Jungle)
            {
                if (jungleObject.Status == Statuses.Hidden || jungleObject.Status == Statuses.Pointed || Configuration.DebugMode)
                {
                    jungleObject.SetStatus(Statuses.Marked);
                }
            }
        }
        
        /// <summary>
        /// Finds a list of surrounding points within a given distance.
        /// </summary>
        /// <param name="coordinates">Selected center point.</param>
        /// <param name="distance">Distance from the selected point.</param>
        /// <returns>List of Points.</returns>
        internal IEnumerable<Point> FindNeighboursTo(Point coordinates, int distance)
        {
            var neighbours = new List<Point>();
            for (var x = -distance; x <= distance; x++)
            {
                for (var y = -distance; y <= distance; y++)
                {
                    neighbours.Add(new Point(coordinates.X + x, coordinates.Y + y));
                }
            }
            return neighbours;
        }

        /// <summary>
        /// Changes the status of points to pointed.
        /// </summary>
        /// <param name="points">List of points.</param>
        internal void SetPointedAt(List<Point> points)
        {
            points.ForEach(point => SetPointedAt(point, false));
        }

        /// <summary>
        /// Changes the status of a point to pointed.
        /// </summary>
        /// <param name="point">Point to change a status.</param>
        /// <param name="beastOrBadOnly">Change the status only for the beast points.</param>
        internal void SetPointedAt(Point point, bool beastOrBadOnly)
        {
            JungleObject jungleObject = GetJungleObjectAt(point);
            if (jungleObject != null)
            {
                bool canPointField = jungleObject.JungleObjectType != JungleObjectTypes.EmptyField &&
                    (Configuration.Beasts.Contains(jungleObject.JungleObjectType) || Configuration.BadItems.Contains(jungleObject.JungleObjectType) || !beastOrBadOnly);
                if (Configuration.DebugMode)
                {
                    if (canPointField && jungleObject.Status == Statuses.Visible && !Configuration.VisibleItems.Contains(jungleObject.JungleObjectType))
                    {
                        jungleObject.SetStatus(Statuses.Pointed);
                    }
                }
                else if (canPointField && jungleObject.Status == Statuses.Hidden)
                {
                    jungleObject.SetStatus(Statuses.Pointed);
                }
            }
        }

        internal JungleObject GetJungleObjectAt(Point point)
        {
            return Jungle.FirstOrDefault(jo => jo.Coordinates.X == point.X && jo.Coordinates.Y == point.Y);
        }

        /// <summary>
        /// Finds random unvisited jungle object of given type
        /// </summary>
        /// <param name="jungleObjectType">Type of the searched jungle object</param>
        /// <returns>Found object or null</returns>
        public JungleObject GetRandomJungleObject(JungleObjectTypes jungleObjectType)
        {
            JungleObject result = null;
            int jungleObjectCount = CountOf(jungleObjectType);
            if (jungleObjectCount > 0)
            {
                int randomJungleObject = Configuration.Random.Next(jungleObjectCount) + 1;
                int counter = 0;
                foreach (JungleObject jungleObject in Jungle)
                {
                    if (jungleObject.JungleObjectType == jungleObjectType && jungleObject.Status != Statuses.Visited)
                    {
                        counter += 1;
                        if (counter == randomJungleObject)
                        {
                            result = jungleObject;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Counts unvisited objects of given type in the jungle
        /// </summary>
        /// <param name="jungleObjectType">type of the jungle object to count</param>
        /// <returns>count of objects</returns>
        internal int CountOf(JungleObjectTypes jungleObjectType)
        {
            return Jungle.Count(jo => jo.JungleObjectType == jungleObjectType && jo.Status != Statuses.Visited);
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

        public List<JungleObject> GetJungleObjects(JungleObjectTypes jungleObjectType)
        {
            List<JungleObject> result = new List<JungleObject>();
            foreach (JungleObject jungleObject in Jungle.Where(jo => jo.JungleObjectType == jungleObjectType))
            {
                result.Add(jungleObject);
            }
            return result;
        }

        private JungleObject FindObjectInVector(Point coordinates, int distance, JungleObjectTypes jungleObjectType, Statuses statuses, bool horizontally)
        {
            JungleObject result = null;
            Point point = new Point();

            if (horizontally)
                point.Y = coordinates.Y + distance;
            else
                point.X = coordinates.X + distance;

            for (int radius = -Math.Abs(distance); radius <= Math.Abs(distance); radius++)
            {
                if (horizontally)
                    point.X = coordinates.X + radius;
                else
                    point.Y = coordinates.Y + radius;
                JungleObject jungleObject = GetJungleObjectAt(point);
                if (jungleObject != null && jungleObject.JungleObjectType == jungleObjectType && (jungleObject.Status & statuses) > 0)
                {
                    result = jungleObject;
                    break;
                }
            }
            return result;
        }
    }
}
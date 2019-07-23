using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleManager
    {
        private int emptyFieldsCount;

        public List<JungleObject> JungleObjects { get; private set; } = new List<JungleObject>();

        internal void GenerateJungle()
        {
            Configuration.Read();
            Random random = new Random();

            JungleObjects.Clear();
            foreach (JungleObjectTypes jungleObjectType in Enum.GetValues(typeof(JungleObjectTypes)))
            {
                if (jungleObjectType == JungleObjectTypes.DenseJungle)
                {
                    int denseJungleCount = random.Next(Configuration.JungleObjectsCount[JungleObjectTypes.DenseJungle]) + 1;
                    for (int i = 0; i < denseJungleCount; i++)
                    {
                        JungleObjects.Add(new JungleObject(JungleObjectTypes.DenseJungle));
                    }

                }
                else if (jungleObjectType == JungleObjectTypes.EmptyField || jungleObjectType == JungleObjectTypes.Rambler)
                {

                }
                else
                {
                    for (int i = 0; i < Configuration.JungleObjectsCount[jungleObjectType]; i++)
                    {
                        JungleObjects.Add(new JungleObject(jungleObjectType));
                    }
                }
            }

            emptyFieldsCount = Configuration.JungleHeight * Configuration.JungleWidth - JungleObjects.Count;
            for (int i = JungleObjects.Count; i < Configuration.JungleHeight * Configuration.JungleWidth; i++)
            {
                JungleObjects.Add(new JungleObject(JungleObjectTypes.EmptyField));
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

        internal void ReleaseRambler(Rambler rambler)
        {
            Random random = new Random();
            int ramblerPosition = random.Next(emptyFieldsCount) + 1;
            JungleObject ramblerJungleObject = null;
            foreach (JungleObject jungleObject in JungleObjects)
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
            return JungleObjects.Count(jo => jo.JungleObjectType == jungleObjectType);
        }

        public List<JungleObject> GetJungleObjects(byte objectType)
        {
            List<JungleObject> result = new List<JungleObject>();
            foreach (JungleObjectTypes jungleObjectType in Enum.GetValues(typeof(JungleObjectTypes)))
            {
                if (((byte)jungleObjectType & objectType) > 0)
                {
                    result.Add(new JungleObject(jungleObjectType));
                }
            }
            return result;
        }
    }
}

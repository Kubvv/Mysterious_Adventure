using System;
using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleObject
    {
        public JungleObjectTypes JungleObjectType { get; private set; }
        public string Name { get; private set; }
        public string Shape => Configuration.ShapeOf(JungleObjectType);
        public Point Coordinates { get; private set; }

        public JungleObject(JungleObjectTypes jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            Name = Enum.GetName(typeof(JungleObjectTypes), jungleObjectType);
        }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }
    }
}

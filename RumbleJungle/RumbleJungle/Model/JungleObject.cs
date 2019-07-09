using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleObject
    {
        public Point Coordinates { get; private set; }
        public JungleObjectTypes JungleObjectType { get; private set; }

        public JungleObject(JungleObjectTypes jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
        }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }
    }
}

using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleObject
    {
        public Point Coordinates { get; private set; }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }
    }
}

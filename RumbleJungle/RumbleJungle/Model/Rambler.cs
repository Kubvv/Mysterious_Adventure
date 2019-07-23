using System;
using System.Windows;

namespace RumbleJungle.Model
{
    public class Rambler : JungleObject
    {
        public int Health { get; private set; }

        public Rambler() : base(JungleObjectTypes.Rambler)
        {
        }

        public void Reset()
        {
            Health = 100;
        }

        public event EventHandler Moved;

        public override void SetCoordinates(Point point)
        {
            base.SetCoordinates(point);
            Moved?.Invoke(this, null);
        }
    }
}

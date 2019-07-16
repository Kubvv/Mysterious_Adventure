using System;
using System.Windows;

namespace RumbleJungle.Model
{
    class Rambler : JungleObject
    {
        public event EventHandler Moved;

        public int Health { get; private set; }

        public Rambler() : base(JungleObjectTypes.Rambler)
        {
            Health = 100;
        }

        public override void SetCoordinates(Point point)
        {
            base.SetCoordinates(point);
            Moved?.Invoke(this, null);
        }
    }
}

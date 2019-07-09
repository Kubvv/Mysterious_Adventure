using System;
using System.Windows;

namespace RumbleJungle.Model
{
    class Rambler : JungleObject
    {
        public event EventHandler Moved;

        public Rambler() : base(JungleObjectTypes.Rambler)
        {
        }

        public override void SetCoordinates(Point point)
        {
            base.SetCoordinates(point);
            Moved?.Invoke(this, null);
        }
    }
}

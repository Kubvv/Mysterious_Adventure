using System;
using System.Windows;

namespace RumbleJungle.Model
{
    public class JungleObject
    {
        public JungleObjectTypes JungleObjectType { get; private set; }
        public string Name { get; private set; }
        public Point Coordinates { get; private set; }
        public Statuses Status { get; private set; }

        public event EventHandler StatusChanged;

        public JungleObject(JungleObjectTypes jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            Name = Enum.GetName(typeof(JungleObjectTypes), jungleObjectType);
            Status = Configuration.VisibleItems.Contains(jungleObjectType) ? Statuses.Visible : Statuses.Hidden;
        }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }

        internal void SetStatus(Statuses status)
        {
            Status = status;
            StatusChanged?.Invoke(this, null);
        }
    }
}

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

        public event EventHandler TypeChanged;
        public event EventHandler StatusChanged;

        public JungleObject(JungleObjectTypes jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            Name = Enum.GetName(typeof(JungleObjectTypes), jungleObjectType);
        }

        public void Reset()
        {
            Status = Configuration.VisibleItems.Contains(JungleObjectType) || Configuration.DebugMode ? Statuses.Visible : Statuses.Hidden;
        }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }

        public void SetStatus(Statuses status)
        {
            Status = status;
            StatusChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Changes the type of the jungle object to given type
        /// </summary>
        /// <param name="jungleObjectType">New type of jungle object</param>
        public void ChangeTypeTo(JungleObjectTypes jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            Name = Enum.GetName(typeof(JungleObjectTypes), jungleObjectType);
            TypeChanged?.Invoke(this, null);
        }
    }
}

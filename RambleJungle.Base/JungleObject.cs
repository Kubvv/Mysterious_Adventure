namespace RambleJungle.Base
{
    using System.Drawing;

    public class JungleObject
    {
        public JungleObjectType JungleObjectType { get; private set; }
        public string Name { get; private set; }
        public Point Coordinates { get; private set; }
        public Statuses Status { get; private set; }
        public object? BackingObject { get; private set; }

        public event EventHandler? TypeChanged;
        public event EventHandler? StatusChanged;

        public JungleObject(JungleObjectType jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            BackingObject = null;
            Name = Enum.GetName(typeof(JungleObjectType), jungleObjectType) ?? "?";
        }

        public JungleObject(JungleObjectType jungleObjectType, object backingObject, string name)
        {
            JungleObjectType = jungleObjectType;
            BackingObject = backingObject;
            Name = name;
        }

        public virtual void Reset()
        {
            Status = Config.VisibleItems.Contains(JungleObjectType) || Config.DebugMode ? Statuses.Visible : Statuses.Hidden;
        }

        public virtual void SetCoordinates(Point point)
        {
            Coordinates = point;
        }

        public void SetStatus(Statuses status)
        {
            Status = status;
            StatusChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Changes the type of the jungle object to given type
        /// </summary>
        /// <param name="jungleObjectType">New type of jungle object</param>
        public void ChangeTypeTo(JungleObjectType jungleObjectType)
        {
            JungleObjectType = jungleObjectType;
            Name = Enum.GetName(typeof(JungleObjectType), jungleObjectType) ?? "?";
            TypeChanged?.Invoke(this, new EventArgs());
        }
    }
}

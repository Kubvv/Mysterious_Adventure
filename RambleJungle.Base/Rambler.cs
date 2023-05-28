namespace RambleJungle.Base
{
    using System.Drawing;

    public class Rambler : LivingJungleObject
    {
        private readonly JungleModel jungleModel;

        public double Strength { get; private set; } = 1;

        public bool Visible { get; private set; }

        public Rambler(JungleModel jungleModel) : base(JungleObjectType.Rambler)
        {
            this.jungleModel = jungleModel;
            Reset();
        }

        public new void Reset()
        {
            SetHealth(Config.DebugMode ? 50 : 100);
            SetVisible(true);
        }

        public event EventHandler? Moved;

        public override void SetCoordinates(Point point)
        {
            base.SetCoordinates(point);
            JungleObject? jungleObject = jungleModel.GetJungleObjectAt(point);
            jungleObject?.SetStatus(Statuses.Visited);
            Moved?.Invoke(this, new EventArgs());
        }

        public event EventHandler? StrengthChanged;

        public void SetStrength(double newStrength)
        {
            Strength = newStrength;
            StrengthChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler? VisibleChanged;

        public void SetVisible(bool visible)
        {
            Visible = visible;
            VisibleChanged?.Invoke(this, new EventArgs());
        }
    }
}

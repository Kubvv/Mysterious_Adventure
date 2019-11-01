using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class RamblerViewModel : JungleObjectViewModel
    {
        private readonly Rambler rambler;

        public double Strength => rambler.Strength;
        public bool Visible => rambler.Visible;
        public new double Width => base.Width - 1;
        public new double Height => base.Height - 1;

        public RamblerViewModel(GameModel gameModel) : base(gameModel?.Rambler)
        {
            rambler = gameModel?.Rambler;
            if (rambler != null)
            {
                rambler.Moved += RamblerMoved;
                rambler.HealthChanged += RamblerHealthChanged;
                rambler.StrengthChanged += RamblerStrengthChanged;
                rambler.VisibleChanged += RamblerVisibleChanged;
            }
        }

        private void RamblerMoved(object sender, EventArgs e)
        {
            Update();
        }
        private void RamblerHealthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Health));
        }

        private void RamblerStrengthChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Strength));
        }

        private void RamblerVisibleChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Visible));
        }
    }
}

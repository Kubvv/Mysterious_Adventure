using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class RamblerViewModel : JungleObjectViewModel
    {
        private readonly Rambler rambler;

        public int Health => rambler.Health;

        public RamblerViewModel(GameManager gameManager) : base(gameManager.Rambler)
        {
            rambler = gameManager.Rambler;
            rambler.Moved += RamblerMoved;
        }

        private void RamblerMoved(object sender, EventArgs e)
        {
            Update();
        }
    }
}

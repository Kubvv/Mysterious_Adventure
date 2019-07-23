using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : JungleObjectViewModel
    {
        private readonly Treasure treasure;

        public int Found => treasure.Found;

        public TreasureViewModel(GameManager gameManager) : base(gameManager.Treasure)
        {
            treasure = gameManager.Treasure;
        }
    }
}

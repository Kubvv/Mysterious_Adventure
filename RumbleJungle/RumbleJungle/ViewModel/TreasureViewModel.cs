using RumbleJungle.Model;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : JungleObjectViewModel
    {
        public int Found => Configuration.JungleObjectsCount[JungleObjectTypes.Treasure] - Count;

        public TreasureViewModel() : base(new JungleObject(JungleObjectTypes.Treasure))
        {
        }
    }
}

using RumbleJungle.Model;
using System.Collections.Generic;
using System.Linq;

namespace RumbleJungle.ViewModel
{
    public class TreasureViewModel : JungleObjectViewModel
    {
        public int Found => Configuration.JungleObjectsCount[JungleObjectTypes.Treasure] - Count;

        public TreasureViewModel(JungleModel jungleModel) : base(jungleModel.GetJungleObjects(new List<JungleObjectTypes>() { JungleObjectTypes.Treasure }).FirstOrDefault())
        {
        }
    }
}

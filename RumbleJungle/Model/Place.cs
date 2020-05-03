using CommonServiceLocator;
using System;

namespace RumbleJungle.Model
{
    public class Place : JungleObject
    {
        private readonly JungleManager jungleManager = ServiceLocator.Current.GetInstance<JungleManager>();

        public Places PlaceType { get; private set; }
        public int Count { get; private set; }

        public Place(Places place) : base(Enum.GetName(typeof(Places), place))
        {
            PlaceType = place;
            Count = jungleManager.CountOf(place);
        }
    }
}

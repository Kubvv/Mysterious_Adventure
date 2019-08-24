namespace RumbleJungle.Model
{
    public class WeaponToBeast
    {
        public WeaponTypes WeaponType;
        public JungleObjectTypes BeastType;

        public WeaponToBeast(WeaponTypes weaponType, JungleObjectTypes beastType)
        {
            WeaponType = weaponType;
            BeastType = beastType;
        }
    }
}

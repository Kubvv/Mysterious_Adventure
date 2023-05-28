namespace RambleJungle.Base
{
    public class JungleArsenal : JungleObject
    {
        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();

        public JungleArsenal(JungleObjectType jungleStoreType) : base(jungleStoreType)
        {
        }

        public void AddWeapon(Weapon weapon)
        {
            Weapons.Add(weapon);
        }
    }
}
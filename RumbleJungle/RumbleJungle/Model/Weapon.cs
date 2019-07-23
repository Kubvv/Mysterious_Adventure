using System;

namespace RumbleJungle.Model
{
    public class Weapon
    {
        private WeaponTypes weaponType;

        public string Name { get; private set; }
        public string Shape => Configuration.ShapeOf(weaponType);
        public int Count { get; private set; }

        public Weapon(WeaponTypes weaponType)
        {
            this.weaponType = weaponType;
            Name = Enum.GetName(typeof(WeaponTypes), weaponType);
            Count = weaponType == WeaponTypes.Dagger ? -1 : 1;
        }

        public void Reset()
        {
            Count = weaponType == WeaponTypes.Dagger ? -1 : 1;
        }
    }
}

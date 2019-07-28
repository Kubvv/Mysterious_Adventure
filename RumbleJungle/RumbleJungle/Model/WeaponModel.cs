using System;
using System.Collections.Generic;

namespace RumbleJungle.Model
{
    public class WeaponModel
    {
        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();

        public WeaponModel()
        {
            foreach (WeaponTypes weaponType in Enum.GetValues(typeof(WeaponTypes)))
            {
                Weapons.Add(new Weapon(weaponType));
            }
        }
        public void CollectWeapon()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.Reset();
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace RambleJungle.Model
{
    public class WeaponModel
    {
        private readonly List<int> weaponsLeftForDraw = new List<int>();

        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();

        public WeaponModel()
        {
            foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
            {
                Weapons.Add(new Weapon(weaponType));
            }
        }
        public void CollectWeapon()
        {
            Weapons.ForEach(weapon => weapon.Reset());
        }

        public Weapon ExclusiveRandomWeapon()
        {
            if (weaponsLeftForDraw.Count <= 0)
            {
                ResetRandomWeapons();
            }
            int index = Config.Random.Next(weaponsLeftForDraw.Count);
            int randomWeaponIndex = weaponsLeftForDraw[index];
            weaponsLeftForDraw.RemoveAt(index);
            return Weapons[randomWeaponIndex];
        }

        public void ResetRandomWeapons()
        {
            weaponsLeftForDraw.Clear();

            int index = 0;
            foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
            {
                if (weaponType != WeaponType.Dagger)
                {
                    weaponsLeftForDraw.Add(index);
                }
                index++;
            }
        }

        public void ChangeRandomWeaponCount(int quantity)
        {
            ExclusiveRandomWeapon().ChangeCount(quantity);
        }

        public void ChangeAllWeaponsCount(int quantity)
        {
            Weapons.ForEach(weapon => weapon.ChangeCount(quantity));
        }

        public void SetDoubleAttack()
        {
            ExclusiveRandomWeapon().SetDoubleAttack(true);
        }
    }
}

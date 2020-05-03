using System;

namespace RumbleJungle.Model
{
    public class Weapon
    {
        public WeaponType WeaponType { get; private set; }
        public string Name { get; private set; }
        public int Count { get; private set; }
        public bool DoubleAttack { get; private set; }

        public event EventHandler CountChanged;
        public event EventHandler DoubleAttackChanged;

        public Weapon(WeaponType weaponType)
        {
            WeaponType = weaponType;
            Name = Enum.GetName(typeof(WeaponType), weaponType);
            Reset();
        }

        public void Reset()
        {
            Count = WeaponType == WeaponType.Dagger ? -1 : 1;
            DoubleAttack = false;
        }

        public void ChangeCount(int quantity)
        {
            if (WeaponType != WeaponType.Dagger)
            {
                Count += quantity;
                CountChanged?.Invoke(this, null);
            }
        }

        public void SetDoubleAttack(bool value)
        {
            if (WeaponType != WeaponType.Dagger)
            {
                DoubleAttack = value;
                DoubleAttackChanged?.Invoke(this, null);
            }
        }
    }
}

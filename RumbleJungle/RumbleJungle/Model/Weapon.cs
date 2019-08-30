using System;

namespace RumbleJungle.Model
{
    public class Weapon
    {
        public WeaponTypes WeaponType { get; private set; }
        public string Name { get; private set; }
        public int Count { get; private set; }
        public bool DoubleAttack { get; private set; }

        public event EventHandler CountChanged;
        public event EventHandler DoubleAttackChanged;

        public Weapon(WeaponTypes weaponType)
        {
            WeaponType = weaponType;
            Name = Enum.GetName(typeof(WeaponTypes), weaponType);
            Reset();
        }

        public void Reset()
        {
            Count = WeaponType == WeaponTypes.Dagger ? -1 : 1;
            DoubleAttack = false;
        }

        public void ChangeCount(int quantity)
        {
            if (WeaponType != WeaponTypes.Dagger)
            {
                Count += quantity;
                CountChanged?.Invoke(this, null);
            }
        }

        public void SetDoubleAttack(bool value)
        {
            if (WeaponType != WeaponTypes.Dagger)
            {
                DoubleAttack = value;
                DoubleAttackChanged?.Invoke(this, null);
            }
        }
    }
}

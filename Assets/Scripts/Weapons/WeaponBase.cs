using Items;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBase : LocalItem
    {
        [SerializeField] private float attackDelay;
        [SerializeField] protected int weaponDamage;

        protected float AttackTime;
        protected bool CanAttack;
        protected bool IsPlayerWeapon;

        public WeaponBase InitWeapon(Transform attackPoint, bool isPlayerWeapon)
        {
            WeaponBase clientWeapon = Instantiate(this, attackPoint.transform.position, attackPoint.localRotation,
                attackPoint);

            clientWeapon.IsPlayerWeapon = isPlayerWeapon;
            return clientWeapon;
        }

        private void Update()
        {
            AttackTime += Time.deltaTime;
            if (AttackTime >= attackDelay)
                CanAttack = true;
        }

        public abstract void Attack(Transform shotPoint);

        
        
        protected void ResetShootTimer()
        {
            CanAttack = false;
            AttackTime = 0f;
        }
    }
}
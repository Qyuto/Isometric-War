using System;
using Interface;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : WeaponBase
    {
        [SerializeField] private float rayDistance;

        public override void Attack(Transform shotPoint)
        {
            if (!CanAttack) return;
            var rayResult = Physics2D.Raycast(shotPoint.position, shotPoint.right, rayDistance);
            if (rayResult.collider == null) return;
            IAttacked attacked = rayResult.collider.GetComponentInParent<IAttacked>();
            attacked?.GetDamage(weaponDamage);
            ResetShootTimer();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position, transform.right * rayDistance);
        }
    }
}
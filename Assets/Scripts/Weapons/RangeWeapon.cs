using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class RangeWeapon : WeaponBase
    {
        [SerializeField] private float spreadFactor;
        [SerializeField] protected float shotForce;
        [SerializeField] protected BulletInfo bulletInfo;

        public override void Attack(Transform shotPoint)
        {
            if(!CanAttack) return;
            Shoot(shotPoint);
            ResetShootTimer();
        }

        public virtual void Shoot(Transform shotPoint)
        {
            Bullet bullet = Instantiate(bulletInfo.GetBulletPrefab(), shotPoint.transform.position,
                CreateSpread(shotPoint.rotation));
            bullet.InitBullet(weaponDamage + bulletInfo.GetBulletDamage(), shotForce);
        }

        protected Quaternion CreateSpread(Quaternion rotation)
        {
            Vector3 temp = rotation.eulerAngles;
            return Quaternion.Euler(temp.x, temp.y, temp.z + Random.Range(-spreadFactor, spreadFactor));
        }
    }
}
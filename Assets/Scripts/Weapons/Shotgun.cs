using UnityEngine;

namespace Weapons
{
    public class Shotgun : Weapon
    {
        [Tooltip("The number of bullets during the shot")] [SerializeField]
        private int bullets;

        public override void Shoot(Transform shotPoint)
        {
            if (!CanShoot) return;

            for (int i = 0; i < bullets; i++)
            {
                Bullet bullet = Instantiate(bulletInfo.GetBulletPrefab(), shotPoint.transform.position,
                    CreateSpread(transform.rotation));
                bullet.InitBullet(weaponDamage + bulletInfo.GetBulletDamage(), shotForce);
            }

            ResetShootTimer();
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class MultiWeapon : Weapon
    {
        [Tooltip("The number of bullets during the shot")] [SerializeField]
        private int bullets;
    
        [SerializeField] private float multiShootDelay;

        private bool _isShooting;
        
        public override void Shoot(Transform shotPoint)
        {
            if (!CanShoot || _isShooting) return;
            StopAllCoroutines();
            
            StartCoroutine(MultiShoot(shotPoint));
        }

        private IEnumerator MultiShoot(Transform shotPoint)
        {
            _isShooting = true;
            for (int i = 0; i < bullets; i++)
            {
                Bullet bullet = Instantiate(bulletInfo.GetBulletPrefab(), shotPoint.transform.position,
                    CreateSpread(shotPoint.rotation));
                bullet.InitBullet(weaponDamage + bulletInfo.GetBulletDamage(), shotForce);
                yield return new WaitForSeconds(multiShootDelay);
            }
            _isShooting = false;
            ResetShootTimer();
        }
    }
}
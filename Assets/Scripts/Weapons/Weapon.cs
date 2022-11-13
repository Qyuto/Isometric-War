using Interface;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Weapon : LocalItem, IWeaponShot
    {
        [SerializeField] private float shotDelay;
        [SerializeField] private float spreadFactor;

        [SerializeField] protected int weaponDamage;
        [SerializeField] protected float shotForce;
        [SerializeField] protected BulletInfo bulletInfo;

        protected float ShotTime;
        protected bool CanShoot;

        public Weapon InitWeapon(Transform shotPoint)
        {
            Weapon clientWeapon = Instantiate(this, shotPoint.transform.position, shotPoint.localRotation, shotPoint);
            return clientWeapon;
        }

        private void Update()
        {
            ShotTime += Time.deltaTime;
            if (ShotTime >= shotDelay)
                CanShoot = true;
        }


        public virtual void Shoot(Transform shotPoint)
        {
            if (!CanShoot) return;

            Bullet bullet = Instantiate(bulletInfo.GetBulletPrefab(), shotPoint.transform.position,
                CreateSpread(shotPoint.rotation));
            bullet.InitBullet(weaponDamage + bulletInfo.GetBulletDamage(), shotForce);

            ResetShootTimer();
        }

        protected Quaternion CreateSpread(Quaternion rotation)
        {
            Vector3 temp = rotation.eulerAngles;
            return Quaternion.Euler(temp.x, temp.y, temp.z + Random.Range(-spreadFactor, spreadFactor));
        }

        protected void ResetShootTimer()
        {
            CanShoot = false;
            ShotTime = 0f;
        }
    }
}
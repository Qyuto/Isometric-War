using System;
using Interface;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeaponShot
    {
        [SerializeField] private float shotDelay;
        [SerializeField] private float spreadFactor;

        [SerializeField] protected int weaponDamage;
        [SerializeField] protected float shotForce;
        [SerializeField] protected WeaponInfo weaponInfo;
        [SerializeField] protected BulletInfo bulletInfo;

        protected float ShotTime;
        protected bool CanShoot;

        public Weapon InitWeapon(Transform shotPoint) =>
            Instantiate(this, shotPoint.transform.position, shotPoint.localRotation, shotPoint);

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
                CreateSpread(transform.rotation));
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
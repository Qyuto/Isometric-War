using System;
using Interface;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeaponShot
    {
        [SerializeField] private int weaponDamage;
        [SerializeField] private float shotForce;
        [SerializeField] private float shotDelay;

        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private BulletInfo bulletInfo;

        private float _shotTime;
        private bool _canShoot;

        public Weapon InitWeapon(Transform shotPoint) =>
            Instantiate(this, shotPoint.transform.position, shotPoint.localRotation, shotPoint);

        private void Update()
        {
            _shotTime += Time.deltaTime;
            if (_shotTime >= shotDelay)
                _canShoot = true;
        }


        public virtual void Shoot(Transform shotPoint)
        {
            if (!_canShoot) return;

            Bullet bullet = Instantiate(bulletInfo.GetBulletPrefab(), shotPoint.transform.position, shotPoint.rotation);
            bullet.InitBullet(weaponDamage + bulletInfo.GetBulletDamage(), shotForce);

            ResetShootTimer();
        }

        private void ResetShootTimer()
        {
            _canShoot = false;
            _shotTime = 0f;
        }
    }
}
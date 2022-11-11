using Interface;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Weapon : Item, IWeaponShot
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
            PrepareForPlayer(clientWeapon);

            return clientWeapon;
        }

        private void PrepareForPlayer(Weapon weapon) //  I'm really not sure about this decision
        {
            weapon.GetComponent<Rigidbody2D>().isKinematic = true;
            Destroy(weapon.GetComponent<Collider2D>());
            Destroy(weapon.GetComponent<SpriteRenderer>());
            Destroy(weapon.GetComponentInChildren<Canvas>().gameObject);
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
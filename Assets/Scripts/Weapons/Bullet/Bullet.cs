using Interface;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody2D;
        protected int Damage;
        protected float ShootForce;
        
        public void InitBullet(int damageBullet, float shootForce)
        {
            Damage = damageBullet;
            ShootForce = shootForce;
            Rigidbody2D = GetComponent<Rigidbody2D>();

            Rigidbody2D.AddForce(transform.right * shootForce);
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("BulletDestroyer"))
                DestroyBullet();

            // In this case, it is better not to use TryGetComponent
            IAttacked attacked = col.transform.GetComponentInParent<IAttacked>();
            if (attacked == null) return;

            attacked.GetDamage(Damage);
            BeforeBulletDestroy();
            DestroyBullet();
        }

        protected virtual void BeforeBulletDestroy()
        {
            Debug.Log("Maybe some effects");
        }

        protected void DestroyBullet()
        {
            BeforeBulletDestroy();
            Destroy(gameObject);
        }
    }
}
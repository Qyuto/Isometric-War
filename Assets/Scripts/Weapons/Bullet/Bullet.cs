using Interface;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bulletParticle;

        protected string EnemyTag;
        protected Rigidbody2D Rigidbody2D;
        protected int Damage;
        protected float ShootForce;

        public void InitBullet(int damageBullet, float shootForce, string enemyTag)
        {
            Damage = damageBullet;
            ShootForce = shootForce;
            EnemyTag = enemyTag;

            Rigidbody2D = GetComponent<Rigidbody2D>();
            Rigidbody2D.AddForce(transform.right * shootForce);
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("BulletDestroyer")) DestroyBullet();

            if (!col.CompareTag(EnemyTag)) return;
            // In this case, it is better not to use TryGetComponent
            IAttacked attacked = col.transform.GetComponentInParent<IAttacked>();
            if (attacked == null) return;

            attacked.GetDamage(Damage);
            DestroyBullet();
        }

        protected virtual void BeforeBulletDestroy()
        {
            // ParticleSystem particle = Instantiate(bulletParticle, transform.position, transform.rotation);
            // particle.Play();
            // Destroy(particle.gameObject,2f);
        }

        protected void DestroyBullet()
        {
            BeforeBulletDestroy();
            Destroy(gameObject);
        }
    }
}
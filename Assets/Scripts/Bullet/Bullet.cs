using Interface;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask destroyLayersBullet;

        private Rigidbody2D _rigidbody2D;
        private int _damage;

        public void InitBullet(int damageBullet, float shootForce)
        {
            _damage = damageBullet;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _rigidbody2D.AddForce(transform.right * shootForce);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("BulletDestroyer"))
                Destroy(gameObject);

            // In this case, it is better not to use TryGetComponent
            IAttacked attacked = col.transform.GetComponentInParent<IAttacked>();
            if (attacked == null) return;

            attacked.GetDamage(_damage);
            BeforeBulletDestroy();
            Destroy(gameObject);
        }

        protected virtual void BeforeBulletDestroy()
        {
            Debug.Log("Maybe some effects");
        }
    }
}
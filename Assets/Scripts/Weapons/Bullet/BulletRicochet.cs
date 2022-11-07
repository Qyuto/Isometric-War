using Interface;
using UnityEngine;

namespace Weapons
{
    public class BulletRicochet : Bullet
    {
        [SerializeField] private int maxRicochetCount;
        [SerializeField] private LayerMask ricochetMask;

        private int _currentRicochetCount;

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("BulletDestroyer"))
                Ricochet();

            // In this case, it is better not to use TryGetComponent
            IAttacked attacked = col.transform.GetComponentInParent<IAttacked>();
            if (attacked == null) return;


            attacked.GetDamage(Damage);
            DestroyBullet();
        }

        private void Ricochet()
        {
            if (_currentRicochetCount == maxRicochetCount)
                DestroyBullet();

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, 2f, ricochetMask);

            _currentRicochetCount++;

            Vector3 reflect = Vector3.Reflect(transform.right, hit2D.normal);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * reflect);
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.AddForce(transform.right * ShootForce);


            Debug.Log("Bullet Ricochet");
        }
    }
}
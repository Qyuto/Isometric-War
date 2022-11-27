using System;
using Interface;
using UnityEngine;

namespace Items.Busters
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private int mineDamage;
        [SerializeField] private ParticleSystem explodeParticle;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionDelay;

        private void Awake()
        {
            Invoke(nameof(Explosion), explosionDelay);
        }

        private void Explosion()
        {
            var collider = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (var cldr in collider)
            {
                IAttacked attacked = cldr.GetComponentInParent<IAttacked>();
                attacked?.GetDamage(mineDamage);
            }

            SpawnExplosionParticle();
            Destroy(gameObject);
        }
        private void SpawnExplosionParticle()
        {
            Instantiate(explodeParticle, transform.position, explodeParticle.transform.rotation);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, explosionRadius);
        }
    }
}
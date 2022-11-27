using System;
using Interface;
using UnityEngine;

namespace NPC.Enemy
{
    public class EnemyExplosion : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private ParticleSystem explosionSystem;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private float findRadius;


        private void Update()
        {
            Explode();
        }

        private void Explode()
        {
            var res = Physics2D.OverlapCircle(transform.position, findRadius, playerMask);
            if (res == null) return;

            IAttacked attacked = res.GetComponentInParent<IAttacked>();
            if (attacked == null) return;

            attacked.GetDamage(damage);
            SpawnExplosionParticle();
            Destroy(gameObject);
        }

        private void SpawnExplosionParticle()
        {
            Instantiate(explosionSystem, transform.position, explosionSystem.transform.rotation);
        }
    }
}
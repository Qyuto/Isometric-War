using UnityEngine;

namespace Weapons
{
    public class BulletWithEffect : Bullet
    {
        [SerializeField] private ParticleSystem bulletParticle;


        protected override void BeforeBulletDestroy()
        {
            ParticleSystem particle = Instantiate(bulletParticle, transform.position, transform.rotation);
            particle.Play();
            Destroy(particle.gameObject,2f);
        }
    }
}
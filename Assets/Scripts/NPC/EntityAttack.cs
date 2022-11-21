using UnityEngine;
using Weapons;

namespace NPC
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] protected Weapon entityWeapon;
        [SerializeField] protected Transform shootPoint;

        private void Awake()
        {
            if (entityWeapon == null)
            {
                Debug.LogError("Entity weapon not found\nScript component destroyed");
                Destroy(this);
            }

            entityWeapon = entityWeapon.InitWeapon(shootPoint);
        }

        public virtual void Attack()
        {
            entityWeapon.Shoot(shootPoint);
        }
    }
}
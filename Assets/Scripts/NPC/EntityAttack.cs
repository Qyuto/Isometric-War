using UnityEngine;
using Weapons;

namespace NPC
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] protected WeaponBase entityWeapon;
        [SerializeField] protected Transform shootPoint;

        private void Awake()
        {
            if (entityWeapon == null)
                Debug.LogError("Entity weapon not found\nScript component destroyed");

            entityWeapon = entityWeapon.InitWeapon(shootPoint);
        }

        public virtual void Attack()
        {
            entityWeapon.Attack(shootPoint);
        }
    }
}
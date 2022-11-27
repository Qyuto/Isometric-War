using UnityEngine;
using Weapons;

namespace NPC
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] private OwnerType ownerType = OwnerType.EnemyNpc;
        [SerializeField] protected WeaponBase entityWeapon;
        [SerializeField] protected Transform shootPoint;

        private void Awake()
        {
            if (entityWeapon == null)
                Debug.LogError("Entity weapon not found\nScript component destroyed");

            switch (ownerType)
            {
                case OwnerType.Player:
                    entityWeapon = entityWeapon.InitWeapon(shootPoint, true);
                    break;
                case OwnerType.EnemyNpc:
                    entityWeapon = entityWeapon.InitWeapon(shootPoint, false);
                    break;
            }
        }

        public virtual void Attack()
        {
            entityWeapon.Attack(shootPoint);
        }
    }

    internal enum OwnerType
    {
        Player,
        EnemyNpc
    }
}
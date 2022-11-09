using UnityEngine;
using Weapons;

namespace NPC.Enemy
{
    [RequireComponent(typeof(EntityHealth))]
    public class NpcEnemyAttack : MonoBehaviour
    {
        [SerializeField] private Weapon botWeapon;
        [SerializeField] private Transform shootPoint;

        private void Start()
        {
            if (botWeapon == null) return;
            botWeapon = botWeapon.InitWeapon(shootPoint);
        }

        //At the moment it seems to me that making this method virtual will help us in the future
        public virtual void Attack()
        {
            botWeapon.Shoot(shootPoint);
        }
    }
}
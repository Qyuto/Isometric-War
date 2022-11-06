using Interface;
using UnityEngine;

namespace NPC.Enemy
{
    [RequireComponent(typeof(IWeaponShot), typeof(NpcEnemyAttack))]
    public class NpcEnemyAggressor : MonoBehaviour
    {
        [SerializeField] private float aggressorRadius;
        [SerializeField] private LayerMask playerMask;

        private NpcEnemyAttack _enemyAttack;
        private IWeaponShot _attacker;

        private void Awake()
        {
            _attacker = GetComponent<IWeaponShot>();
            _enemyAttack = GetComponent<NpcEnemyAttack>();
        }

        private void Update()
        {
            FindEnemy();
        }

        private void FindEnemy()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, aggressorRadius, playerMask);

            if (target == null) return;
            RotateToEnemy(target.transform);
            _enemyAttack.Attack();
        }

        private void RotateToEnemy(Transform enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, aggressorRadius);
        }
    }
}
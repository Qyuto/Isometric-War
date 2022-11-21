using UnityEngine;

namespace NPC.Enemy
{
    [RequireComponent(typeof(EntityAttack))]
    public class EnemyAggressor : MonoBehaviour
    {
        [SerializeField] private float aggressorRadius;
        [SerializeField] private LayerMask playerMask;

        private EntityAttack _enemyAttack;
        protected Collider2D _target;

        private void Awake()
        {
            _enemyAttack = GetComponent<EntityAttack>();
        }

        private void Update()
        {
            FindTarget();
        }

        protected virtual void FindTarget()
        {
            _target = Physics2D.OverlapCircle(transform.position, aggressorRadius, playerMask);

            if (_target == null) return;
            RotateToTarget(_target.transform);
            _enemyAttack.Attack();
        }

        private void RotateToTarget(Transform enemy)
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
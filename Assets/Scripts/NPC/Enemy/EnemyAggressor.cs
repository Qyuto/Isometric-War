using UnityEngine;

namespace NPC.Enemy
{
    [RequireComponent(typeof(EntityAttack))]
    public class EnemyAggressor : MonoBehaviour
    {
        [SerializeField] private float aggressorRadius;
        [SerializeField] private LayerMask playerMask;

        private EntityAttack _enemyAttack;
        protected Collider2D Target;

        public Collider2D LastTarget => Target;


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
            Target = Physics2D.OverlapCircle(transform.position, aggressorRadius, playerMask);

            if (Target == null) return;
            _enemyAttack.Attack();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, aggressorRadius);
        }
    }
}
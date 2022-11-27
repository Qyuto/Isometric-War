using UnityEngine;

namespace NPC.Enemy
{
    public class EnemyAgentMove : AgentMove
    {
        [SerializeField] private float dangerDistance;
        private Vector3 _destination;

        protected override void SetAgentDestination(Transform target)
        {
            if ((transform.position - target.position).magnitude < dangerDistance)
                _destination = transform.position + (transform.right * -dangerDistance);
            else
                _destination = target.position;
            Agent.SetDestination(_destination);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dangerDistance);
            Gizmos.DrawWireSphere(_destination, 0.5f);
        }
    }
}
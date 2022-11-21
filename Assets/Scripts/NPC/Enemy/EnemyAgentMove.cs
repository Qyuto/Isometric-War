using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace NPC.Enemy
{
    public class EnemyAgentMove : MonoBehaviour
    {
        [SerializeField] private float stopDistance;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.stoppingDistance = stopDistance;
        }

        public void InitAgent(UnityEvent<Transform> unityEvent)
        {
            unityEvent.AddListener((SetAgentDestination));
        }

        private void SetAgentDestination(Transform target)
        {
            _agent.SetDestination(target.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stopDistance);
        }
    }
}
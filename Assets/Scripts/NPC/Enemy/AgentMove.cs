using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace NPC.Enemy
{
    public abstract class AgentMove : MonoBehaviour
    {
        protected NavMeshAgent Agent;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
        }

        public void InitAgent(UnityEvent<Transform> unityEvent)
        {
            unityEvent.AddListener((SetAgentDestination));
        }

        protected abstract void SetAgentDestination(Transform target);
    }
}
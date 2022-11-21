using UnityEngine;
using UnityEngine.Events;

namespace NPC.Enemy
{
    [RequireComponent(typeof(EnemyDynamicAggressor), typeof(EnemyAgentMove))]
    public class EnemyDynamic : MonoBehaviour
    {
        private EnemyDynamicAggressor _aggressor;
        private EnemyAgentMove _agentMove;
        private UnityEvent<Transform> _onEnemyFindTarget;


        private void Awake()
        {
            _onEnemyFindTarget = new UnityEvent<Transform>();
            _aggressor = GetComponent<EnemyDynamicAggressor>();
            _agentMove = GetComponent<EnemyAgentMove>();
        }

        private void Start()
        {
            _aggressor.InitAggressor(_onEnemyFindTarget);
            _agentMove.InitAgent(_onEnemyFindTarget);
        }
    }
}
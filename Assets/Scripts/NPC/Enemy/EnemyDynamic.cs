using UnityEngine;
using UnityEngine.Events;

namespace NPC.Enemy
{
    public class EnemyDynamic : MonoBehaviour
    {
        private EnemyDynamicAggressor _aggressor;
        private EnemyAgentMove _agentMove;
        private EnemyRotate _enemyRotate;
        private UnityEvent<Transform> _onEnemyFindTarget;

        private void Awake()
        {
            _onEnemyFindTarget = new UnityEvent<Transform>();
        }

        private void Start()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            if (TryGetComponent(out EnemyDynamicAggressor aggressor)) // bad idia
                aggressor.InitAggressor(_onEnemyFindTarget);
            if (TryGetComponent(out AgentMove agentMove))
                agentMove.InitAgent(_onEnemyFindTarget);
            if (TryGetComponent(out EnemyRotate rotate))
                rotate.InitRotate(_onEnemyFindTarget);
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace NPC.Enemy
{
    public class EnemyDynamicAggressor : EnemyAggressor
    {
        private UnityEvent<Transform> _onEnemyFindTarget;

        public void InitAggressor(UnityEvent<Transform> unityEvent)
        {
            _onEnemyFindTarget = unityEvent;
        }

        protected override void FindTarget()
        {
            base.FindTarget();
            if (Target == null) return;
            _onEnemyFindTarget?.Invoke(Target.transform);
        }
    }
}
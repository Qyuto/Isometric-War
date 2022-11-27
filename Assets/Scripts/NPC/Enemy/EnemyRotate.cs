using UnityEngine;
using UnityEngine.Events;

namespace NPC.Enemy
{
    public class EnemyRotate : MonoBehaviour
    {
        public void InitRotate(UnityEvent<Transform> onEnemyFindTarget)
        {
            onEnemyFindTarget.AddListener(RotateToEnemy);
        }
        private void RotateToEnemy(Transform enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        }
    }
}
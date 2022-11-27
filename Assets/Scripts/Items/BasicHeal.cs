using UnityEngine;

namespace Interface
{
    public class BasicHeal : MonoBehaviour
    {
        [SerializeField] private int healValue;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            ICured cured = col.transform.GetComponentInParent<ICured>();

            if (cured == null) return;

            cured.AddHealth(healValue);
            Destroy(gameObject);
        }
    }
}
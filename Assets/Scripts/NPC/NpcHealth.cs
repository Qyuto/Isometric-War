using Interface;
using UnityEngine;

namespace NPC
{
    public class NpcHealth : MonoBehaviour, IAttacked, ICured
    {
        [SerializeField] private int health;

        public void GetDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Debug.Log("Npc death");
                Destroy(gameObject);
            }
        }

        public void AddHealth(int value)
        {
            health += value;
        }
    }
}
using System;
using Interface;
using UnityEngine;

namespace NPC
{
    public class EntityHealth : MonoBehaviour, IAttacked, ICured
    {
        [SerializeField] private int health;
        protected Action<int> OnHealthChange;

        private void Awake()
        {
            OnHealthChange = null;
        }

        public void GetDamage(int damage)
        {
            health -= damage;
            OnHealthChange?.Invoke(health);

            if (health <= 0) Destroy(gameObject);
        }

        public void AddHealth(int value)
        {
            health += value;
            OnHealthChange?.Invoke(health);
        }
    }
}
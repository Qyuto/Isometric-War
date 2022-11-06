using Interface;
using Interface.UI;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IAttacked, ICured
    {
        [SerializeField] private int health;
        [SerializeField] private HealthUI healthUI; //Todo: Create pattern facade in next time

        public void GetDamage(int damage)
        {
            health -= damage;
            healthUI.ChangeHealthSlider(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void AddHealth(int value)
        {
            health += value;
            healthUI.ChangeHealthSlider(health);
        }
    }
}
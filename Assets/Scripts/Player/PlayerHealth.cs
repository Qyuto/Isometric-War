using NPC;
using UI.Main;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : EntityHealth
    {
        [SerializeField] private HealthUI healthUI; //Todo: Create pattern facade in next time

        private void Start()
        {
            OnHealthChange += i => healthUI.ChangeHealthSlider(i);
        }
    }
}
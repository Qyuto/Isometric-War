using NPC;
using UI.Main;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : EntityHealth
    {
        private HealthUI _healthUI;

        private void Start()
        {
            _healthUI = GameObject.Find("PlayerLayer").GetComponent<HealthUI>();
            OnHealthChange += i => _healthUI.ChangeHealthSlider(i);
        }
    }
}
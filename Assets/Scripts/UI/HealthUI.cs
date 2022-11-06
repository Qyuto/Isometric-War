using UnityEngine;
using UnityEngine.UI;

namespace Interface.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void ChangeHealthSlider(int value)
        {
            healthSlider.value = value;
        }
    }
}
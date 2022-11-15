using UnityEngine;
using UnityEngine.UI;

namespace UI.Main
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
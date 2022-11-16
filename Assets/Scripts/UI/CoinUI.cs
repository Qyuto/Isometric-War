using TMPro;
using UnityEngine;

namespace UI.Main
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinTextValue;

        public void ChangeCoinValue(int value)
        {
            coinTextValue.text = $"x{value.ToString()}";
        }
    }
}
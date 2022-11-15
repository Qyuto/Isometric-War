using TMPro;
using UI.Inventory;
using UnityEngine;

namespace UI.Trade
{
    public class TradeInfoHelperUI : ItemInfoHelperUI
    {
        [SerializeField] private TextMeshProUGUI costText;
        public TextMeshProUGUI CostText => costText;
    }
}
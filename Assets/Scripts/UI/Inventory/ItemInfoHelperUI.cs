using TMPro;
using UnityEngine;

namespace UI.Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemInfoHelperUI : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemRarity;
        [SerializeField] private TextMeshProUGUI itemDescription;

        private CanvasGroup _itemGroupInfo;

        private void Awake()
        {
            _itemGroupInfo = GetComponent<CanvasGroup>();
        }

        public CanvasGroup ItemGroupInfo => _itemGroupInfo;
        public TextMeshProUGUI ItemName => itemName;
        public TextMeshProUGUI ItemRarity => itemRarity;
        public TextMeshProUGUI ItemDescription => itemDescription;

    }
}
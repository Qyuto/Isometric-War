using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [CreateAssetMenu(fileName = "itemInfo", menuName = "Isometric/Item", order = 0)]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private Sprite uiItemSprite;
        [SerializeField] private Item itemPrefab;
        
        [SerializeField] private int itemCost;
        [SerializeField] private string itemName;


        public Item ItemPrefab => itemPrefab;
        public Sprite UIItemSprite => uiItemSprite;
    }
}
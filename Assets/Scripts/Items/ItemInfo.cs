using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item info", menuName = "Item/Info", order = 0)]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private ItemRarity rarity = ItemRarity.SoBad;
        [SerializeField] private int itemCost;
        [SerializeField] private Sprite worldSprite;
        [SerializeField] private LocalItem localItem;
        [SerializeField] private WorldItem worldItem;
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;


        public ItemRarity Rarity => rarity;
        public int ItemCost => itemCost;
        public LocalItem LocalItem => localItem;
        public WorldItem WorldItem => worldItem;
        public Sprite WorldSprite => worldSprite;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
    }

    public enum ItemRarity
    {
        SoBad = 0,
        Bad,
        Normal,
        Good,
        OhGod
    }
}
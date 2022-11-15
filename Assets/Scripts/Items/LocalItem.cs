using UnityEngine;

namespace Items
{
    public class LocalItem : MonoBehaviour
    {
        [SerializeField] protected ItemInfo itemInfo;

        public WorldItem DropItem(Vector3 position) => Instantiate(itemInfo.WorldItem, position, Quaternion.identity);
        public ItemInfo GetItemInfo() => itemInfo;
    }
}
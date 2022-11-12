using Items;

namespace Interface.Items
{
    public interface IUsableItem
    {
        public Item PickUp();
        public void Init();
        public ItemInfo GetItemInfo();
    }
}
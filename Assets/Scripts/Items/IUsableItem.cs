namespace Items
{
    public interface IUsableItem
    {
        public LocalItem PickUp();
        public ItemInfo GetItemInfo();
        public void InitItem();
    }
}
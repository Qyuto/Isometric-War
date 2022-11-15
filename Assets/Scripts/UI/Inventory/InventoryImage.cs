using Items;
using UI.Main;


namespace UI.Inventory
{
    public class InventoryImage : UIImage
    {
        public virtual void InitInventoryImage(ItemInfo newItemInfo, ItemInfoHelperUI helperUI)
        {
            LocalItemInfo = newItemInfo;
            HelperUI = helperUI;
        }
    }
}
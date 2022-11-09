using Items;

namespace Interface.Items
{
    public interface IUsable
    {
        public Item PickUp();
        public void Init();
    }
}
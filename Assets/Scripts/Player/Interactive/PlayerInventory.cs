using Interface.Items;
using Interface.UI;
using Items;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private Item[] playerInventory = new Item[3];
        
        private int _posIndex;

        public void InitInventory(IUsable usable)
        {
            if (playerInventory[_posIndex] != null) DropItem();
            playerInventory[_posIndex] = usable.PickUp();
            inventoryUI.UpdateImage(_posIndex,playerInventory[_posIndex].GetItemInfo().UIItemSprite);
        }

        private void Update()
        {
            SetIndex();

            if (Input.GetKeyDown(KeyCode.F))
                DropItem();
        }

        private void DropItem()
        {
            if (playerInventory[_posIndex] == null) return;
            
            var newItem = Instantiate(playerInventory[_posIndex], transform.position + transform.right,
                Quaternion.identity);
            playerInventory[_posIndex] = null;
            inventoryUI.DeleteImage(_posIndex);
            
            newItem.Init();
        }

        private void SetIndex()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                _posIndex = 0;
            else if (Input.GetKeyDown(KeyCode.X))
                _posIndex = 1;
            else if (Input.GetKeyDown(KeyCode.C))
                _posIndex = 2;
            inventoryUI.SetSelectedImage(_posIndex);
        }
    }
}
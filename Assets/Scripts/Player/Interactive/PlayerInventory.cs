using System;
using Interface.Items;
using Interface.UI;
using Items;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private PlayerAttack playerAttack;
        [SerializeField] private Item[] playerInventory = new Item[3];

        private int _posIndex;
        private Action<int> _onPlayerChangeIndex;

        private void Awake()
        {
            _onPlayerChangeIndex = null;
            _onPlayerChangeIndex += i =>
            {
                _posIndex = i;
                if (playerInventory[_posIndex] is Weapon weapon)
                    playerAttack.ChangeCurrentWeapon(weapon);
                inventoryUI.SetSelectedImage(_posIndex);
            };
        }

        public void InitInventory(IUsable usable)
        {
            if (playerInventory[_posIndex] != null) DropItem();
            playerInventory[_posIndex] = usable.PickUp();
            inventoryUI.UpdateImage(_posIndex, playerInventory[_posIndex].GetItemInfo().UIItemSprite);
            _onPlayerChangeIndex(_posIndex);
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
                _onPlayerChangeIndex(0);
            else if (Input.GetKeyDown(KeyCode.X))
                _onPlayerChangeIndex(1);
            else if (Input.GetKeyDown(KeyCode.C))
                _onPlayerChangeIndex(2);
        }
    }
}
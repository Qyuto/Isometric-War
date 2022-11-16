using System;
using Items;
using Items.Busters;
using UI.Inventory;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private PlayerAttack playerAttack;
        [SerializeField] private PlayerBusters playerBusters;
        [SerializeField] private LocalItem[] playerInventory = new LocalItem[3];

        private int _posIndex;
        private Action<int> _onPlayerChangeIndex;
        private Action _onPlayerInitItem;
        private Action _onPlayerDropItem;

        private void Awake()
        {
            _onPlayerChangeIndex = OnPlayerChangeIndex;
            _onPlayerInitItem = OnPlayerInitItem;
            _onPlayerDropItem = OnPlayerDropItem;
        }


        private void Update()
        {
            SetIndex();
            if (Input.GetKeyDown(KeyCode.F)) DropLocalItem();
        }

        public void InitInventory(IUsableItem usable)
        {
            if (ItemIsExists(_posIndex)) DropLocalItem();

            playerInventory[_posIndex] = usable.PickUp();
            inventoryUI.UpdateImage(_posIndex, usable.GetItemInfo());
            OnPlayerInitItem();
        }


        private void DropLocalItem()
        {
            if (!ItemIsExists(_posIndex)) return;

            WorldItem worldItem = playerInventory[_posIndex].DropItem(transform.position + transform.right);
            worldItem.InitItem();
            OnPlayerDropItem();
            playerInventory[_posIndex] = null;
            inventoryUI.DeleteImage(_posIndex);
        }

        private bool ItemIsExists(int index) => playerInventory[_posIndex] != null;

        private void SetIndex()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                _onPlayerChangeIndex(0);
            else if (Input.GetKeyDown(KeyCode.X))
                _onPlayerChangeIndex(1);
            else if (Input.GetKeyDown(KeyCode.C))
                _onPlayerChangeIndex(2);
        }

        private void OnPlayerInitItem()
        {
            switch (playerInventory[_posIndex])
            {
                case ActiveBuster activeBuster:
                    playerBusters.AddNewBuster(activeBuster.GetBuster());
                    break;
                case Weapon weapon:
                    playerAttack.ChangeCurrentWeapon(weapon);
                    break;
            }
        }

        private void OnPlayerChangeIndex(int value)
        {
            _posIndex = value;
            inventoryUI.SetSelectedImage(value);

            switch (playerInventory[_posIndex])
            {
                case Weapon weapon:
                    playerAttack.ChangeCurrentWeapon(weapon);
                    break;
            }
        }

        private void OnPlayerDropItem()
        {
            switch (playerInventory[_posIndex])
            {
                case ActiveBuster activeBuster:
                    playerBusters.RemoveBuster(activeBuster);
                    break;
                case Weapon weapon:
                    playerAttack.ChangeCurrentWeapon(null);
                    break;
            }
        }
    }
}
using System;
using Interface.Items;
using Interface.UI;
using Items;
using Items.Busters;
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
        private Action _onPlayerInitInventory;
        private Action<int> _onPlayerChangeIndex;


        private void Awake()
        {
            _onPlayerChangeIndex = null;
            _onPlayerInitInventory = null;

            _onPlayerInitInventory = OnPlayerInitInventory;
            _onPlayerChangeIndex += OnPlayerChangeIndex;
        }


        public void InitInventory(IUsableItem usable)
        {
            if (playerInventory[_posIndex] != null) DropItem();
            playerInventory[_posIndex] = usable.PickUp();

            _onPlayerInitInventory();
            inventoryUI.UpdateImage(_posIndex, playerInventory[_posIndex].GetItemInfo().UIItemSprite);
        }


        private void Update()
        {
            SetIndex();

            if (Input.GetKeyDown(KeyCode.F))
                DropItem();
        }

        // Todo: Delete scripts that were added by this item 
        private void DropItem()
        {
            if (playerInventory[_posIndex] == null) return;

            var newItem = Instantiate(playerInventory[_posIndex], transform.position + transform.right,
                Quaternion.identity);
            if (playerInventory[_posIndex] as ItemBuster)
                PlayerBusterCollections.Instance.RemoveBuster(_posIndex);

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


        private void OnPlayerInitInventory()
        {
            switch (playerInventory[_posIndex])
            {
                case Weapon weapon:
                {
                    playerAttack.ChangeCurrentWeapon(weapon);
                    break;
                }
                case ItemBuster buster:
                {
                    PlayerBusterCollections.Instance.AddBuster(buster.ActiveBuster, _posIndex);
                    break;
                }
            }
        }

        private void OnPlayerChangeIndex(int index)
        {
            _posIndex = index;
            switch (playerInventory[_posIndex])
            {
                case Weapon weapon:
                {
                    playerAttack.ChangeCurrentWeapon(weapon);
                    break;
                }
            }

            inventoryUI.SetSelectedImage(_posIndex);
        }
    }
}
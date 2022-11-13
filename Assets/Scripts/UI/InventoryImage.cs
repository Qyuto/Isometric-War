using System;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Interface.UI
{
    public class InventoryImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image childrenImage;
        [SerializeField] private CanvasGroup itemGroupInfo;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemRarity;
        [SerializeField] private TextMeshProUGUI itemDescription;

        private ItemInfo _localItemInfo;

        private void ShowItemInfo()
        {
            if (_localItemInfo == null) return;

            itemName.text = _localItemInfo.ItemName;
            itemDescription.text = _localItemInfo.ItemDescription;
            CheckItemRarity();
            itemGroupInfo.gameObject.SetActive(true);
        }

        private void HideItemInfo()
        {
            itemGroupInfo.gameObject.SetActive(false);
        }

        public void ChangeSprite(ItemInfo itemInfo)
        {
            _localItemInfo = itemInfo;
            childrenImage.sprite = itemInfo.WorldSprite;

            CheckItemRarity();
            TurnImage(true);
        }

        private void CheckItemRarity()
        {
            switch (_localItemInfo.Rarity)
            {
                case ItemRarity.SoBad:
                {
                    itemRarity.text = "So Bad";
                    itemRarity.color = Color.gray;
                }

                    break;
                case ItemRarity.Bad:
                {
                    itemRarity.text = "Uhh...";
                    itemRarity.color = Color.grey;
                }
                    break;
                case ItemRarity.Normal:
                {
                    itemRarity.text = "Normal";
                    itemRarity.color = Color.green;
                }
                    break;
                case ItemRarity.Good:
                {
                    itemRarity.text = "So good";
                    itemRarity.color = Color.cyan;
                }
                    break;
                case ItemRarity.OhGod:
                {
                    itemRarity.text = "Where did you find this";
                    itemRarity.color = Color.red;
                }
                    break;
            }
        }

        public void TurnImage(bool status)
        {
            childrenImage.gameObject.SetActive(status);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowItemInfo();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideItemInfo();
        }
    }
}
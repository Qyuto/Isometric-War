using Items;
using UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Main
{
    public abstract class UIImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image childrenImage;
        protected ItemInfo LocalItemInfo;
        protected ItemInfoHelperUI HelperUI;

        protected virtual void ShowItemInfo()
        {
            if (LocalItemInfo == null) return;

            HelperUI.ItemName.text = LocalItemInfo.ItemName;
            HelperUI.ItemDescription.text = LocalItemInfo.ItemDescription;
            SetItemRarityText(LocalItemInfo.Rarity);
            HelperUI.ItemGroupInfo.gameObject.SetActive(true);
        }


        private void HideItemInfo()
        {
            HelperUI.ItemGroupInfo.gameObject.SetActive(false);
        }


        public void ChangeSprite()
        {
            Debug.Log(transform.position);
            childrenImage.sprite = LocalItemInfo.WorldSprite;
            SetItemRarityText(LocalItemInfo.Rarity);
            TurnImage(true);
        }

        private void SetItemRarityText(ItemRarity rarity)
        {
            switch (rarity)
            {
                case ItemRarity.SoBad:
                {
                    HelperUI.ItemRarity.text = "So Bad";
                    HelperUI.ItemRarity.color = Color.gray;
                }
                    break;
                case ItemRarity.Bad:
                {
                    HelperUI.ItemRarity.text = "Uhh...";
                    HelperUI.ItemRarity.color = Color.grey;
                }
                    break;
                case ItemRarity.Normal:
                {
                    HelperUI.ItemRarity.text = "Normal";
                    HelperUI.ItemRarity.color = Color.green;
                }
                    break;
                case ItemRarity.Good:
                {
                    HelperUI.ItemRarity.text = "So good";
                    HelperUI.ItemRarity.color = Color.cyan;
                }
                    break;
                case ItemRarity.OhGod:
                {
                    HelperUI.ItemRarity.text = "Where did you find this";
                    HelperUI.ItemRarity.color = Color.red;
                }
                    break;
                default:
                {
                    HelperUI.ItemRarity.text = "So Bad";
                    HelperUI.ItemRarity.color = Color.gray;
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
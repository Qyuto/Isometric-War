using System.Collections.Generic;
using Items;
using NPC.Trader;
using UI.Inventory;
using Unity.Mathematics;
using UnityEngine;

namespace UI.Trade
{
    public class TradeUI : MonoBehaviour
    {
        //Todo: Creat trade ui window
        [SerializeField] private Transform imageParent;
        [SerializeField] private TradeImage imagePrefab;
        [SerializeField] private TradeInfoHelperUI infoHelperUI;

        private List<InventoryImage> _traderItems;

        public void InitTradeUI(List<LocalItem> traderItems, ITrade playerTrade)
        {
            gameObject.SetActive(true);
            foreach (var item in traderItems)
            {
                TradeImage newImage = Instantiate(imagePrefab, Vector3.zero, quaternion.identity, imageParent);
                newImage.InitTradeImage(item.GetItemInfo(), infoHelperUI, playerTrade);
                newImage.ChangeSprite();
            }
        }
    }
}
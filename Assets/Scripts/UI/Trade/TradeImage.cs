using Items;
using NPC.Trader;
using UI.Main;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.Trade
{
    public class TradeImage : UIImage, IPointerClickHandler
    {
        private ITrade _playerTrade;

        protected override void ShowItemInfo()
        {
            base.ShowItemInfo();
            if (HelperUI is TradeInfoHelperUI ui)
                ui.CostText.text = LocalItemInfo.ItemCost.ToString();
        }

        public void InitTradeImage(ItemInfo newItemInfo, TradeInfoHelperUI helperUI, ITrade playerTrade)
        {
            LocalItemInfo = newItemInfo;
            HelperUI = helperUI;
            _playerTrade = playerTrade;
            childrenImage = Instantiate(childrenImage, Vector3.zero, Quaternion.identity, transform);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Try to buy item");
            if (_playerTrade.GetMoney() >= LocalItemInfo.ItemCost)
            {
                LocalItemInfo.LocalItem.DropItem(_playerTrade.GetDropItemPosition());
                _playerTrade.ReduceMoney(LocalItemInfo.ItemCost);
            }
                
        }
    }
}
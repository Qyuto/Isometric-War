using System.Collections.Generic;
using Items;
using NPC.Trader;
using UI.Trade;
using UnityEngine;

namespace Player
{
    public class PlayerTrade : MonoBehaviour, ITrade
    {
        [SerializeField] private int playerMoney;
        [SerializeField] private TradeUI tradeUI;


        public void AddMoney(int value)
        {
            playerMoney += value;
        }

        public void ReduceMoney(int value)
        {
            playerMoney -= value;
            if (playerMoney < 0) playerMoney = 0;
        }

        public Vector3 GetDropItemPosition() => transform.position + transform.right;

        public int GetMoney() => playerMoney;

        public void StartTrading(List<LocalItem> traderItems)
        {
            tradeUI.InitTradeUI(traderItems, this);
        }
    }
}
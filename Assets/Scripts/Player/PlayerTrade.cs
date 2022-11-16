using System;
using System.Collections.Generic;
using Items;
using NPC.Trader;
using UI.Main;
using UI.Trade;
using UnityEngine;

namespace Player
{
    public class PlayerTrade : MonoBehaviour, ITrade
    {
        [SerializeField] private int playerMoney;
        [SerializeField] private TradeUI tradeUI;
        [SerializeField] private CoinUI coinUI;

        private void Start()
        {
            coinUI.ChangeCoinValue(playerMoney);
        }

        public void AddMoney(int value)
        {
            if (value < 0) return;
            playerMoney += value;
            coinUI.ChangeCoinValue(playerMoney);
        }

        public void ReduceMoney(int value)
        {
            if (value < 0) return;

            playerMoney -= value;
            if (playerMoney < 0) playerMoney = 0;
            coinUI.ChangeCoinValue(playerMoney);
        }

        public Vector3 GetDropItemPosition() => transform.position + transform.right;

        public int GetMoney() => playerMoney;

        public void StartTrading(List<LocalItem> traderItems)
        {
            tradeUI.InitTradeUI(traderItems, this);
        }
    }
}
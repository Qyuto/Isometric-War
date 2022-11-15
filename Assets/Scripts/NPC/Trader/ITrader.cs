using System.Collections.Generic;
using Items;
using UnityEngine;

namespace NPC.Trader
{
    public interface ITrade
    {
        public void AddMoney(int value);
        public void ReduceMoney(int value);

        public Vector3 GetDropItemPosition();
        
        public int GetMoney();
    }

    public interface ITrader
    {
        public List<LocalItem> GetTraderItems();
        public void SellItem(LocalItem item);
    }
}
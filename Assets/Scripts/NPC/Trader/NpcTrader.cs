using System.Collections.Generic;
using Items;
using UnityEngine;

namespace NPC.Trader
{
    public class NpcTrader : NpcMain, ITrader
    {
        [SerializeField] private List<LocalItem> localItems;

        public List<LocalItem> GetTraderItems() => localItems;

        public void SellItem(LocalItem item)
        {
            localItems.Remove(item);
        }
    }
}
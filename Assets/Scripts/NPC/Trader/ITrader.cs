using System.Collections.Generic;
using Interface;
using Items;
using UnityEngine;

namespace NPC.Trader
{
    public interface ITrade : IFunded
    {
        public Vector3 GetDropItemPosition();

    }

    public interface ITrader
    {
        public List<LocalItem> GetTraderItems();
        public void SellItem(LocalItem item);
    }
}
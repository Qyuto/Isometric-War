using UnityEngine;

namespace Items.Busters
{
    public class ItemBuster : Item
    {
        [SerializeField] private ActiveBuster activeBuster;
        public ActiveBuster ActiveBuster => activeBuster;


    }
}
using Items.Busters;
using UnityEngine;

namespace Player
{
    public class PlayerBusterCollections : MonoBehaviour
    {
        public static PlayerBusterCollections Instance;

        private ActiveBuster[] _localBusters;

        private void Awake()
        {
            if (Instance != null) Destroy(this);

            _localBusters = new ActiveBuster[3];
            Instance = this;
        }

        public void AddBuster(ActiveBuster component, int index)
        {
            ActiveBuster buster = (ActiveBuster)gameObject.AddComponent(component.GetType());
            buster.InitBuster(component);
            _localBusters[index] = buster;
        }

        public void RemoveBuster(int index)
        {
            Destroy(gameObject.GetComponent(_localBusters[index].GetType()));
            _localBusters[index] = null;
        }
    }
}
using UnityEngine;

namespace Items.Busters
{
    public abstract class ActiveBuster : MonoBehaviour
    {
        private bool _isInit;

        public bool IsInit => _isInit;

        public abstract ActiveBuster GetComponent();

        public virtual void InitBuster(ActiveBuster oldBuster)
        {
            // Destroy(oldBuster.gameObject);
            _isInit = true;
        }
    }
}
using System;

namespace Items.Busters
{
    public abstract class ActiveBuster : LocalItem
    {
        protected bool IsInit;

        protected Action OnBusterInitialized;

        public virtual void InitBuster()
        {
            IsInit = true;
            OnBusterInitialized?.Invoke();
        }

        public virtual void Copy(ActiveBuster old)
        {
            itemInfo = old.itemInfo;
        }

        public abstract ActiveBuster GetBuster();
    }
}
using UnityEngine;

namespace Items.Busters
{
    public class BusterMine : ActiveBuster
    {
        [SerializeField] private Mine minePrefab;

        private void Update()
        {
            if(!IsInit) return;
            if (Input.GetKeyDown(KeyCode.Q))
                CreateMine();
        }

        private void CreateMine()
        {
            Instantiate(minePrefab, transform.position, Quaternion.identity);
        }
        
        public override void Copy(ActiveBuster old)
        {
            base.Copy(old);
            if (old is BusterMine mine)
                this.minePrefab = mine.minePrefab;
        }

        public override ActiveBuster GetBuster()
        {
            return this;
        }
    }
}
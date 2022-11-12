using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items.Busters
{
    public class TestBuster : ActiveBuster
    {
        [SerializeField] private Vector2 randomTpRange;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                Teleport();
        }

        private void Teleport()
        {
            if (!IsInit) return;
            transform.position += new Vector3(Random.Range(-randomTpRange.x, randomTpRange.x),
                Random.Range(-randomTpRange.y, randomTpRange.y));
        }

        public override ActiveBuster GetComponent()
        {
            return this;
        }

        public override void InitBuster(ActiveBuster oldBuster)
        {
            base.InitBuster(oldBuster);
            if (oldBuster is TestBuster buster)
                this.randomTpRange = buster.randomTpRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireCube(transform.position, randomTpRange);
        }
    }
}
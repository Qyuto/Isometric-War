using System;
using System.Collections;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items.Busters
{
    public class BusterTeleport : ActiveBuster
    {
        [SerializeField] private Vector2 randomTeleportDistance;

        private PlayerMove _clientMove;
        private Rigidbody2D _clientBody2D;

        private void Awake()
        {
            OnBusterInitialized = BusterInitialized;
        }

        private void BusterInitialized()
        {
            _clientMove = GetComponent<PlayerMove>();
            _clientBody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!IsInit) return;
            if (Input.GetKeyDown(KeyCode.T))
            {
                StopAllCoroutines();
                StartCoroutine(Teleport());
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("BulletDestroyer"))
            {
                StopAllCoroutines();
                _clientMove.enabled = true;
            }
        }

        IEnumerator Teleport()
        {
            Vector3 nextPosition = transform.position +
                                   new Vector3(Random.Range(-randomTeleportDistance.x, randomTeleportDistance.x),
                                       Random.Range(-randomTeleportDistance.y, randomTeleportDistance.y));

            _clientBody2D.velocity = Vector2.zero;
            _clientMove.enabled = false;
            while (Vector2.Distance(transform.position, nextPosition) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, nextPosition, Time.fixedDeltaTime);
                yield return null;
            }

            _clientMove.enabled = true;
        }


        public override void Copy(ActiveBuster old)
        {
            base.Copy(old);
            if (old is not BusterTeleport teleport) return;
            this.randomTeleportDistance = teleport.randomTeleportDistance;
        }

        public override ActiveBuster GetBuster() => GetComponent<BusterTeleport>();
    }
}
using System.Collections;
using Items;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC.Buyer
{
    public class BoxBuyer : MonoBehaviour
    {
        [Range(1, 10)] [SerializeField] private int coinCount;
        [SerializeField] private Vector2 randomCoinPosition;
        [SerializeField] private Coin coin;

        private void GetMoney(int itemCost, Vector3 dropCoinPosition)
        {
            for (int i = 0; i < coinCount; ++i)
            {
                Vector3 newCoinPosition = dropCoinPosition + new Vector3(
                    Random.Range(-randomCoinPosition.x / 2, randomCoinPosition.x / 2),
                    Random.Range(-randomCoinPosition.y / 2, randomCoinPosition.y / 2));
                if (i == coinCount - 1)
                {
                    Instantiate(coin, newCoinPosition, quaternion.identity)
                        .ChangeDropValue(itemCost % coinCount + itemCost / coinCount);
                    break;
                }

                Instantiate(coin, newCoinPosition, quaternion.identity).ChangeDropValue(itemCost / coinCount);
            }
        }

        private IEnumerator DestroySoldItem(Transform itemTransform, int value)
        {
            Vector3 finallySize = new Vector3(0.1f, 0.1f, 0.1f);

            while ((itemTransform.localScale - finallySize).sqrMagnitude > 0.2f)
            {
                itemTransform.localScale =
                    Vector3.Lerp(itemTransform.localScale, finallySize, Time.fixedDeltaTime / 2f);
                itemTransform.position =
                    Vector3.Lerp(itemTransform.position, transform.position, Time.fixedDeltaTime / 2f);
                itemTransform.Rotate(0, 0, 0.7f);
                yield return null;
            }

            Destroy(itemTransform.gameObject);
            GetMoney(value, transform.position);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out WorldItem item)) return;
            StartCoroutine(DestroySoldItem(col.transform, item.GetItemInfo().ItemCost));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, randomCoinPosition);
        }
    }
}
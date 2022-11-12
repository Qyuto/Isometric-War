using Interface;
using Interface.Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    [RequireComponent(typeof(Rigidbody2D), typeof(ItemUI))]
    public class Item : MonoBehaviour, IInteractive, ISelected, IUsableItem
    {
        [SerializeField] private ItemInfo itemInfo;
        [SerializeField] private InteractiveType interactiveType;

        private ItemUI _itemUI;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _itemUI = GetComponent<ItemUI>();
            Undo();
        }

        public Item PickUp()
        {
            Destroy(gameObject);
            return itemInfo.ItemPrefab;
        }

        public void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.AddTorque(Random.Range(-5, 5));
            _rigidbody2D.AddForce(transform.right * Random.Range(-10, 10));
        }

        public ItemInfo GetItemInfo() => itemInfo;
        public InteractiveType GetInteractiveType() => interactiveType;

        public void Select()
        {
            _itemUI.ShowLocalCanvas();
        }

        public void Undo()
        {
            _itemUI.HideLocalCanvas();
        }
    }
}
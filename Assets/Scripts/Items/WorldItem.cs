using Interface;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody2D), typeof(WorldItemUI))]
    public class WorldItem : MonoBehaviour, ISelected, IInteractive, IUsableItem
    {
        [SerializeField] private InteractiveType interactiveType;
        [SerializeField] private ItemInfo itemInfo;

        private Rigidbody2D _rigidbody2D;
        private WorldItemUI _worldItemUI;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _worldItemUI = GetComponent<WorldItemUI>();
        }

        public void Select()
        {
            if (_worldItemUI == null) return;
            _worldItemUI.ShowLocalCanvas();
        }

        public void Undo()
        {
            if (_worldItemUI == null) return;
            _worldItemUI.HideLocalCanvas();
        }

        public InteractiveType GetInteractiveType() => interactiveType;

        public LocalItem PickUp()
        {
            Destroy(gameObject);
            return itemInfo.LocalItem;
        }

        public ItemInfo GetItemInfo() => itemInfo;

        public void InitItem()
        {
            _rigidbody2D.AddTorque(Random.Range(-5f, 5f));
            _rigidbody2D.AddForce(transform.right * Random.Range(-15f, 15f));
        }
    }
}
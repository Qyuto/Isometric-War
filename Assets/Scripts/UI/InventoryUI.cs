using System.Linq;
using UnityEngine;

namespace Interface.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform inventoryImageParent;
        private InventoryImage[] _baseImages;

        private void Awake()
        {
            FindBaseImage();
        }

        private void FindBaseImage()
        {
            _baseImages = inventoryImageParent.GetComponentsInChildren<InventoryImage>().ToArray();
        }

        public void UpdateImage(int index, Sprite sprite)
        {
            _baseImages[index].ChangeSprite(sprite);
        }

        public void DeleteImage(int index)
        {
            _baseImages[index].TurnImage(false);
        }

        public void SetSelectedImage(int index)
        {
            _baseImages[index].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            for (int i = 0; i < _baseImages.Length; i++)
            {
                if (index == i) continue;
                _baseImages[i].transform.localScale = Vector3.one;
            }
        }
    }
}
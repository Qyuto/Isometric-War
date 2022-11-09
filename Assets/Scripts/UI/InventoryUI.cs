using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform inventoryImageParent;
        [SerializeField] private Image imagePrefab;

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
        
        // public void UpdateImage(int index, Sprite sprite) // Old variant
        // {
        //     Image oldImage = _baseImages[index].GetComponentInChildren<Image>();
        //     if (oldImage != null)
        //         Destroy(oldImage.gameObject);
        //     
        //     var res = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, _baseImages[index].transform);
        //     RectTransform uiTransform = res.GetComponent<RectTransform>();
        //
        //     uiTransform.anchorMin = new Vector2(0.5f, 0.5f);
        //     uiTransform.anchorMax = new Vector2(0.5f, 0.5f);
        //     uiTransform.pivot = new Vector2(0.5f, 0.5f);
        //     uiTransform.anchoredPosition = Vector2.zero;
        //     
        //     res.sprite = sprite;
        // }

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
using UnityEngine;
using UnityEngine.UI;

namespace Interface.UI
{
    public class InventoryImage : MonoBehaviour
    {
        [SerializeField] private Image childrenImage;

        public void ChangeSprite( Sprite sprite )
        {
            TurnImage(true);
            childrenImage.sprite = sprite;
        }

        public void TurnImage(bool status)
        {
            childrenImage.gameObject.SetActive(status);
        }
    }
}
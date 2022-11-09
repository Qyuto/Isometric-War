using UnityEngine;

namespace Items
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Canvas localCanvas;

        public void ShowLocalCanvas()
        {
            localCanvas.enabled = true;
        }

        public void HideLocalCanvas()
        {
            localCanvas.enabled = false;
        }
    }
}
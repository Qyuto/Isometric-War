using UnityEngine;

namespace Items
{
    public class WorldItemUI : MonoBehaviour
    {
        private Canvas _localCanvas;

        private void Awake()
        {
            _localCanvas = GetComponentInChildren<Canvas>();
            HideLocalCanvas();
        }

        public void ShowLocalCanvas()
        {
            _localCanvas.enabled = true;
        }

        public void HideLocalCanvas()
        {
            _localCanvas.enabled = false;
        }
    }
}
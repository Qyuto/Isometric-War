using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using NPC.Trader;
using Unity.Mathematics;
using UnityEngine;

namespace UI.Trade
{
    public class TradeUI : MonoBehaviour
    {
        //Todo: Creat trade ui window
        [SerializeField] private Transform imageParent;
        [SerializeField] private TradeImage imagePrefab;
        [SerializeField] private TradeInfoHelperUI infoHelperUI;

        private List<TradeImage> _tradeImages;
        private RectTransform _rectTransform;

        private void Awake()
        {
            gameObject.SetActive(false);
            _rectTransform = GetComponent<RectTransform>();
        }

        public void InitTradeUI(List<LocalItem> traderItems, ITrade playerTrade)
        {
            gameObject.SetActive(true);
            _tradeImages = new List<TradeImage>();

            for (int i = 0; i < traderItems.Count; i++)
            {
                if (i % 4 == 0) PositionCorrection();

                TradeImage newImage = Instantiate(imagePrefab, Vector3.zero, quaternion.identity, imageParent);
                newImage.InitTradeImage(traderItems[i].GetItemInfo(), infoHelperUI, playerTrade);
                newImage.ChangeSprite();
                _tradeImages.Add(newImage);
            }

            StartCoroutine(CheckDistance(playerTrade.GetDropItemPosition(), playerTrade));
        }

        private void PositionCorrection()
        {
            _rectTransform.sizeDelta += new Vector2(0, 40);
            _rectTransform.anchoredPosition =
                new Vector2(_rectTransform.anchoredPosition.x, -_rectTransform.sizeDelta.y / 2);
        }


        private IEnumerator CheckDistance(Vector3 initPosition, ITrade dTrade)
        {
            while ((initPosition - dTrade.GetDropItemPosition()).sqrMagnitude < 6f)
                yield return null;

            gameObject.SetActive(false);
            foreach (var image in _tradeImages)
                Destroy(image.gameObject);
            _tradeImages.Clear();
        }
    }
}
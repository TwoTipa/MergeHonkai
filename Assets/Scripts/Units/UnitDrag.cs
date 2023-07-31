using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Units
{
    [RequireComponent(typeof(RectTransform), typeof(Unit))]
    public class UnitDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private GameController gameController;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            gameController = ServiceLocator.ServiceLocator.Current.Get<GameController>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var gameStop = gameController.GameStop;
            if (gameStop) return;
            var parent = transform.parent;
            parent.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var gameStop = gameController.GameStop;
            if (gameStop) return;
            rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var gameStop = gameController.GameStop;
            if (gameStop) return;
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
using System;
using GameResources;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using YG;

namespace UI.Resources
{
    public class Resource
    {
        protected TextMeshProUGUI UiComponent;
        protected GameResource Resources;

        private int targetValue = 0;
        private float currentValue = 0;

        public Resource(TextMeshProUGUI ui, GameResource resource)
        {
            UiComponent = ui;
            Resources = resource;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                var step = targetValue * 0.01f + 10f;
                var newValue =  (int)Mathf.MoveTowards( currentValue, targetValue, step);
                currentValue = newValue;
                UiComponent.text = currentValue.ToString();

            }).AddTo(UiComponent.gameObject);
            Resources.Count.Subscribe(_ =>
            {
                targetValue = Resources.Count.Value;
            }).AddTo(UiComponent.gameObject);
        }
    }
}
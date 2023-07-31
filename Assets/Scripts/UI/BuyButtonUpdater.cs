using System;
using TMPro;
using UniRx;
using Units;
using UnityEngine;

namespace UI
{
    public class BuyButtonUpdater : MonoBehaviour
    {
        [SerializeField] private string formate;
        private TextMeshProUGUI textUi;
        private UnitBuySystem buySystem;
        private void Start()
        {
            textUi = GetComponent<TextMeshProUGUI>();
            buySystem = ServiceLocator.ServiceLocator.Current.Get<UnitBuySystem>();
            buySystem.Price.OnChanged += UpdateText;
            UpdateText(buySystem.Price.Value);
        }

        private void OnDestroy()
        {
            buySystem.Price.OnChanged -= UpdateText;
        }

        private void UpdateText(int val)
        {
            textUi.text = String.Format(formate, val);
        }
    }
}
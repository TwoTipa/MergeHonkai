using System;
using System.Collections.Generic;
using ServiceLocator;
using TMPro;
using UI.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameResources
{
    public class ResourcesController : MonoBehaviour, IService
    {
        [SerializeField] private TextMeshProUGUI moneyUi;
        [SerializeField] private TextMeshProUGUI gemUi;
        
        public readonly Dictionary<string, GameResource> Resources = new();

        private void Start()
        {
            AddResource(moneyUi,new Money("Money"));
            AddResource(gemUi,new Gem("Gem"));
            
            
            Resources.TryGetValue("Money", out var money);
            if (money.Count.Value <= 0)
            {
                money.Count.Value = 0;
                money.Add(100);
            }
        }

        private void Update()
        {
            //ButtonController();
        }

        private void ButtonController()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Resources.TryGetValue("Money", out var money);
                money.Add(100000000);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Resources.TryGetValue("Money", out var money);
                if(!money.TrySpend(100))return;
            }
        }

        private void AddResource(TextMeshProUGUI ui, GameResource res)
        {
            Resources.Add(res.Name, res);
            new Resource(ui, res);
        }
    }
}
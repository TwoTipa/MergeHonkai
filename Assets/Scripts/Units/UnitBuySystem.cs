using Ads;
using Fight;
using GameResources;
using ReactiveProperty;
using ServiceLocator;
using UnityEngine;

namespace Units
{
    public class UnitBuySystem : MonoBehaviour, IService
    {
        [SerializeField] private Unit unitForBuy;
        [SerializeField] private GameObject rewardWin;
        public MyReactiveProperty<int> Price { get; private set; } = new MyReactiveProperty<int>(0);
        private UnitSlot[] slots;

        private int count = 0;

        private void Start()
        {
            slots = GetComponentsInChildren<UnitSlot>();
            Price.Value = 100;
            if (PlayerPrefs.HasKey("Price"))
            {
                Price.Value = PlayerPrefs.GetInt("Price");
            }
        }

        public void BuyUnit()
        {
            if(ServiceLocator.ServiceLocator.Current.Get<GameController>().GameStop) return;
            var slot = FindFreeSlot();
            if (slot == null) return;
            var locator = ServiceLocator.ServiceLocator.Current;
            var money = locator.Get<ResourcesController>().Resources["Money"];
            if (!money.TrySpend(Price.Value))
            {
                //locator.Get<Ad>().ShowAd(Price.Value);
                rewardWin.SetActive(true);
                return;
            }
            Price.Value += 10;
            PlayerPrefs.SetInt("Price", Price.Value);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt(slot.transform.name + "Unit", unitForBuy.Level);
            PlayerPrefs.Save();
            count++;
            Instantiate(unitForBuy, slot.transform).name = "Unit"+count;
        }

        private UnitSlot FindFreeSlot()
        {
            foreach (var slot in slots)
            {
                if (slot.transform.childCount > 0) continue;
                
                return slot;
            }

            return null;
        }
    }
}
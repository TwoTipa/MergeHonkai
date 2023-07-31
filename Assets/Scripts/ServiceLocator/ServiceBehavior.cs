using System;
using Ads;
using Fight;
using GameResources;
using Sound;
using UI;
using Units;
using UnityEngine;

namespace ServiceLocator
{
    public class ServiceBehavior : MonoBehaviour
    {
        [SerializeField] private ResourcesController resourcesController;
        [SerializeField] private UnitBuySystem buySystem;
        [SerializeField] private FightSystem fightSystem;
        [SerializeField] private GameController gameController;
        [SerializeField] private Ad ad;
        [SerializeField] private UiSwitcher uiSwitcher;
        [SerializeField] private SoundController soundController;

        private void Start()
        {
            new ServiceLocator().Initialization();
            
            ServiceLocator.Current.Register<ResourcesController>(resourcesController);
            ServiceLocator.Current.Register<UnitBuySystem>(buySystem);
            ServiceLocator.Current.Register<FightSystem>(fightSystem);
            ServiceLocator.Current.Register<GameController>(gameController);
            ServiceLocator.Current.Register<SoundController>(soundController);
            ServiceLocator.Current.Register<Ad>(ad);
            ServiceLocator.Current.Register<UiSwitcher>(uiSwitcher);
            
        }
    }
}
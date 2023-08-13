using System;
using GameResources;
using ServiceLocator;
using UnityEngine;
using YG;

namespace Ads
{
    public class Ad : MonoBehaviour, IService
    {
        [SerializeField] private YandexGame yandexSDK;

        public void ShowAd(int money)
        {
            yandexSDK._RewardedShow(0);
        }

        public void ShowFullScreen()
        {
            yandexSDK._FullscreenShow();
        }

        private void OnEnable()
        {
            yandexSDK.CloseVideoAd.AddListener(onAdClose);
        }

        private void onAdClose()
        {
            ServiceLocator.ServiceLocator.Current.Get<ResourcesController>().Resources["Money"].Add(1000);
        }
    }
}
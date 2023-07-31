using System;
using ServiceLocator;
using UnityEngine;
using YG;

namespace Ads
{
    public class Ad : MonoBehaviour, IService
    {
        [SerializeField] private YandexGame yandexSDK;

        public void ShowAd()
        {
            yandexSDK._RewardedShow(0);
        }

        public void ShowFullScreen()
        {
            yandexSDK._FullscreenShow();
        }
    }
}
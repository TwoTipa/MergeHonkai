using System;
using GameResources;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Enemyes
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image spriteSlot;
        [SerializeField] private Slider hpBar;
        [SerializeField] private TextMeshProUGUI nameUi;


        [SerializeField] private Sprite sprite;
        [SerializeField] private string name;
        [SerializeField] private int maxHp;
        [SerializeField] private int MoneyReward;
        [SerializeField] private int GemReward;

        private float hp;

        public void ResetHp()
        {
            hp = maxHp;
        }
        
        public bool TakeDamage(int value)
        {
            hp -= value;
            if (hp <= 0)
            {
                Dead();
                return true;
            }

            return false;
        }

        private void Update()
        {
            hpBar.value = Mathf.Lerp(hpBar.value, hp, 0.1f);
        }

        private void Start()
        {
            spriteSlot.sprite = sprite;
            hpBar.maxValue = maxHp;
            hp = maxHp;
            hpBar.value = hp;
            nameUi.text = name;
        }

        private void Dead()
        {
            GetReward();
            Destroy(gameObject);
        }

        private void GetReward()
        {
            var resources = ServiceLocator.ServiceLocator.Current.Get<ResourcesController>();
            resources.Resources["Money"].Add(MoneyReward);
            resources.Resources["Gem"].Add(GemReward);
        }
    }
}
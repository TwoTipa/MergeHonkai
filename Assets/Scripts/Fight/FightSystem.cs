using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Enemyes;
using ServiceLocator;
using Sound;
using UI;
using UniRx;
using UnityEngine;
using Unit = Units.Unit;

namespace Fight
{
    public class FightSystem : MonoBehaviour, IService
    {
        public static event Action<bool> EndLevel;
        
        [SerializeField] private GameObject unitZone;
        [SerializeField] private GameObject enemyZone;
        [SerializeField] private Canvas losePopup;
        [SerializeField] private Canvas winPopup;
        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip loseSound;
        private List<Unit> units;
        private Enemy[] enemies;
        private CompositeDisposable disposable = new();

        private bool _isFight = false;
        
        public void StartFight()
        {
            if (_isFight) return;
            _isFight = true;
            ServiceLocator.ServiceLocator.Current.Get<GameController>().Stop();
            units = unitZone.GetComponentsInChildren<Unit>().ToList();
            enemies = enemyZone.GetComponentsInChildren<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.ResetHp();
            }
            foreach (var unit in units)
            {
                unit.Reset();
            }
            float timer = 0;
            int index = 0;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                bool endFight = true;
                foreach (var item in units)
                {
                    if (item.IsActive)
                    {
                        endFight = false;
                    }
                }
                if (endFight)
                {
                    EndFight();
                    disposable.Clear();
                    return;
                }
                
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    if (index > units.Count-1) index = 0;
                    bool res = units[index].CheckEnemy();
                    if (res)
                    {
                        timer = 0;
                    }
                    index++;
                }
            }).AddTo(disposable);
        }

        private void WinFight()
        {
            winPopup.gameObject.SetActive(true);
            SoundController.Instance.PlayClip(winSound);
        }

        private void LoseFight()
        {
            losePopup.gameObject.SetActive(true);
            SoundController.Instance.PlayClip(loseSound);
        }

        private void EndFight()
        {
            _isFight = false;
            ServiceLocator.ServiceLocator.Current.Get<GameController>().Play();
            disposable.Clear();
            enemies = enemyZone.GetComponentsInChildren<Enemy>();
            bool result = enemies.Length <= 0;
            if (result)
            {
                WinFight();
            }
            else
            {
                LoseFight();
            }
            EndLevel?.Invoke(result);
        }
    }
}
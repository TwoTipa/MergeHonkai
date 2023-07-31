using System;
using Enemyes;
using Sound;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        public static event Action<Unit, Sprite> UnitUpgrade;
        public int Level { get; private set; } = 0;
        public int AtackCount { get; private set; }
        [SerializeField] private Image spriteSlot;
        [SerializeField] private Slider hpBar;
        [SerializeField] private TextMeshProUGUI nameUi;
        [SerializeField] private TextMeshProUGUI attackCountText;
        [SerializeField] private AudioClip punchSound;
        [SerializeField] private AudioClip UpgradeSound;

        [SerializeField] private UnitLevels[] levelSetting;
        private CompositeDisposable attackAnim = new();
        private int MaxLevel = 0;
        public bool IsActive => _isActive;
        private bool _isActive = true;
        
        
        public void Reset()
        {
            AtackCount = levelSetting[Level].atackCount;
            _isActive = true;
            attackCountText.text = AtackCount.ToString();
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void LoadLevel(int level)
        {
            Level = level;
            SetSetting(Level);
        }
        
        public void Upgrade(Unit resourceUnit)
        {
            if (Level >= levelSetting.Length - 1)
            {
                return;   
            }
            Destroy(resourceUnit.gameObject);
            Level++;
            SetSetting(Level);
            SoundController.Instance.PlayClip(UpgradeSound);
            UnitUpgrade?.Invoke(this, levelSetting[Level].spleshArt);
        }

        public bool CheckEnemy()
        {
            bool ret = !_isActive;
            if (AtackCount <= 0) ret = true;
            var hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, LayerMask.GetMask("Enemy"));
            if (hit.transform == null)
            {
                Disable();
                return false;
            }
            var enemy = hit.transform.GetComponent<EnemySlot>();
            bool isEnemy = enemy.IsFree();
            if (isEnemy) ret = true;
            if (ret)
            {
                Disable();
                return false;
            }
            Attack(enemy);
            return true;
        }
        
        public void Attack(EnemySlot enemy)
        {
            
            enemy.TakeDamage(levelSetting[Level].dmg);
            AtackCount -= 1;
            attackCountText.text = AtackCount.ToString();

            attackAnim.Clear();
            bool dir = true;
            float dist = 0;
            var pos = transform.position;
            Vector3 targetUp = pos + Vector3.up * 10;
            Vector3 startPos = pos;
            SoundController.Instance.PlayClip(punchSound);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                var delta = Time.deltaTime;
                if (dir)
                {
                    transform.transform.position = Vector3.Lerp(transform.transform.position, targetUp, 10f * delta);
                    dist += delta;
                    if ((targetUp - transform.transform.position).magnitude <= 1)
                    {
                        dir = !dir;
                        dist = 0f;
                    }
                }
                else
                {
                    transform.transform.position = Vector3.Lerp(transform.transform.position, startPos, 10f * delta);
                    dist += delta;
                    if ((startPos - transform.transform.position).magnitude <= 1)
                    {
                        transform.localPosition = Vector3.zero;
                        attackAnim.Clear();
                        dist = 0f;
                    }
                }
            }).AddTo(attackAnim);
        }
        
        private void Start()
        {
            SetSetting(Level);
        }

        private void SetSetting(int i)
        {
            var setting = levelSetting[i];
            spriteSlot.sprite = setting.sprite;
            nameUi.text = setting.name;
            hpBar.maxValue = setting.hp;
            AtackCount = setting.atackCount;
            attackCountText.text = AtackCount.ToString();
        }
    }
}
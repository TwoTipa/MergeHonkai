using System;
using System.Timers;
using Sound;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class EpicUnlockWin : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Canvas canvas;
        [SerializeField] private AudioClip unlock;

        private CompositeDisposable disposable = new();
        private int Level = 0;
        
        
        private void OnEnable()
        {
            Unit.UnitUpgrade += UnitOnUnitUpgrade;
        }

        private void OnDisable()
        {
            Unit.UnitUpgrade -= UnitOnUnitUpgrade;
        }

        private void Start()
        {
            Level = PlayerPrefs.GetInt("EpicWinLevel");
        }

        private void UnitOnUnitUpgrade(Unit obj, Sprite image)
        {
            if (!(Level < obj.Level)) return;
            Level++;
            PlayerPrefs.SetInt("EpicWinLevel", Level);
            PlayerPrefs.Save();
            Initialize(image);
            SoundController.Instance.PlayClip(unlock);
            canvas.gameObject.SetActive(true);
        }

        public void Initialize(Sprite unitImage)
        {
            image.sprite = unitImage;
            image.color = Color.black;

            float timer = 0f;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    image.color = Color.white;
                }

                if (timer >= 3f)
                {
                    canvas.gameObject.SetActive(false);
                    disposable.Clear();
                }
            }).AddTo(disposable);
        }
    }
}
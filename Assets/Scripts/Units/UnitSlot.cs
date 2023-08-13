using System;
using Sound;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Units
{
    public class UnitSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private AudioClip failedMerge;
        
        public void OnDrop(PointerEventData eventData)
        {
            var gameStop = ServiceLocator.ServiceLocator.Current.Get<GameController>().GameStop;
            if(gameStop) return;
            var unit = eventData.pointerDrag.transform;
            if (transform.childCount > 0)
            {
                TryMerge(unit.GetComponent<Unit>(), transform.GetComponentInChildren<Unit>());
                return;
            }
            unit.SetParent(transform);
            
            
            SaveUnit();
        }

        private void OnDestroy()
        {
            // var unit = GetComponentInChildren<Unit>();
            // if (unit != null)
            // {
            //     SaveUnitM(unit);
            // }
        }

        private void OnApplicationQuit()
        {
            SaveUnit();
        }

        private float timer = 0f;
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = 0f;
                SaveUnit();
            }
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(transform.name + "Unit")) return;
            var unitLvl = PlayerPrefs.GetInt(transform.name + "Unit");
            if (unitLvl >= 0)
            {
                var loadUnit = Instantiate(unitPrefab, transform);
                loadUnit.LoadLevel(unitLvl);
            }
        }

        private void SaveUnit()
        {
            var unit = GetComponentInChildren<Unit>();
            if (unit == null)
            {
                PlayerPrefs.SetInt(transform.name + "Unit", -1);
                return;
            }
            PlayerPrefs.SetInt(transform.name + "Unit", unit.Level);
            PlayerPrefs.Save();
        }
        
        private void TryMerge(Unit dragUnit, Unit inSlotUnit)
        {
            bool success = dragUnit.Level == inSlotUnit.Level;

            if (dragUnit == inSlotUnit)
            {
                success = false;
            }

            if (!success)
            {
                SoundController.Instance.PlayClip(failedMerge);
                return;
            }
            
            inSlotUnit.Upgrade(dragUnit);
            
            SaveUnit();
        }
    }
}
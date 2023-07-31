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
        }

        private void OnDestroy()
        {
            var unit = GetComponentInChildren<Unit>();
            if (unit != null)
            {
                SaveUnit(unit);
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

        private void SaveUnit(Unit unit)
        {
            Debug.Log("Сохраняю");
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
        }
    }
}
using System;
using UnityEngine;

namespace Enemyes
{
    public class LevelBuilder : MonoBehaviour
    {
        private EnemySlot[] slots;

        private void Awake()
        {
            slots = GetComponentsInChildren<EnemySlot>();
        }

        public void BuildLevel(LevelSetting level)
        {
            ClearLevel();
            for (int i = 0; i < level.enemyOnSlots.Length; i++)
            {
                Instantiate(level.enemyOnSlots[i], slots[i].transform);
            }
        }

        private void ClearLevel()
        {
            foreach (var slot in slots)
            {
                for (int i = slot.transform.childCount - 1; i >= 0; i--)
                {
                    Transform child = slot.transform.GetChild(i);
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
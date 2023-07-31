using System;
using Fight;
using UnityEngine;

namespace Enemyes
{
    public class EnemySlot : MonoBehaviour
    {
        private void OnEnable()
        {
            FightSystem.EndLevel += FightSystemOnEndLevel;
        }

        private void OnDisable()
        {
            FightSystem.EndLevel -= FightSystemOnEndLevel;
        }

        private void FightSystemOnEndLevel(bool obj)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        public bool IsFree()
        {
            var child = transform.GetComponentInChildren<Enemy>();
            if (child == null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                return true;
            }
            return false;
        }
        
        public bool TakeDamage(int obj)
        {
            var ret = transform.GetComponentInChildren<Enemy>().TakeDamage(obj);
            if (ret)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            return ret;
        }
    }
}
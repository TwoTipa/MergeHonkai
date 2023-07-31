using System;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitLevels", menuName = "Units/UnitLevels", order = 0)]
    public class UnitLevels : ScriptableObject
    {
        public Sprite sprite;
        public Sprite spleshArt;
        public string name;
        public int dmg;
        public int hp;
        public int atackCount;
        public float fireRate;
    }
}
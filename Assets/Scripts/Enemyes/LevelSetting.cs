using UnityEngine;
using UnityEngine.Serialization;

namespace Enemyes
{
    [CreateAssetMenu(fileName = "LevelSetting", menuName = "Level/LevelSetting", order = 0)]
    public class LevelSetting : ScriptableObject
    {
        public Enemy[] enemyOnSlots;
        public Sprite background;

    }
}
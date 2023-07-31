using UnityEngine;

namespace Enemyes
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "Level/LevelList", order = 0)]
    public class LevelList : ScriptableObject
    {
        public LevelSetting[] levels;

    }
}
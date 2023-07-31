using System;
using Ads;
using Fight;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Enemyes
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private LevelList levelList;
        [SerializeField] private Image background;
        private LevelBuilder builder;
        private int lvlNum = 0;
        private bool infinityMode = false;

        private int levelCompleteCount = 0;
        
        private void Start()
        {
            builder = GetComponent<LevelBuilder>();
            var lvl = PlayerPrefs.GetInt("Level");
            lvlNum = lvl;
            BuildLevel();
        }

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
            if (obj)
            {
                NextLevel();
            }
            else
            {
                RestartLevel();
            }
        }

        private void NextLevel()
        {
            lvlNum++;
            BuildLevel();
            PlayerPrefs.SetInt("Level", lvlNum);
            PlayerPrefs.Save();
        }

        private void RestartLevel()
        {
            BuildLevel();
        }

        private void BuildLevel()
        {
            //ServiceLocator.ServiceLocator.Current.Get<UiSwitcher>().SwitchCanvas(UiMods.Game);
            if (infinityMode || levelList.levels.Length-1 < lvlNum)
            {
                infinityMode = true;
                StartInfinityLevel();
                return;
            }

            background.sprite = levelList.levels[lvlNum].background;
            builder.BuildLevel(levelList.levels[lvlNum]);
            levelCompleteCount++;
            if (levelCompleteCount >= 2)
            {
                levelCompleteCount = 0;
                ServiceLocator.ServiceLocator.Current.Get<Ad>().ShowFullScreen();
            }
        }

        private void StartInfinityLevel()
        {
            
        }
    }
}
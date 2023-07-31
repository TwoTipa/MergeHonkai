using System;
using System.Collections.Generic;
using ServiceLocator;
using UnityEngine;

namespace UI
{
    public class UiSwitcher : MonoBehaviour, IService
    {
        [SerializeField] private UiMods[] key;
        [SerializeField] private UiMod[] value;
        private Canvas[] allCanvas;
        
        private readonly Dictionary<UiMods, UiMod> modList = new();
        
        private void Awake()
        {
            allCanvas = GetComponentsInChildren<Canvas>();
            for (int i = 0; i < key.Length; i++)
            {
                modList.Add(key[i], value[i]);
            }
        }

        public void SwitchCanvas(UiMods mod)
        {
            allCanvas = GetComponentsInChildren<Canvas>();
            foreach (var canvas in allCanvas)
            {
                GameObject o;
                (o = canvas.gameObject).SetActive(false);
                Destroy(o);
            }
            foreach (var canvas in modList[mod].canvases)
            {
                Instantiate(canvas, transform).gameObject.SetActive(true);
            }
        }
    }

    [CreateAssetMenu(fileName = "UiGameMod", menuName = "Ui/UiMod", order = 0)]
    public class UiMod : ScriptableObject
    {
        public Canvas[] canvases;
    }

    [Serializable]
    public enum UiMods
    {
        Game,
        Market,
        Lose,
        Win,
        epicWin
    }
}
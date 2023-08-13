using System;
using UnityEngine;

namespace Sound
{
    public class FocusSoundController : MonoBehaviour
    {
        [SerializeField] private SoundController _soundController;
        void OnApplicationFocus(bool hasFocus)
        { 
            Silence(!hasFocus);
        }

        void OnApplicationPause(bool isPaused)
        {
            Silence(isPaused);
        }

        private void Silence(bool silence)
        {
            _soundController.SwitchSound(!silence);
        }
        
    }
}
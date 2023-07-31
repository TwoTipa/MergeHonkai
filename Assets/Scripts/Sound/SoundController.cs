using System;
using ServiceLocator;
using UnityEngine;
using UnityEngine.Playables;

namespace Sound
{
    public class SoundController : MonoBehaviour, IService
    {
        [SerializeField] private float masterVolume;
        [SerializeField] private float musicVolume;
        [SerializeField] private AudioSource sound;
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioClip fonMusic;

        private bool soundOn = true;
        private bool musicOn = true;
        
        public static SoundController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void PlayClip(AudioClip clip)
        {
            sound.PlayOneShot(clip, masterVolume);
        }

        public void ChangeSoundVolume(float volume)
        {
            masterVolume = volume;
        }

        public void SwitchSound(bool volume)
        {
            if (soundOn)
            {
                masterVolume = 0f;
                soundOn = false;
            }
            else
            {
                masterVolume = 1f;
                soundOn = true;
            }
        }

        public void SwitchMusic(bool volume)
        {
            if (musicOn)
            {
                musicVolume = 0f;
                music.volume = 0f;
                musicOn = false;
            }
            else
            {
                musicVolume = 0.4f;
                music.volume = 0.4f;
                musicOn = true;
            }
        }
        
        public void ChangeMusicVolume(float volume)
        {
            musicVolume = volume;
        }

        private void Start()
        {
            music.loop = true;
            music.clip = fonMusic;
            music.volume = musicVolume;
            music.Play();
        }
    }
}
using System;
using ServiceLocator;
using UnityEngine;
using UnityEngine.Playables;

namespace Sound
{
    public class SoundController : MonoBehaviour, IService
    {
        [SerializeField] private GameObject musicIcon;
        [SerializeField] private GameObject musicIconOff;
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
            musicIcon.SetActive(volume);
            musicIconOff.SetActive(!volume);
            masterVolume = volume ? 0.4f : 0;
            music.volume = masterVolume;
            soundOn = volume;
        }

        public void SwitchMusic(bool volume)
        {
            masterVolume = volume ? 0.4f : 0;
            music.volume = masterVolume;
            soundOn = volume;
        }
        
        public void ChangeMusicVolume(float volume)
        {
            musicVolume = volume;
        }

        private void Start()
        {
            music.loop = true;
            music.clip = fonMusic;
            music.volume = masterVolume;
            music.Play();
        }
    }
}
using System;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        public Sound[] sounds;
        public AudioMixerGroup soundEffectsMixer;
        public AudioMixerGroup musicEffectsMixer;
        private void Awake()
        {
            #region Singleton

            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            #endregion

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
            }
        }
        
        public void Play(Sound.Names soundName)
        {
            var s = Array.Find(sounds, item => item.name == soundName); //find if there is a sound called with the name specified
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " has not found a source to play!");
                return;
            }

            if (s.name == Sound.Names.MainMenuTheme ||s.name == Sound.Names.BattleTheme ||s.name == Sound.Names.BossTheme )
            {
                s.source.outputAudioMixerGroup = musicEffectsMixer;
            }
            else
            {
                s.source.outputAudioMixerGroup = soundEffectsMixer;
            }
            if (!s.canBeOverridden && s.source.isPlaying)
            {
                return;
            }
            
            if (s.isRandom)
            {
                // s.source.volume = s.volume + Random.Range(0, s.volumeVariance);
                s.source.pitch = s.pitch + Random.Range(0, s.pitchVariance);
            }
            else
            {
                s.source.volume = s.volume;
                // s.source.pitch = s.pitch;    
            }
            s.source.Play();
        }

        public void StopSound(Sound.Names soundName)
        {
            var s = Array.Find(sounds, item => item.name == soundName);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " has not found a source to play!");
                return;
            }

            s.source.Stop();

        }
    }

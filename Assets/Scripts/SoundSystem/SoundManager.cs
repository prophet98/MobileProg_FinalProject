using System;
using System.Collections;
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

            s.source.volume = 1.0f;
            if (!s.canBeOverridden && s.source.isPlaying)
            {
                return;
            }
            if (s.name == Sound.Names.MainMenuTheme ||s.name == Sound.Names.BattleTheme01 ||s.name == Sound.Names.HubTheme ||s.name == Sound.Names.BattleTheme03 ||s.name == Sound.Names.BossTheme )
            {
                foreach (var sound in sounds)
                {
                    if (sound.name.ToString().Contains("Theme") && sound.source.isPlaying)
                    { 
                        StartCoroutine(StartFade(sound));
                    }
                }
                s.source.outputAudioMixerGroup = musicEffectsMixer;
            }
            else
            {
                s.source.outputAudioMixerGroup = soundEffectsMixer;
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

        private IEnumerator StartFade(Sound sound)
        {
            float currentTime = 0;
            float start = sound.source.volume;
            
            while (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
                sound.source.volume = Mathf.Lerp(start, 0.0f, currentTime /1f);
                yield return null;
            }
            StopSound(sound.name);
            yield break;
        }
        
    }

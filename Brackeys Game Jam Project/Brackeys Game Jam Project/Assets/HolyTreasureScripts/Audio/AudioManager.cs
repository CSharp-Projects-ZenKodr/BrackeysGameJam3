using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.HolyTreasureScripts.Audio {
    public class AudioManager : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The instance of this class.
        /// </summary>
        public static AudioManager Instance;

        /// <summary>
        /// The collection of sounds this audio manager will use.
        /// </summary>
        public Sound[] sounds;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("There is more than one instance of the Audio Manager in the scene!");
                Destroy(gameObject);
                return;
            }

            //DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds) {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loopSound;
            }
        }

        private void Start() {
            //NOTE: Play Music here
        }

        /// <summary>
        /// Plays a certain sound.
        /// </summary>
        /// <param name="soundName">
        /// The name of the sound you want to play.
        /// </param>
        public AudioSource PlaySound(string soundName) {
            Sound s = Array.Find(sounds, sound => sound.soundName == soundName);
            if (s == null) {
                Debug.LogWarning(soundName + " did not return one of the sounds.");
                return null;
            }
            s.source.Play();
            return s.source;
        }

        /// <summary>
        /// Plays a certain sound with the given parameters.
        /// </summary>
        /// <param name="soundName">
        /// The name of the sound you want to play.
        /// </param>
        /// <param name="intendedVolume">
        /// The intended volume of the sound.
        /// </param>
        /// <param name="intendedPitch">
        /// The intended pitch of the sound.
        /// </param>
        public AudioSource PlaySound(string soundName, float intendedVolume, float intendedPitch) {
            Sound s = Array.Find(sounds, sound => sound.soundName == soundName);
            if (s == null) {
                Debug.LogWarning("Sound name given did not return one of the sounds.");
                return null;
            }
            s.source.volume = intendedVolume;
            s.source.pitch = intendedPitch;
            s.source.Play();
            return s.source;
        }

        /// <summary>
        /// Stops a certain sound.
        /// </summary>
        /// <param name="soundName">
        /// The name of the sound you're stopping.
        /// </param>
        public void StopSound(string soundName) {
            Sound s = Array.Find(sounds, sound => sound.soundName == soundName);
            if (s == null) {
                Debug.LogWarning("Sound name given did not return one of the sounds.");
                return;
            }
            s.source.Stop();
        }
    }
}
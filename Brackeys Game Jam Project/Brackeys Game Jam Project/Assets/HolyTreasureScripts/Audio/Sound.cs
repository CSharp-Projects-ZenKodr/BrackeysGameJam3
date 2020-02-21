using UnityEngine;
using UnityEngine.Audio;

namespace Assets.HolyTreasureScripts.Audio {
    [System.Serializable]
    public class Sound {

        #region Variables
        /// <summary>
        /// The clip this sound plays off of.
        /// </summary>
        public AudioClip clip;
        /// <summary>
        /// The AudioSource component this sound is stored in.
        /// </summary>
        public AudioSource source { get; set; }
        /// <summary>
        /// The name of the sound.
        /// </summary>
        public string soundName;
        /// <summary>
        /// The volume of this sound.
        /// </summary>
        [Range(0f, 1f)]
        public float volume;
        /// <summary>
        /// The pitch of this sound.
        /// </summary>
        [Range(0.1f, 3f)]
        public float pitch;
        /// <summary>
        /// Return true if you want the sound to loop, or false if not.
        /// </summary>
        public bool loopSound;
        #endregion

    }
}
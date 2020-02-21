using Assets.HolyTreasureScripts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class TreasureChest : DigPrize {

        #region Variables
        /// <summary>
        /// The volume of this sound.
        /// </summary>
        [Range(0f, 1f)]
        public float collectVolume;
        /// <summary>
        /// The pitch the money sound is at when player collects dough.
        /// </summary>
        [Range(0.1f, 3f)]
        public float collectPitch;

        /// <summary>
        /// The Animator attached to this treasure chest.
        /// </summary>
        private Animator attachedAnimator;
        /// <summary>
        /// The audio manager in the scene.
        /// </summary>
        private AudioManager audioMan;
        /// <summary>
        /// Return true if player got the treasure, or false if not.
        /// </summary>
        private bool treasureGot = false;
        #endregion

        private void Start() {
            GetDigPrizeData();
            attachedAnimator = GetComponent<Animator>();
            audioMan = AudioManager.Instance;
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!treasureGot) {
                    audioMan.PlaySound("Money", collectVolume, collectPitch);
                    attachedAnimator.SetTrigger("Open");

                    PlayerInventory.Instance.UpdateMoney(prizeValue);
                    treasureGot = true;
                }
            }
        }
    }
}
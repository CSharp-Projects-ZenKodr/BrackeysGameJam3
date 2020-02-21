using Assets.HolyTreasureScripts.Audio;
using Assets.HolyTreasureScripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class MoneyBag : DigPrize {
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
        /// The maximum int value (exclusive) this bag could be worth.
        /// </summary>
        public int exclusiveMaxValue = 201;
        /// <summary>
        /// The minimum int value (inclusive this bag could be worth)
        /// </summary>
        public int inclusiveMinValue = 100;


        /// <summary>
        /// The money particle system attached to this money bag.
        /// </summary>
        private ParticleSystem psMoney;
        /// <summary>
        /// The Audio Manager in the scene.
        /// </summary>
        private AudioManager audioMan;
        /// <summary>
        /// Return true if player got the treasure, or false if not.
        /// </summary>
        private bool treasureGot = false;
        #endregion

        private void Start() {
            GetDigPrizeData();
            psMoney = transform.GetChild(0).GetComponent<ParticleSystem>();
            prizeValue = UnityEngine.Random.Range(inclusiveMinValue, exclusiveMaxValue);
            audioMan = AudioManager.Instance;
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!treasureGot) {
                    audioMan.PlaySound("Money", collectVolume, collectPitch);
                    PlayerInventory.Instance.UpdateMoney(prizeValue);
                    psMoney.transform.parent = null;
                    psMoney.Play();
                    Destroy(gameObject);
                    Destroy(psMoney.gameObject, 5f);

                    treasureGot = true;
                }
            }
        }
    }
}
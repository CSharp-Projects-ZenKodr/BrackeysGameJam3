using Assets.HolyTreasureScripts.Audio;
using Assets.HolyTreasureScripts.GameStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class Mine : DigPrize {

        #region Variables
        /// <summary>
        /// A particle system simulating a big explosion.
        /// </summary>
        public GameObject bigExplosionEffect;

        /// <summary>
        /// The Game Manager in the scene.
        /// </summary>
        private GameManager gameMan;
        /// <summary>
        /// The Audio Manager in the scene.
        /// </summary>
        private AudioManager audioMan;
        /// <summary>
        /// Return true if the mine exploded, or false if not.
        /// </summary>
        private bool mineExploded = false; 
        #endregion

        private void Start() {
            GetDigPrizeData();
            gameMan = GameManager.Instance;
            audioMan = AudioManager.Instance;
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!mineExploded) {
                    audioMan.PlaySound("Explosion");

                    gameMan.UpdateMineStatus(gameMan.minesExploded + 1);

                    GameObject eps = Instantiate(bigExplosionEffect, transform.position, transform.rotation);

                    Destroy(eps, 5f);
                    Destroy(gameObject);

                    //Debug.Log("EXPLODE!");
                    mineExploded = true;
                }
            }
        }
    }
}
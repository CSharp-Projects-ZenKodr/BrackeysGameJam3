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
        private GameManager manager;
        /// <summary>
        /// Return true if the mine exploded, or false if not.
        /// </summary>
        private bool mineExploded = false; 
        #endregion

        private void Start() {
            GetDigPrizeData();
            manager = GameManager.Instance;
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!mineExploded) {
                    manager.UpdateMineStatus(manager.minesExploded + 1);

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
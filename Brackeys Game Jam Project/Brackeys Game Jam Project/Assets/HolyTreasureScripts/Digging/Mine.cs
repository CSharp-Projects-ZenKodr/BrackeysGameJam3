using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class Mine : DigPrize {

        #region Variables
        /// <summary>
        /// Return true if the mine exploded, or false if not.
        /// </summary>
        private bool mineExploded = false; 
        #endregion

        private void Start() {
            GetDigPrizeData();
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!mineExploded) {
                    Debug.Log("EXPLODE!");
                    mineExploded = true;
                }
            }
        }
    }
}
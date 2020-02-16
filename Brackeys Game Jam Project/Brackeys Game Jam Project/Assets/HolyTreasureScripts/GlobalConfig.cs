using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public static class GlobalConfig {

        /// <summary>
        /// Saves a specific treasure chest's status.
        /// </summary>
        /// <param name="tName">
        /// The name of the treasure that the chest holds.
        /// </param>
        /// <param name="status">
        /// The status of the chest.
        /// </param>
        public static void SaveTreasureChestStatus(string tName, bool status) {
            string varName = "Key_" + tName + "_Status";
            int newStat = 0;

            if (status == true) {
                newStat = 1;
            } else {
                newStat = 0;
            }
            Debug.Log(varName + " " + newStat);

            PlayerPrefs.SetInt(varName, newStat);
        }
    }
}
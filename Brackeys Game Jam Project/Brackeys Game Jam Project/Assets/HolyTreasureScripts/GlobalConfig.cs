using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public static class GlobalConfig {
        //13000
        #region Variables
        /// <summary>
        /// A list of the default names of high scorers.
        /// </summary>
        public static List<string> defaultHighScoreNames = new List<string>() {
        "DWN", "KLW", "ABC", "POP", "DAD", "MOM", "HHH", "DOG", "CAT", "YOU" };

        /// <summary>
        /// A list of the default scores of high scorers.
        /// </summary>
        public static List<int> defaultHighScoreValues = new List<int>() {
        26000, 24000, 22000, 18000, 14000, 12000, 10000, 8000, 6000, 4000}; 
        #endregion
    }
}
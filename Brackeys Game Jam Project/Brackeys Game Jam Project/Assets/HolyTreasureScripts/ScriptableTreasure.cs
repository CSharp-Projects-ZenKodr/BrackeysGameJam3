using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    [CreateAssetMenu(fileName ="New Treasure", menuName = "Treasure")]
    public class ScriptableTreasure : ScriptableObject {
        #region MyRegion
        /// <summary>
        /// The Icon of the Treasure.
        /// </summary>
        public Sprite treasureIcon;
        /// <summary>
        /// The Name of the Treasure.
        /// </summary>
        public string treasureName;
        /// <summary>
        /// The Description of the Treasure.
        /// </summary>
        [TextArea]
        public string treasureDescription;
        /// <summary>
        /// The Value of the Treasure.
        /// </summary>
        public int treasureValue;
        #endregion
    }
}

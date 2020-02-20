using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Tutorial {
    public class Waypoint : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The previous Waypoint that came before this one.
        /// </summary>
        public Waypoint previousWaypoint { get; set; }

        /// <summary>
        /// The corresponding direction text mesh that goes with this Waypoint.
        /// </summary>
        public TextMesh correspondingDirection;
        /// <summary>
        /// The next Waypoint the player must go to.
        /// </summary>
        public Waypoint nextWaypoint;
        /// <summary>
        /// The next direction that will be displayed when Waypoint is reached.
        /// </summary>
        [TextArea]
        public string nextDirection; 
        #endregion
    }
}
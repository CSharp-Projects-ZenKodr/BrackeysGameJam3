using Assets.HolyTreasureScripts.Digging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Tutorial {
    public class Waypoint_PrizeGot : Waypoint {

        #region Variables
        /// <summary>
        /// The Dig Prize this script reads from.
        /// </summary>
        public DigPrize dp { get; set; }
        #endregion

        /// <summary>
        /// Sets the prize for this Waypoint.
        /// </summary>
        /// <param name="prize">
        /// The prize.
        /// </param>
        public void SetPrize (GameObject prize) {
            dp = prize.GetComponent<DigPrize>();
        }

        private void Update() {
            if (dp != null) {
                if (dp.dugUp) {
                    GoToNextTutorialStep();
                }
            }
        }

        public void GoToNextTutorialStep() {
            Debug.Log("Next");
            correspondingDirection.text = nextDirection;
            if (nextWaypoint != null) {
                nextWaypoint.gameObject.SetActive(true);
                nextWaypoint.previousWaypoint = this;
            }
        }

        private void OnDisable() {
            correspondingDirection.gameObject.SetActive(false);
        }
    }
}
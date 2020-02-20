using Assets.HolyTreasureScripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Tutorial {
    public class Waypoint_AbstractBox : Waypoint {

        #region Variables
        /// <summary>
        /// Return true if this waypoint is the last one, or false if not.
        /// </summary>
        public bool isLastWaypoint = false;

        /// <summary>
        /// Return true if script should check if Waypoint was hit, or false if not.
        /// </summary>
        private bool checkForHit = true;
        #endregion

        private void OnEnable() {
            ToggleActivationStatuses(true);
        }

        private void Update() {
            if (isLastWaypoint && !checkForHit) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("End Tutorial");
                    SceneTransitioner sceneTran = SceneTransitioner.Instance;
                    sceneTran.attachedAnimator.SetTrigger("FadeOut");
                } 
            }
        }

        private void OnDisable() {
            ToggleActivationStatuses(false);
        }

        /// <summary>
        /// Toggles the activation statuses of numerous items.
        /// </summary>
        /// <param name="newStatus">
        /// The objects new activation status.
        /// </param>
        private void ToggleActivationStatuses (bool newStatus) {
            correspondingDirection.gameObject.SetActive(newStatus);
        }

        private void OnTriggerEnter(Collider other) {
            if (checkForHit) {
                if (other.tag == "Player") {
                    Debug.Log("Next Step");
                    correspondingDirection.text = nextDirection;
                    if (nextWaypoint != null) {
                        nextWaypoint.gameObject.SetActive(true);
                        nextWaypoint.previousWaypoint = this; 
                    }
                    if (previousWaypoint != null) {
                        previousWaypoint.gameObject.SetActive(false);
                    }
                    checkForHit = false;
                }
            }
        }

    }
}
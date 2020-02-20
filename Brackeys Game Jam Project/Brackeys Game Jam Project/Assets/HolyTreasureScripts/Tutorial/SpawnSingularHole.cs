using Assets.HolyTreasureScripts.Digging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Tutorial {
    public class SpawnSingularHole : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The pile parent that will spawn.
        /// </summary>
        public GameObject pileParent;
        /// <summary>
        /// The ground floor the pile will spawn on.
        /// </summary>
        public GameObject groundFloor;
        /// <summary>
        /// The prize that will spawn in the hole.
        /// </summary>
        public GameObject prize;
        /// <summary>
        /// The position the hole will spawn at relative to the ground floor.
        /// </summary>
        public Vector2 spawnPosition;
        /// <summary>
        /// The next waypoint for the player to meet.
        /// </summary>
        public Waypoint_PrizeGot nextWaypoint;
        /// <summary>
        /// Return true if the hole is special, or false if not.
        /// </summary>
        public bool isSpecialHole;
        /// <summary>
        /// Return true to spawn the hole OnEnable, or false to spawn OnTriggerEnter;
        /// </summary>
        public bool spawnOnEnable = false;

        /// <summary>
        /// Return true if the player has hit this object, or false if not.
        /// </summary>
        private bool checkForHit = true;
        #endregion

        private void OnEnable() {
            if (spawnOnEnable) {
                SpawnHole();
            }
        }

        private void SpawnHole () {
            GameObject newHole = Instantiate(pileParent, groundFloor.transform);
            DiggablePile dp = newHole.transform.GetChild(0).GetComponent<DiggablePile>();

            GameObject newPrize = dp.AddPrize(prize, isSpecialHole);
            if (nextWaypoint != null) {
                nextWaypoint.SetPrize(newPrize); 
            }

            newHole.transform.localPosition = new Vector3(spawnPosition.x, groundFloor.transform.position.y, spawnPosition.y);
            checkForHit = false;
        }

        private void OnTriggerEnter(Collider other) {
            if (!spawnOnEnable) {
                if (checkForHit) {
                    if (other.tag == "Player") {
                        SpawnHole();
                    }
                } 
            }
        }
    }
}
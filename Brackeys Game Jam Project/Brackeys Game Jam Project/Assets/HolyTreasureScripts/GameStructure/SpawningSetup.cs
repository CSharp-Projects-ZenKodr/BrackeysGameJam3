using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.GameStructure {
    public class SpawningSetup : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The prefab that holds the pile parent data.
        /// </summary>
        public GameObject pileParent;
        /// <summary>
        /// The ground floors in the scene.
        /// </summary>
        public GameObject[] groundFloors;
        /// <summary>
        /// The maximum holes per floor.
        /// </summary>
        public int maxHolesPerFloor;
        /// <summary>
        /// The value of the border of the play field.
        /// </summary>
        public float borderValue = 100;

        /// <summary>
        /// The holes that have been spawned.
        /// </summary>
        private List<GameObject> spawnedHoles = new List<GameObject>();
        /// <summary>
        /// Return true if new hole is an acceptible distance from the previous holes.
        /// </summary>
        private bool distanceIsAcceptable = false;
        #endregion

        private void Start() {
            for (int i = 0; i < maxHolesPerFloor; i++) {
                //Holes need to be .5 away from each other.
                Vector3 potentialNewPosition = Vector3.one;

                while (!distanceIsAcceptable) {
                    potentialNewPosition = new Vector3(Random.Range(-borderValue, borderValue), 0, Random.Range(-borderValue, borderValue));
                    
                    if (spawnedHoles.Count != 0) {
                        foreach (GameObject hole in spawnedHoles) {
                            float dist = Vector3.Distance(potentialNewPosition, hole.transform.localPosition);

                            if (dist >= 0.5f) {
                                distanceIsAcceptable = true;
                                Debug.Log("Acceptible");
                            }
                            else {
                                continue;
                            }
                        }
                    }
                }

                GameObject newHole = Instantiate(pileParent, groundFloors[0].transform);

                newHole.transform.localPosition = potentialNewPosition;
                spawnedHoles.Add(newHole);
                distanceIsAcceptable = false;
            }
        }
    }
}
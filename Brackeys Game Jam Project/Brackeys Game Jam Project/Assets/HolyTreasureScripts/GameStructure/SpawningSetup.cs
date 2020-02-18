using Assets.HolyTreasureScripts.Digging;
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
        /// The Dig Prizes the Diggable Piles can have.
        /// </summary>
        public GameObject[] digPrizes;
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
        #endregion

        private void Start() {
            for (int h = 0; h < groundFloors.Length; h++) {
                
                for (int i = 0; i < maxHolesPerFloor; i++) {
                    //Debug.Log(i);

                    Vector3 potentialNewPosition = Vector3.one;

                    potentialNewPosition = new Vector3(Random.Range(-borderValue, borderValue), 0, Random.Range(-borderValue, borderValue));

                    GameObject newHole = Instantiate(pileParent, groundFloors[h].transform);
                    DiggablePile dp = newHole.transform.GetChild(0).GetComponent<DiggablePile>();

                    if (i < 7) {
                        //Mines
                        dp.AddPrize(digPrizes[0], true);
                    }
                    if (i >= 7 && i < 14) {
                        //Treasure Chests
                        dp.AddPrize(digPrizes[1], true);
                    }
                    if (i >= 14) {
                        //Money Bags
                        dp.AddPrize(digPrizes[2], false);
                    }
                    newHole.transform.localPosition = potentialNewPosition;
                }
            }
        }
    }
}
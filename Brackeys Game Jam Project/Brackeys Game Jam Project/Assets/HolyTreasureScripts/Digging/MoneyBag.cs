using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//TODO: Add money particle system on collection
namespace Assets.HolyTreasureScripts.Digging {
    public class MoneyBag : DigPrize {

        #region Variables
        /// <summary>
        /// The monetary value of the money bag.
        /// </summary>
        private int bagValue; 
        #endregion

        private void Start() {
            GetDigPrizeData();
            bagValue = Random.Range(0, 201);
        }

        private void OnTriggerEnter(Collider other) {
            if (dugUp) {
                if (other.tag == "Player") {
                    PlayerInventory.Instance.currentMoney += bagValue;
                    Destroy(gameObject);
                }
            }
        }
    }
}
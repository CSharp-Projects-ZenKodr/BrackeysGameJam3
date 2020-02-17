using Assets.HolyTreasureScripts.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class MoneyBag : DigPrize {
        #region Variables
        /// <summary>
        /// The maximum int value (exclusive) this bag could be worth.
        /// </summary>
        public int exclusiveMaxValue = 201; 
        #endregion

        private void Start() {
            GetDigPrizeData();
            prizeValue = Random.Range(0, exclusiveMaxValue);
        }

        private void OnTriggerEnter(Collider other) {
            if (dugUp) {
                if (other.tag == "Player") {
                    PlayerInventory.Instance.currentMoney += prizeValue;
                    GameplayUI.Instance.UpdateMoneyValue(PlayerInventory.Instance.currentMoney);
                    //TODO: Add money particle system on collection
                    Destroy(gameObject);
                }
            }
        }
    }
}
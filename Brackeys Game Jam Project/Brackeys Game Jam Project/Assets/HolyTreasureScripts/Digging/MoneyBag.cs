using Assets.HolyTreasureScripts.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class MoneyBag : DigPrize {
        #region Variables
        /// <summary>
        /// The money particle system attached to this money bag.
        /// </summary>
        public ParticleSystem psMoney;
        /// <summary>
        /// The maximum int value (exclusive) this bag could be worth.
        /// </summary>
        public int exclusiveMaxValue = 201;
        /// <summary>
        /// The minimum int value (inclusive this bag could be worth)
        /// </summary>
        public int inclusiveMinValue = 100;
        #endregion

        private void Start() {
            GetDigPrizeData();
            prizeValue = Random.Range(inclusiveMinValue, exclusiveMaxValue);
        }

        private void OnTriggerEnter(Collider other) {
            if (dugUp) {
                if (other.tag == "Player") {
                    PlayerInventory.Instance.currentMoney += prizeValue;
                    GameplayUI.Instance.UpdateMoneyValue(PlayerInventory.Instance.currentMoney);
                    psMoney.Play();
                    Destroy(gameObject);
                    Destroy(psMoney, 5f);
                }
            }
        }
    }
}
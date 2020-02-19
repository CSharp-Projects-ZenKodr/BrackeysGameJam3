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
        /// <summary>
        /// The minimum int value (inclusive this bag could be worth)
        /// </summary>
        public int inclusiveMinValue = 100;


        /// <summary>
        /// The money particle system attached to this money bag.
        /// </summary>
        private ParticleSystem psMoney;
        /// <summary>
        /// Return true if player got the treasure, or false if not.
        /// </summary>
        private bool treasureGot = false;
        #endregion

        private void Start() {
            GetDigPrizeData();
            psMoney = transform.GetChild(0).GetComponent<ParticleSystem>();
            prizeValue = Random.Range(inclusiveMinValue, exclusiveMaxValue);
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!treasureGot) {
                    PlayerInventory.Instance.UpdateMoney(prizeValue);
                    psMoney.transform.parent = null;
                    psMoney.Play();
                    Destroy(gameObject);
                    Destroy(psMoney, 5f);

                    treasureGot = true;
                }
            }
        }
    }
}
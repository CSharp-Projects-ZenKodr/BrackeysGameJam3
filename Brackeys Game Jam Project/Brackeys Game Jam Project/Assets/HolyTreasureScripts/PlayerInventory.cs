using Assets.HolyTreasureScripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public class PlayerInventory : MonoBehaviour {

        #region MyRegion
        /// <summary>
        /// The instance of this class.
        /// </summary>
        public static PlayerInventory Instance;

        /// <summary>
        /// The money the player currently has on hand.
        /// </summary>
        public int currentMoney = 0;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one Player Inven class in the scene!");
            }
        }

        /// <summary>
        /// Updates the player's money.
        /// </summary>
        /// <param name="val">
        /// The incoming monetary value.
        /// </param>
        public void UpdateMoney(int val) {
            currentMoney += val;
            GameplayUI.Instance.UpdateMoneyValue(currentMoney);
        }
    }
}
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

        /// <summary>
        /// The list of treasures the player has aquired.
        /// </summary>
        public List<ScriptableTreasure> acquiredTreasures { get; set; }
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one Player Inven class in the scene!");
            }
        }

        /// <summary>
        /// Add a treasure to the player's inventory.
        /// </summary>
        /// <param name="treasure">
        /// The treasure being acquired.
        /// </param>
        public void AddTreasure (ScriptableTreasure treasure) {
            //TODO: Add Scriptable Treasure to the list.
            //acquiredTreasures.Add(treasure);
            currentMoney += treasure.treasureValue;
        }
    }
}
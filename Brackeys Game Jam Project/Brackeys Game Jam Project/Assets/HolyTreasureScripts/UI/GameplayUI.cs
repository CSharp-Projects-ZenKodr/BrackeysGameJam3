using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HolyTreasureScripts.UI {
    public class GameplayUI : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The static instance of this class.
        /// </summary>
        public static GameplayUI Instance;

        /// <summary>
        /// The flaot representing how much oxygen the player has left.
        /// </summary>
        public float oxygenValue { get; set; }
        /// <summary>
        /// Return true if oxygen should drain, or false if not.
        /// </summary>
        public bool drainOxygen { get; set; }

        /// <summary>
        /// The image that represents how much oxygen the player has left.
        /// </summary>
        public Image oxygenFill;
        /// <summary>
        /// The text component that displays the player's money value.
        /// </summary>
        public Text moneyValue;
        /// <summary>
        /// The Text that displays what floor the player is on.
        /// </summary>
        public Text floorText;
        /// <summary>
        /// The rate at which the oxygen will decay.
        /// </summary>
        public float oxygenDecayRate = 0.1f;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one Gameplay UI class in the scene!");
            }
        }

        private void Start() {
            oxygenValue = 1;
        }

        private void Update() {
            if (drainOxygen) {
                YourOxygenIsRunningLow(); 
            }
        }

        private void YourOxygenIsRunningLow() {
            oxygenValue -= oxygenDecayRate * Time.deltaTime;
            oxygenFill.fillAmount = oxygenValue;
        }

        /// <summary>
        /// Updates the money value display.
        /// </summary>
        /// <param name="val">
        /// The value it shall now display.
        /// </param>
        public void UpdateMoneyValue(int val) {
            moneyValue.text = val.ToString();
        }

        /// <summary>
        /// Updates the Floor Text component.
        /// </summary>
        /// <param name="val">
        /// The floor value the player is on.
        /// </param>
        public void UpdateFloorText(int val) {
            if (val < 4) {
                floorText.text = "Floor " + val; 
            } else {
                floorText.text = "LAVA!";
            }
        }
    } 
}
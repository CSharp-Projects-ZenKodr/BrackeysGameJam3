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
        /// The image that represents how much oxygen the player has left.
        /// </summary>
        public Image oxygenFIll;
        /// <summary>
        /// The text component that displays the player's money value.
        /// </summary>
        public Text moneyValue;
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
            YourOxygenIsRunningLow();
        }

        private void YourOxygenIsRunningLow() {
            oxygenValue -= oxygenDecayRate * Time.deltaTime;
            oxygenFIll.fillAmount = oxygenValue;
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
    } 
}
using Assets.HolyTreasureScripts.Audio;
using Assets.HolyTreasureScripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Tutorial {
    public class Waypoint_Shop : Waypoint {

        #region Variables
        /// <summary>
        /// The GameplayUI in the scene.
        /// </summary>
        private GameplayUI gameUI;
        /// <summary>
        /// The Audio Manager in the scene.
        /// </summary>
        private AudioManager audioMan;
        #endregion

        private void OnEnable() {
            gameUI = GameplayUI.Instance;
            gameUI.oxygenFill.fillAmount = 0.1f;
            audioMan = AudioManager.Instance;
            audioMan.PlaySound("Heart");
        }

        public void PressedBuyButton() {
            Debug.Log("Pressed");
            audioMan.StopSound("Heart");
            gameUI.oxygenFill.fillAmount = 1f;
            previousWaypoint.gameObject.SetActive(false);
            nextWaypoint.gameObject.SetActive(true);
            nextWaypoint.previousWaypoint = this;
        }
    }
}
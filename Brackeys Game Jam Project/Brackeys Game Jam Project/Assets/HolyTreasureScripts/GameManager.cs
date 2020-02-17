using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Assets.HolyTreasureScripts.UI;

namespace Assets.HolyTreasureScripts {
    public class GameManager : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The Gameplay UI class in the scene.
        /// </summary>
        private GameplayUI gameUI;
        /// <summary>
        /// The User Controller within the scene.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The Scene Transitioner in the scene.
        /// </summary>
        private SceneTransitioner sceneTran;
        /// <summary>
        /// Return true if class should check for a game over, or false if not.
        /// </summary>
        private bool checkForGameOver = true;
        #endregion

        private void Start() {
            gameUI = GameplayUI.Instance;
            useCon = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();
            sceneTran = SceneTransitioner.Instance;
        }

        private void Update() {
            if (checkForGameOver) {
                if (gameUI.oxygenValue <= 0) {
                    useCon.ableToMove = false;
                    sceneTran.attachedAnimator.SetTrigger("FadeOut");
                    Debug.Log("Game Over");
                    checkForGameOver = false;
                }
            }
        }
    }
}
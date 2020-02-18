using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using Assets.HolyTreasureScripts.UI;

namespace Assets.HolyTreasureScripts {
    public class GameManager : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The static instance of this class.
        /// </summary>
        public static GameManager Instance;

        /// <summary>
        /// Return true if walls are reinforced, or false if not.
        /// </summary>
        public bool reinforced { get; set; }
        /// <summary>
        /// How many mines have been exploded thus far.
        /// </summary>
        public int minesExploded { get; set; }

        /// <summary>
        /// The transform that holds all the mine icons.
        /// </summary>
        public Transform mineIconParent;
        /// <summary>
        /// The transform that holds all the hit icons.
        /// </summary>
        public Transform hitIconParent;

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
        /// A collection of all the mine icons.
        /// </summary>
        private Image[] mineIcons;
        /// <summary>
        /// A collection of all the hit icons.
        /// </summary>
        private Image[] hitIcons;
        /// <summary>
        /// Return true if class should check for a game over, or false if not.
        /// </summary>
        private bool checkForGameOver = true;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one instance of the Game Manager class in the scene!");
            }
        }

        private void Start() {
            mineIcons = mineIconParent.GetComponentsInChildren<Image>();
            hitIcons = hitIconParent.GetComponentsInChildren<Image>();
            foreach (Image mine in mineIcons) {
                mine.enabled = false;
            }
            foreach (Image hit in hitIcons) {
                hit.enabled = false;
            }
            UpdateMineStatus(0);

            gameUI = GameplayUI.Instance;
            useCon = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();
            sceneTran = SceneTransitioner.Instance;
        }

        /// <summary>
        /// Updates the game's mine status.
        /// </summary>
        /// <param name="hits">
        /// The amount of mines that have been hit.
        /// </param>
        public void UpdateMineStatus(int hits) {
            if (hits > 5) {
                Debug.LogError("Too many hits being reported!");
                return;
            }

            if (reinforced) {
                foreach (Image mine in mineIcons) {
                    mine.enabled = true;
                }
            } else {
                if (hits > 3) {
                    Debug.LogError("Walls aren't being reinforced, too many hits being reported.");
                    return;
                }
                for (int i = 4; i > 1; i--) {
                    mineIcons[i].enabled = true;
                }
            }

            int length = 4 - hits;

            for (int i = 4; i > length; i--) {
                hitIcons[i].enabled = true;
            }

            minesExploded = hits;
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
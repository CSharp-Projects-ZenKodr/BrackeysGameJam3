using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.HolyTreasureScripts.UI {
    public class SceneTransitioner : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The static instance of this class.
        /// </summary>
        public static SceneTransitioner Instance;

        /// <summary>
        /// The animator attached to the game object.
        /// </summary>
        public Animator attachedAnimator { get; private set; }
        /// <summary>
        /// Return true if class instance is in the Main Menu scene, or false if not.
        /// </summary>
        public bool mainMenuScene { get; set; }

        /// <summary>
        /// The name of the scene we will be transitioning to.
        /// </summary>
        public string nextSceneName;
        /// <summary>
        /// Return true if class instance is in the Tutorial Scene, or false if not.
        /// </summary>
        public bool tutorialScene = false;

        /// <summary>
        /// The user controller in the scene;
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The GameplayUI in the scene;
        /// </summary>
        private GameplayUI gameUI;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one Scene Transition class in the scene!");
            }
        }

        private void Start() {
            if (!mainMenuScene) {
                useCon = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();
                gameUI = GameplayUI.Instance; 
            }
            attachedAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// What should happen once the scene is done fading out.
        /// </summary>
        public void FadeOutComplete() {
            PlayerPrefs.SetInt("ObtainedScore", PlayerInventory.Instance.currentMoney);
            SceneManager.LoadScene(nextSceneName);
        }

        /// <summary>
        /// What should happen once the scene is done fading in.
        /// </summary>
        public void FadeInComplete() {
            useCon.ableToMove = true;
            if (!tutorialScene) {
                gameUI.drainOxygen = true; 
            }
        }
    }
}
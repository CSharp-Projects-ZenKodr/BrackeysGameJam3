using Assets.HolyTreasureScripts.Audio;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        /// The Text component that has the Ready...Go text on it.
        /// </summary>
        public Text readyGo;
        /// <summary>
        /// The name of the scene we will be transitioning to.
        /// </summary>
        public string nextSceneName;
        /// <summary>
        /// Return true if class instance is in the Tutorial Scene, or false if not.
        /// </summary>
        public bool tutorialScene = false;

        /// <summary>
        /// The user controller in the scene.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The GameplayUI in the scene.
        /// </summary>
        private GameplayUI gameUI;
        /// <summary>
        /// The player inventory in the scene.
        /// </summary>
        private PlayerInventory playInv;
        /// <summary>
        /// The Audio Manager in the scene.
        /// </summary>
        private AudioManager audioMan;
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
                playInv = PlayerInventory.Instance;
            }
            if (tutorialScene) {
                audioMan = AudioManager.Instance;
                audioMan.PlaySound("TutorialTheme");
            }
            attachedAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// What should happen once the scene is done fading out.
        /// </summary>
        public void FadeOutComplete() {
            if (playInv != null) {
                PlayerPrefs.SetInt("ObtainedScore", playInv.currentMoney); 
            }
            SceneManager.LoadScene(nextSceneName);
        }

        /// <summary>
        /// What should happen once the scene is done fading in.
        /// </summary>
        public void FadeInComplete() {
            if (!tutorialScene) {
                StartCoroutine(CountdownToPlay());
            } else {
                useCon.ableToMove = true;
            }
        }

        IEnumerator CountdownToPlay() {
            yield return new WaitForSeconds(3);
            readyGo.text = "GO!";
            gameUI.drainOxygen = true;
            useCon.ableToMove = true;
            yield return new WaitForSeconds(3);
            readyGo.text = "";
            StopCoroutine(CountdownToPlay());
        }
    }
}
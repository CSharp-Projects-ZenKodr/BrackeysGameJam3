using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.HolyTreasureScripts.Audio;

namespace Assets.HolyTreasureScripts.UI {
    public class MainMenu : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The Credits of the game.
        /// </summary>
        public GameObject credits;
        /// <summary>
        /// The speed at which the shovel will move.
        /// </summary>
        public float shovelSpeed;
        /// <summary>
        /// The time it takes before the hole swallows the scene.
        /// </summary>
        public float timeBeforeSwallow;
        /// <summary>
        /// The time it takes before the active button is destoryed.
        /// </summary>
        public float timeBeforeButtonDestroy;
        /// <summary>
        /// The rate at which the music fades out.
        /// </summary>
        public float fadeOutRate;

        /// <summary>
        /// The Scene Transitioner within the scene.
        /// </summary>
        private SceneTransitioner transitioner;
        /// <summary>
        /// The Audio Manager within the scene.
        /// </summary>
        private AudioManager audioMan;
        /// <summary>
        /// The currently active shovel.
        /// </summary>
        private GameObject activeShovel;
        /// <summary>
        /// The currenly active button.
        /// </summary>
        private GameObject activeButton;
        /// <summary>
        /// The currently active hole.
        /// </summary>
        private GameObject activeHole;
        /// <summary>
        /// The audio source that plays the Main Menu Music.
        /// </summary>
        private AudioSource mainMenuMusic;
        /// <summary>
        /// Return true if scene is changing, or false if not.
        /// </summary>
        private bool changingScene;
        /// <summary>
        /// Return true if the script should check for shovel activation, or false if not.
        /// </summary>
        private bool checkForShovelActivation = true;
        /// <summary>
        /// The name of the next scene.
        /// </summary>
        private string nextScene;
        /// <summary>
        /// Return true if fading out, or false if not.
        /// </summary>
        private bool fadeOut = false;
        #endregion

        private void Start() {
            audioMan = AudioManager.Instance;
            mainMenuMusic = audioMan.PlaySound("MainMusic");
        }

        private void Update() {
            if (changingScene) {
                activeShovel.transform.Translate(activeShovel.transform.right * Time.deltaTime * shovelSpeed);
                if (checkForShovelActivation) {
                    StartCoroutine(DestroyButton());
                    StartCoroutine(SwallowScene());
                    checkForShovelActivation = false;
                }
            }
            if (fadeOut) {
                if (mainMenuMusic.volume > 0) {
                    mainMenuMusic.volume -= fadeOutRate;
                }
            }
        }

        IEnumerator DestroyButton() {
            audioMan.PlaySound("Swallow");
            yield return new WaitForSeconds(timeBeforeButtonDestroy);
            transitioner = activeHole.AddComponent<SceneTransitioner>();
            transitioner.mainMenuScene = true;
            activeHole.transform.parent = transform;
            activeHole.transform.SetAsLastSibling();
            transitioner.nextSceneName = nextScene;
            Destroy(activeButton);
            StopCoroutine(DestroyButton());
        }

        IEnumerator SwallowScene() {
            yield return new WaitForSeconds(timeBeforeSwallow);
            transitioner.attachedAnimator.SetTrigger("Swallow");
            StopCoroutine(SwallowScene());
        }

        /// <summary>
        /// Toggles the Shovel Icon on or off.
        /// </summary>
        /// <param name="shovelIcon">
        /// The Shovel Icon.
        /// </param>
        /// <param name="toggle">
        /// The new status of the shovel.
        /// </param>
        public void ToggleShovel(GameObject shovelIcon) {
            if (!changingScene) {
                shovelIcon.SetActive(!shovelIcon.activeInHierarchy);
                if (shovelIcon.activeInHierarchy) {
                    activeShovel = shovelIcon;
                    audioMan.PlaySound("ButtonBoop");
                } else {
                    activeShovel = null;
                }
            }
        }

        /// <summary>
        /// Sets the active button.
        /// </summary>
        /// <param name="button">
        /// The button to become active.
        /// </param>
        public void SetActiveButton(GameObject button) {
            activeButton = button;
        }

        /// <summary>
        /// Sets the active hole.
        /// </summary>
        /// <param name="hole">
        /// The hole to become activee.
        /// </param>
        public void SetActiveHole(GameObject hole) {
            activeHole = hole;
        }

        /// <summary>
        /// Changes the scene.
        /// </summary>
        /// <param name="sceneName">
        /// The scene that is being transitioned to.
        /// </param>
        public void ChangeScene(string sceneName) {
            nextScene = sceneName;
            fadeOut = true;
            changingScene = true;
        }

        /// <summary>
        /// Toggles the credits on and off.
        /// </summary>
        public void ToggleCredits() {
            credits.SetActive(!credits.activeInHierarchy);
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame () {
            Debug.Log("Player Quit Game");
            Application.Quit();
        }
    }
}
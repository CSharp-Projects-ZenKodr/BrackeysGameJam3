using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.HolyTreasureScripts.UI {
    public class GameOverUI : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The text component that displays the high scorer names.
        /// </summary>
        public Text highScorerNameTextBox;
        /// <summary>
        /// The text component that displays the high scorer scores.
        /// </summary>
        public Text highScorerScoreTextBox;
        /// <summary>
        /// The Input field the player puts their name in when they get a high score.
        /// </summary>
        public InputField playerNameField;
        /// <summary>
        /// The GameObject that holds the main menu button information.
        /// </summary>
        public GameObject mainMenuButton;

        /// <summary>
        /// A List of the previous high scores acquired.
        /// </summary>
        private List<int> previousScores = new List<int>();
        /// <summary>
        /// A list of the previous high score winners.
        /// </summary>
        private List<string> previousNames = new List<string>();
        /// <summary>
        /// The index of the new high score if the player got it.
        /// </summary>
        private int newHighScoreIndex;
        #endregion

        private void Start() {
            bool firstTimeHere = false;
            int firstTimeSeeingScreen = PlayerPrefs.GetInt("FirstTime");

            if (firstTimeSeeingScreen == 0) {
                firstTimeHere = true;

                PlayerPrefs.SetInt("FirstTime", 1);
            } else if (firstTimeSeeingScreen == 1) {
                firstTimeHere = false;
            } else {
                Debug.LogError("Int loaded wrong when checking for first time!");
            }

            if (firstTimeHere) {
                previousNames = GlobalConfig.defaultHighScoreNames;
                previousScores = GlobalConfig.defaultHighScoreValues;
            } else {
                for (int i = 0; i < 10; i++) {
                    previousNames.Add(PlayerPrefs.GetString("ScoreName_" + i));
                }
                for (int i = 0; i < 10; i++) {
                    previousScores.Add(PlayerPrefs.GetInt("ScoreValue_" + i));
                }
            }

            //Get obtained score
            //TODO: Make sure when player gets game over, their treasure score saves to this.
            int obtainedScore = PlayerPrefs.GetInt("ObtainedScore");
            //Compare it to the scores that already exist
            bool newHighScore = false;
            newHighScoreIndex = 0;

            for (int i = 0; i < previousScores.Count; i++) {
                if (obtainedScore < previousScores[i]) {
                    continue;
                } else {
                    Debug.Log(obtainedScore + " is higher than or equal to " + previousNames[i] + "'s " + previousScores[i]);
                    newHighScore = true;
                    newHighScoreIndex = i;
                    break;
                }
            }
            //If higher than any of them, insert it at appropiate spot
            if (newHighScore) {
                previousScores.Insert(newHighScoreIndex, obtainedScore);
                //Get rid of very bottom score
                previousScores.Remove(previousScores.Last());
            }
            //Write scores to box
            StringBuilder sb = new StringBuilder();
            foreach (int val in previousScores) {
                sb.AppendLine(val.ToString());
            }
            highScorerScoreTextBox.text = sb.ToString();
            //Put empty name in corresponding placement
            if (newHighScore) {
                previousNames.Insert(newHighScoreIndex, string.Empty);
                //Get rid of very bottom name
                previousNames.Remove(previousNames.Last());
            }
            //Write names to box
            sb = new StringBuilder();
            foreach (string name in previousNames) {
                sb.AppendLine(name);
            }
            highScorerNameTextBox.text = sb.ToString();
            //Turn on input field so player can input their name
            if (newHighScore) {
                playerNameField.gameObject.SetActive(true);
            }

            if (!newHighScore) {
                mainMenuButton.SetActive(true);
                SaveScores();
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.K)) {
                //Secret key that resets the high scores.
                PlayerPrefs.SetInt("FirstTime", 0);
            }
        }

        /// <summary>
        /// Should be called when the player changes the text in the input field.
        /// </summary>
        /// <param name="newText">
        /// The text coming in from the player.
        /// </param>
        public void TextChanged (string newText) {
            //Make empty equal to what the player is inputing, can see creation of name real time
            if (newText != playerNameField.text.ToUpper()) {
                playerNameField.text = playerNameField.text.ToUpper();
                newText = playerNameField.text;
            }

            previousNames[newHighScoreIndex] = newText;

            StringBuilder sb = new StringBuilder();
            foreach (string name in previousNames) {
                sb.AppendLine(name);
            }
            highScorerNameTextBox.text = sb.ToString();
        }

        /// <summary>
        /// Should be called thwn the player ends their input when entering high score name.
        /// </summary>
        /// <param name="newText">
        /// The Text coming in from the player.
        /// </param>
        public void InputEnded (string newText) {
            //Once input is complete, save scores, allow player to leave.
            playerNameField.gameObject.SetActive(false);

            SaveScores();

            mainMenuButton.SetActive(true);
        }

        /// <summary>
        /// Goes to main menu.
        /// </summary>
        public void GoToMainMenu () {
            SceneManager.LoadScene("Main Menu");
        }

        /// <summary>
        /// Saves Scores.
        /// </summary>
        private void SaveScores() {
            for (int i = 0; i < previousNames.Count; i++) {
                PlayerPrefs.SetString("ScoreName_" + i, previousNames[i]);
            }
            for (int i = 0; i < previousScores.Count; i++) {
                PlayerPrefs.SetInt("ScoreValue_" + i, previousScores[i]);
            }
        }
    }
}
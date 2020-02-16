using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.HolyTreasureScripts.UI {
    public class TreasureNotification : MonoBehaviour{

        #region Variables
        /// <summary>
        /// The instance of this class.
        /// </summary>
        public static TreasureNotification Instance;

        /// <summary>
        /// The image that displays the treasure's icon.
        /// </summary>
        public Image tIcon;
        /// <summary>
        /// The text that displays the treasure's name.
        /// </summary>
        public Text tName;
        /// <summary>
        /// The text that displays the treasure's description.
        /// </summary>
        public Text tDescription;
        /// <summary>
        /// The text that displays the treasure's value.
        /// </summary>
        public Text tValue;

        /// <summary>
        /// The animator attached to the notification card.
        /// </summary>
        private Animator attachedAnimator;
        /// <summary>
        /// The User Controller in the scene.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// Return true if the window is up, or false if not.
        /// </summary>
        private bool windowUp = false;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one Treasure Notif class in the scene!");
            }
        }

        private void Start() {
            attachedAnimator = GetComponent<Animator>();
            useCon = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();
        }

        private void Update() {
            if (windowUp) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    attachedAnimator.SetBool("Toggle", false);
                    useCon.ableToMove = true;
                    windowUp = false;
                }
            }
        }

        /// <summary>
        /// Pops up the notification card.
        /// </summary>
        /// <param name="treasure">
        /// The treasure information being displayed.
        /// </param>
        public void PopUpCard (ScriptableTreasure treasure) {
            tIcon.sprite = treasure.treasureIcon;
            tName.text = treasure.treasureName;
            tDescription.text = treasure.treasureDescription;
            tValue.text = "Value: <b>$" + treasure.treasureValue + "</b>";
            attachedAnimator.SetBool("Toggle", true);
            windowUp = true;
        }
    }
}
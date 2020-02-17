using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.HolyTreasureScripts.Digging {
    public class DiggablePile : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The hole the digging makes.
        /// </summary>
        public Transform hole;
        /// <summary>
        /// The prize that is dug up when dig is complete.
        /// </summary>
        public Transform digPrizeTransform;
        /// <summary>
        /// The rate at which the player digs.
        /// </summary>
        public float digRate;

        /// <summary>
        /// The prize class of the Dig Prize.
        /// </summary>
        private DigPrize prize;
        /// <summary>
        /// The controller attached to the player.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The initial scale of the pile.
        /// </summary>
        private Vector3 initialPileScale;
        /// <summary>
        /// The initial position of the Dig Prize.
        /// </summary>
        private Vector3 initialDigPrizePos;
        /// <summary>
        /// The fully dug scale of the pile.
        /// </summary>
        private Vector3 fullyDugPileScale;
        /// <summary>
        /// A vector 3 where all of its values are thirty.
        /// </summary>
        private Vector3 Vector3Thirty = new Vector3(30, 30, 30);
        /// <summary>
        /// Return true if the player can dig, or false if not.
        /// </summary>
        private bool playerCanDig = false;
        /// <summary>
        /// The value at which the player has dug thus far.
        /// </summary>
        private float dugValue = 0;
        #endregion

        private void Start() {
            GetInitialData();
        }

        private void GetInitialData() {
            initialPileScale = transform.localScale;
            initialDigPrizePos = digPrizeTransform.localPosition;
            fullyDugPileScale = new Vector3(initialPileScale.x, initialPileScale.y, 0);
            hole.localScale = Vector3.zero;
            prize = digPrizeTransform.GetComponent<DigPrize>();
        }

        private void Update() {
            if (playerCanDig) {
                DigBehavior();
            }
        }

        private void DigBehavior() {
            if (playerCanDig) {
                if (dugValue < 1) {
                    if (Input.GetKey(KeyCode.Space)) {
                        useCon.ableToMove = false;
                        useCon.crouch = true;
                        dugValue += digRate * Time.deltaTime;
                        transform.localScale = Vector3.Lerp(initialPileScale, fullyDugPileScale, dugValue);
                        hole.localScale = Vector3.Lerp(Vector3.zero, Vector3Thirty, dugValue);
                        digPrizeTransform.localPosition = Vector3.Lerp(initialDigPrizePos, Vector3.zero, dugValue);
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        useCon.ableToMove = true;
                        useCon.crouch = false;
                    } 
                } else {
                    prize.MakePrizeActive();
                    useCon.ableToMove = true;
                    useCon.crouch = false;
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                useCon = other.GetComponent<ThirdPersonUserControl>();
                playerCanDig = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Player") {
                useCon = null;
                playerCanDig = false;
            }
        }
    }
}
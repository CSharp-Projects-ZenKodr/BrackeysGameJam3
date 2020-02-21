using Assets.HolyTreasureScripts.Audio;
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
        /// The empty transfor that prize will child to.
        /// </summary>
        public Transform prizeParent;
        /// <summary>
        /// The hole the digging makes.
        /// </summary>
        public Transform hole;
        /// <summary>
        /// The particle system that is supposed to represent dirt being dug up.
        /// </summary>
        public ParticleSystem diggingPS;
        
        /// <summary>
        /// The rate at which the player digs.
        /// </summary>
        private float digRate;
        /// <summary>
        /// The controller attached to the player.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The Audio Manager in the scene.
        /// </summary>
        private AudioManager audioMan;
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
        private Vector3 Vector3Quarter = new Vector3(0.25f, 0.25f, 0.25f);
        /// <summary>
        /// The prize class of the Dig Prize.
        /// </summary>
        private DigPrize prizeClass;
        /// <summary>
        /// The Renderer attached to the pile.
        /// </summary>
        private Renderer attachedRender;
        /// <summary>
        /// The Material that represents the pile.
        /// </summary>
        private Material pileMaterial;
        /// <summary>
        /// Return true if the player can dig, or false if not.
        /// </summary>
        private bool playerCanDig = false;
        /// <summary>
        /// The value at which the player has dug thus far.
        /// </summary>
        private float dugValue = 0;
        #endregion

        private void GetInitialData() {
            attachedRender = GetComponent<Renderer>();
            pileMaterial = attachedRender.material;
            audioMan = AudioManager.Instance;
            initialPileScale = transform.localScale;
            fullyDugPileScale = new Vector3(initialPileScale.x, initialPileScale.y, 0);
            hole.localScale = Vector3.zero;
        }

        private void Update() {
            if (playerCanDig) {
                DigBehavior();
            }
        }

        private void DigBehavior() {
            if (playerCanDig) {
                if (dugValue < 1) {
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        diggingPS.Play();
                        audioMan.PlaySound("Dig");
                    }
                    if (Input.GetKey(KeyCode.Space)) {
                        useCon.ableToMove = false;
                        useCon.crouch = true;
                        dugValue += digRate * Time.deltaTime;
                        transform.localScale = Vector3.Lerp(initialPileScale, fullyDugPileScale, dugValue);
                        hole.localScale = Vector3.Lerp(Vector3.zero, Vector3Quarter, dugValue);
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        useCon.ableToMove = true;
                        useCon.crouch = false;
                        diggingPS.Stop();
                        audioMan.StopSound("Dig");
                    } 
                } else {
                    prizeClass.dugUp = true;
                    useCon.onPile = false;
                    useCon.ableToMove = true;
                    useCon.crouch = false;
                    useCon.interactionCanvas.SetActive(false);
                    audioMan.StopSound("Dig");
                    Destroy(diggingPS);
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Adds a prize to this hole.
        /// </summary>
        /// <param name="prize">
        /// The Prize being added.
        /// </param>
        /// <param name="specialPrize">
        /// Return true if prize is special, or false if not.
        /// </param>
        public GameObject AddPrize (GameObject prize, bool specialPrize) {
            GetInitialData();
            GameObject a = Instantiate(prize, prizeParent);
            prizeClass = a.GetComponent<DigPrize>();
            if (specialPrize) {
                pileMaterial.color = new Color(.624f, .624f, .624f, 1f);
                Debug.Log("I'm Special!", gameObject);
            }
            return a;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                useCon = other.GetComponent<ThirdPersonUserControl>();
                digRate = useCon.digRate;
                useCon.interactionCanvas.SetActive(true);
                useCon.commandText.text = "DIG!";
                playerCanDig = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Player") {
                useCon.interactionCanvas.SetActive(false);
                useCon.ableToMove = true;
                useCon.crouch = false;
                useCon = null;
                diggingPS.Stop();
                playerCanDig = false;
            }
        }
    }
}
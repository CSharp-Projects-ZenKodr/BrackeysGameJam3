using Assets.HolyTreasureScripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.HolyTreasureScripts.UI {
    public class Shop : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The Canvas that holds the shop UI data.
        /// </summary>
        public GameObject shopCanvas;

        /// <summary>
        /// The user controller in the scene.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The Gameplay UI in the scene.
        /// </summary>
        private GameplayUI gameUI;
        /// <summary>
        /// Return true if player is in shop bubble, or false if not.
        /// </summary>
        private bool playerInShopBubble = false;
        #endregion

        private void Start() {
            useCon = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();
            gameUI = GameplayUI.Instance;
        }

        private void Update() {
            if (playerInShopBubble) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    shopCanvas.SetActive(!shopCanvas.activeInHierarchy);
                    useCon.ableToMove = !useCon.ableToMove;
                    useCon.interactionCanvas.SetActive(!useCon.interactionCanvas.activeInHierarchy);
                }
            }
        }

        //TODO: Add Oxygen

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                useCon.interactionCanvas.SetActive(true);
                useCon.commandText.text = "SHOP";
                playerInShopBubble = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Player") {
                useCon.interactionCanvas.SetActive(false);
                playerInShopBubble = false;
            }
        }
    }
}
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
        /// The price of oxygen.
        /// </summary>
        public int price_oxygen;
        /// <summary>
        /// The price of the upgraded tool.
        /// </summary>
        public int price_tool;
        /// <summary>
        /// The price of shining the big light.
        /// </summary>
        public int price_light;
        /// <summary>
        /// The price of reinforcing the walls.
        /// </summary>
        public int price_walls;
        /// <summary>
        /// The price of upgrading the speed.
        /// </summary>
        public int price_speed;
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

        /// <summary>
        /// Adds half the maximum oxygen value to the current oxygen.
        /// </summary>
        public void BuyOxygen() {

        }

        /// <summary>
        /// Upgrades the player's tool so that they dig faster.
        /// </summary>
        public void UpgradeTool () {

        }

        /// <summary>
        /// Shines a big light so the player can see better.
        /// </summary>
        public void ShineLight () {

        }
        
        /// <summary>
        /// Reinforces the walls so that they don't break as easily.
        /// </summary>
        public void ReinforceWalls() {

        }

        /// <summary>
        /// Upgrades the player's movement.
        /// </summary>
        public void UpgradeMoveSpeed() {

        }

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
using Assets.HolyTreasureScripts.Audio;
using Assets.HolyTreasureScripts.GameStructure;
using Assets.HolyTreasureScripts.UI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.HolyTreasureScripts.UI {
    public class Shop : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The static instance of this class.
        /// </summary>
        public static Shop Instance;
        
        /// <summary>
        /// The base, starting price of the big light.
        /// </summary>
        public int baseLightPrice { get; private set; }
        /// <summary>
        /// The base, starting price of reinforcing walls.
        /// </summary>
        public int baseWallPrice { get; private set; }

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
        public int price_floor;
        /// <summary>
        /// The price of upgrading the speed.
        /// </summary>
        public int price_speed;
        /// <summary>
        /// The Text component that displays the oxygen price.
        /// </summary>
        public Text text_oxygen;
        /// <summary>
        /// The Text component that displays the upgrade tool price.
        /// </summary>
        public Text text_tool;
        /// <summary>
        /// The Text component that displays the light price.
        /// </summary>
        public Text text_light;
        /// <summary>
        /// The Text component that displays the reinforce walls price.
        /// </summary>
        public Text text_floor;
        /// <summary>
        /// The Text component that displays the upgrade speed price.
        /// </summary>
        public Text text_speed;
        /// <summary>
        /// The text component that displays the description of each object.
        /// </summary>
        public Text descriptionBox;
        /// <summary>
        /// The shop item group for the upgrade tool display.
        /// </summary>
        public ShopItemGroup toolItemGroup;
        /// <summary>
        /// The shop item group for the upgrade tool display.
        /// </summary>
        public ShopItemGroup lightItemGroup;
        /// <summary>
        /// The shop item group for the reinforce walls display.
        /// </summary>
        public ShopItemGroup floorItemGroup;
        /// <summary>
        /// The shop item group for the move speed display.
        /// </summary>
        public ShopItemGroup speedItemGroup;
        /// <summary>
        /// The Canvas that holds the shop UI data.
        /// </summary>
        public GameObject shopCanvas;

        /// <summary>
        /// The user controller in the scene.
        /// </summary>
        private ThirdPersonUserControl useCon;
        /// <summary>
        /// The third person character the player controls.
        /// </summary>
        private ThirdPersonCharacter character;
        /// <summary>
        /// The Gameplay UI in the scene.
        /// </summary>
        private GameplayUI gameUI;
        /// <summary>
        /// The Game Manager in the scene.
        /// </summary>
        private GameManager gameMan;
        /// <summary>
        /// The inventory the player has on them.
        /// </summary>
        private PlayerInventory playIn;
        /// <summary>
        /// The Audio Manager in the scene.
        /// </summary>
        private AudioManager audioMan;
        /// <summary>
        /// A list of tips to give the player.
        /// </summary>
        private List<string> tips;
        /// <summary>
        /// Return true if player is in shop bubble, or false if not.
        /// </summary>
        private bool playerInShopBubble = false;
        /// <summary>
        /// The default value the description box has.
        /// </summary>
        private string defaultDescription;
        #endregion

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogError("There is more than one instance of the Shop class in the scene!");
            }
        }

        private void Start() {
            tips = File.ReadAllLines(Application.dataPath + "/Resources/Tips.txt").ToList();
            useCon = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>();
            character = useCon.GetComponent<ThirdPersonCharacter>();
            gameUI = GameplayUI.Instance;
            gameMan = GameManager.Instance;
            playIn = PlayerInventory.Instance;
            audioMan = AudioManager.Instance;
            baseLightPrice = price_light;
            baseWallPrice = price_floor;
            UpdatePriceText(text_oxygen, price_oxygen);
            UpdatePriceText(text_tool, price_tool);
            UpdatePriceText(text_light, price_light);
            UpdatePriceText(text_floor, price_floor);
            UpdatePriceText(text_floor, price_floor);
            if (toolItemGroup != null) {
                toolItemGroup.ChangeDisplay("Upgrade to: Trowel", true); 
            }
            defaultDescription = descriptionBox.text;
        }

        private void Update() {
            if (playerInShopBubble) {
                if (!useCon.onPile) {
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        shopCanvas.SetActive(!shopCanvas.activeInHierarchy);
                        useCon.ableToMove = !useCon.ableToMove;
                        if (useCon.interactionCanvas.activeInHierarchy) {
                            useCon.interactionCanvas.SetActive(!useCon.interactionCanvas.activeInHierarchy);
                            useCon.commandText.text = "SHOP";
                        }
                    } 
                }
            }
        }

        /// <summary>
        /// Adds half the maximum oxygen value to the current oxygen.
        /// </summary>
        public void BuyOxygen() {
            if (SpendMoney(price_oxygen)) {
                if (gameUI.oxygenValue > 0.5f) {
                    gameUI.oxygenValue = 1;
                } else {
                    gameUI.oxygenValue += 0.5f;
                }

                price_oxygen *= 2;
                UpdatePriceText(text_oxygen, price_oxygen);
            }
        }

        /// <summary>
        /// Upgrades the player's tool so that they dig faster.
        /// </summary>
        public void UpgradeTool () {
            Debug.Log(useCon.currentToolID);
            if (useCon.currentToolID != 2) {
                if (SpendMoney(price_tool)) {
                    switch (useCon.currentToolID) {
                        case 0:
                            useCon.SwitchTools(1);
                            toolItemGroup.ChangeDisplay("Upgrade to: Drill", true);
                            price_tool *= 2;
                            break;
                        case 1:
                            useCon.SwitchTools(2);
                            toolItemGroup.ChangeDisplay("Cannot Upgrade", "-");
                            price_tool = 0;
                            break;
                        case 2:
                            //Do Nothing, player has best tool, perhaps disable button.
                            break;
                        default:
                            useCon.SwitchTools(0);
                            toolItemGroup.ChangeDisplay("Upgrade to: Shovel", true);
                            price_tool *= 2;
                            break;
                    }
                    
                    UpdatePriceText(text_tool, price_tool);
                }
            }
        }

        /// <summary>
        /// Shines a big light so the player can see better.
        /// </summary>
        public void ShineLight () {
            if (!gameMan.bigLight.enabled) {
                if (SpendMoney(price_light)) {
                    audioMan.PlaySound("BigLight");
                    gameMan.bigLight.enabled = true;
                    lightItemGroup.ChangeDisplay("Light is Shining", "-");
                    price_light = 0;
                    UpdatePriceText(text_light, price_light);
                }
            }
        }

        /// <summary>
        /// Reinforces the walls so that they don't break as easily.
        /// </summary>
        public void ReinforceFloor() {
            if (!gameMan.reinforced) {
                if (SpendMoney(price_floor)) {
                    audioMan.PlaySound("Reinforce");
                    gameMan.reinforced = true;
                    gameMan.UpdateMineStatus(gameMan.minesExploded);
                    floorItemGroup.ChangeDisplay("Floor Reinforced", "-");
                    price_floor = 0;
                    UpdatePriceText(text_floor, price_floor);
                }
            }
        }

        /// <summary>
        /// Upgrades the player's movement.
        /// </summary>
        public void UpgradeMoveSpeed() {
            if (character.m_MoveSpeedMultiplier < 3) {
                if (SpendMoney(price_speed)) {
                    character.m_MoveSpeedMultiplier += 0.5f;

                    if (character.m_MoveSpeedMultiplier == 3) {
                        speedItemGroup.ChangeDisplay("Speed Fully Upgraded","-");
                        price_speed = 0;
                    } else {
                        price_speed *= 2;
                    }

                    UpdatePriceText(text_speed, price_speed);
                }
            }
        }

        /// <summary>
        /// Gives a help tip to the player.
        /// </summary>
        public void GiveTip() {
            string displayTip = tips[UnityEngine.Random.Range(0, tips.Count)];

            descriptionBox.text = displayTip;
        }

        /// <summary>
        /// Spends the player's money.
        /// </summary>
        /// <param name="moneyBeingSpent">
        /// The money being spent (price).
        /// </param>
        private bool SpendMoney(int moneyBeingSpent) {
            bool output = false;

            if (shopCanvas.activeInHierarchy) {
                if (playIn.currentMoney >= moneyBeingSpent) {
                    playIn.UpdateMoney(-moneyBeingSpent);
                    audioMan.PlaySound("Money");
                    output = true;
                }
                else {
                    descriptionBox.text = "You don't have enough money for this.";
                    // Debug.LogError("The player does not have enough money to buy this item.");
                } 
            }

            return output;
        }

        /// <summary>
        /// Updates a specific price text.
        /// </summary>
        /// <param name="priceText">
        /// The price text being updated.
        /// </param>
        /// <param name="newValue">
        /// The new value the price text will have.
        /// </param>
        public void UpdatePriceText (Text priceText, int newValue) {
            if (priceText != null) {
                priceText.text = "Price: -$" + newValue; 
            }
        }

        /// <summary>
        /// Changes the Description Box text.
        /// </summary>
        /// <param name="newMessage">
        /// The new message that will be displayed.
        /// </param>
        public void ChangeDescriptionBox(string newMessage) {
            descriptionBox.text = newMessage;
            if (newMessage == string.Empty) {
                descriptionBox.text = defaultDescription;
            }
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
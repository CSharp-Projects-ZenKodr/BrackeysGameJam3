using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HolyTreasureScripts.UI {
    public class ShopItemGroup : MonoBehaviour {
        #region Variables
        /// <summary>
        /// The label that displays the item name.
        /// </summary>
        public Text itemNameLabel;
        /// <summary>
        /// The Text that is on the button.
        /// </summary>
        public Text buttonText; 
        #endregion

        /// <summary>
        /// Changes an item group text display.
        /// </summary>
        /// <param name="newText">
        /// The new text being displayed.
        /// </param>
        /// <param name="changeLabel">
        /// Return true if you want to change the name label, or false for the button.
        /// </param>
        public void ChangeDisplay(string newText, bool changeLabel) {
            if (changeLabel) {
                itemNameLabel.text = newText;
            } else {
                buttonText.text = newText;
            }
        }

        /// <summary>
        /// Changes both item group text displays.
        /// </summary>
        /// <param name="newNameText">
        /// New Text for the name label.
        /// </param>
        /// <param name="newButtonText">
        /// New Text for the button.
        /// </param>
        public void ChangeDisplay(string newNameText, string newButtonText) {
            itemNameLabel.text = newNameText;
            buttonText.text = newButtonText;
        }
    }
}
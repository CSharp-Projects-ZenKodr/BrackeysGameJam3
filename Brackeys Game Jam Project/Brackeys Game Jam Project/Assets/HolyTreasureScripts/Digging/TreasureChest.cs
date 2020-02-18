using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Assets.HolyTreasureScripts.Digging;
using Assets.HolyTreasureScripts.UI;

namespace Assets.HolyTreasureScripts.Digging {
    public class TreasureChest : DigPrize {

        #region Variables
        /// <summary>
        /// The Animator attached to this treasure chest.
        /// </summary>
        private Animator attachedAnimator;
        /// <summary>
        /// Return true if player got the treasure, or false if not.
        /// </summary>
        private bool treasureGot = false;
        #endregion

        private void Start() {
            GetDigPrizeData();
            attachedAnimator = GetComponent<Animator>();
        }

        private void Update() {
            UnearthPrize();
            if (unearthed) {
                if (!treasureGot) {
                    attachedAnimator.SetTrigger("Open");

                    PlayerInventory.Instance.currentMoney += prizeValue;
                    GameplayUI.Instance.UpdateMoneyValue(PlayerInventory.Instance.currentMoney);
                    treasureGot = true;
                }
            }
        }
    }
}
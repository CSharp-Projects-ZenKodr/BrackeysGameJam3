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
        /// The gradient the wood will tint to.
        /// </summary>
        public Gradient woodGradient;
        /// <summary>
        /// The evaluation of the gradient.
        /// </summary>
        [Range(0, 1)]
        public float evaluation = 0;
        /// <summary>
        /// The rate at which the treasure chest will decay into grey.
        /// </summary>
        public float decayRate = 0.1f;

        /// <summary>
        /// The Animator attached to this treasure chest.
        /// </summary>
        private Animator attachedAnimator;
        /// <summary>
        /// The Renderer attached to this treasure chest.
        /// </summary>
        private Renderer attachedRenderer;
        /// <summary>
        /// The Renderer attached to this treasure chest's lid.
        /// </summary>
        private Renderer lidRenderer;
        /// <summary>
        /// The material that represents red wood on the chest.
        /// </summary>
        private Material chestRedWoodMaterial;
        /// <summary>
        /// The material that represents red wood on the lid.
        /// </summary>
        private Material lidRedWoodMaterial;
        /// <summary>
        /// Return true if player got the treasure, or false if not.
        /// </summary>
        private bool treasureGot = false;
        #endregion

        private void Start() {
            GetDigPrizeData();
            attachedAnimator = GetComponent<Animator>();
            attachedRenderer = GetComponent<Renderer>();
            lidRenderer = transform.GetChild(0).GetComponent<Renderer>();
            chestRedWoodMaterial = attachedRenderer.materials.Last();
            lidRedWoodMaterial = lidRenderer.materials.Last();
        }

        private void Update() {
            if (treasureGot) {
                if (evaluation < 1) {
                    evaluation += decayRate * Time.deltaTime;
                    chestRedWoodMaterial.color = woodGradient.Evaluate(evaluation);
                    lidRedWoodMaterial.color = woodGradient.Evaluate(evaluation); 
                }
            }
            if (dugUp) {
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
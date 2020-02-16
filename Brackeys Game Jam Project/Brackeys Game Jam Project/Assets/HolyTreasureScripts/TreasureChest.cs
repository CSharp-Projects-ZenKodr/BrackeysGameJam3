using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public class TreasureChest : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The Animator attached to this treasure chest.
        /// </summary>
        private Animator attachedAnimator;
        #endregion

        private void Start() {
            attachedAnimator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                Debug.Log("Open Chest");
                attachedAnimator.SetTrigger("Open");
            }
        }
    }
}
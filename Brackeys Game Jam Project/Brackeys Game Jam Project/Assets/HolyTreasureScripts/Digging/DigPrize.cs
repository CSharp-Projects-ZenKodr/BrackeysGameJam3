using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class DigPrize : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The monetary value of this prize.
        /// </summary>
        public int prizeValue;

        /// <summary>
        /// The rigidbody attached to the Dig Prize.
        /// </summary>
        public Rigidbody prizeRigidbody { get; set; }
        /// <summary>
        /// The collider attached to the Dig Prize.
        /// </summary>
        public Collider prizeCollider { get; set; }
        /// <summary>
        /// Return true if Dig Prize has been dug up, or false if not. 
        /// </summary>
        public bool dugUp { get; set; }
        #endregion

        /// <summary>
        /// Gets the data needed for a Dig Prize.
        /// </summary>
        public void GetDigPrizeData() {
            prizeRigidbody = GetComponent<Rigidbody>();
            prizeCollider = GetComponent<Collider>();
        }

        /// <summary>
        /// Makes the Dig Prize active in the scene.
        /// </summary>
        public void MakePrizeActive() {
            prizeCollider.isTrigger = false;
            prizeRigidbody.useGravity = true;
            prizeRigidbody.isKinematic = false;
        }
    }
}

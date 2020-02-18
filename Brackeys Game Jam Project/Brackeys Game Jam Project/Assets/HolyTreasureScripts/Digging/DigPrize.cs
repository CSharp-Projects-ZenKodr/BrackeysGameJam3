using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts.Digging {
    public class DigPrize : MonoBehaviour {

        #region Variables
        public static float unearthingSpeed = 0.8f;

        /// <summary>
        /// The monetary value of this prize.
        /// </summary>
        public int prizeValue;
        /// <summary>
        /// The Y value the dig prize will go to when it is being unearthed.
        /// </summary>
        public float unearthedPrizeValue;
        
        /// <summary>
        /// The vector that the unearthed prize vector will go toward.
        /// </summary>
        public Vector3 unearthedPrizeVector { get; private set; }
        /// <summary>
        /// The initial position of this prize.
        /// </summary>
        public Vector3 initialPrizePos { get; private set; }
        /// <summary>
        /// Return true if Dig Prize has been dug up, or false if not. 
        /// </summary>
        public bool dugUp { get; set; }
        /// <summary>
        /// Return true if Prize has been unearthed, or false if not.
        /// </summary>
        public bool unearthed { get; private set; }

        /// <summary>
        /// The progress this prize has made in unearthing itself.
        /// </summary>
        private float unearthProgress = 0;
        #endregion

        /// <summary>
        /// Gets the data needed for a Dig Prize.
        /// </summary>
        public void GetDigPrizeData() {
            initialPrizePos = transform.localPosition;
            unearthedPrizeVector = new Vector3(0, unearthedPrizeValue, 0);
        }

        /// <summary>
        /// Unearths the Prize.
        /// </summary>
        public void UnearthPrize() {
            if (dugUp) {
                float dist = Vector3.Distance(transform.localPosition, unearthedPrizeVector);

                if (dist > 0) {
                    unearthProgress += unearthingSpeed * Time.deltaTime;
                    
                    transform.localPosition = Vector3.Lerp(initialPrizePos, unearthedPrizeVector, unearthProgress);
                } else {
                    unearthed = true;
                }
            }
        }
    }
}

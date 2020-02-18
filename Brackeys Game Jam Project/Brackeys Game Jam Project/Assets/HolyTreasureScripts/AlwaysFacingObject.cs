using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public class AlwaysFacingObject : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The object this Game Object will always face.
        /// </summary>
        public Transform objectToFace;
        #endregion

        private void Update() {
            transform.LookAt(objectToFace);
        }
    }
}
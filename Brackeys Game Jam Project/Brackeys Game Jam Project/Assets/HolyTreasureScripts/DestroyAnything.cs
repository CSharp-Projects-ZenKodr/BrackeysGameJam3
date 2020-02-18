using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public class DestroyAnything : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            Destroy(other.gameObject);
        }
    }
}
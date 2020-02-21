using Assets.HolyTreasureScripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HolyTreasureScripts {
    public class LavaFloor : MonoBehaviour {

        #region Variables
        /// <summary>
        /// The rate at which the lave animates.
        /// </summary>
        public float animationRate;

        /// <summary>
        /// The Renderer attached to the Lava Floor.
        /// </summary>
        private Renderer attachedRenderer;
        /// <summary>
        /// The material that represent lava.
        /// </summary>
        private Material lavaMaterial;
        /// <summary>
        /// The amount the lava should be scrolled.
        /// </summary>
        private float lavaScroll;
        #endregion

        private void Start() {
            attachedRenderer = GetComponent<Renderer>();
            lavaMaterial = attachedRenderer.material;
        }

        private void Update() {
            lavaScroll += animationRate * Time.deltaTime;

            lavaScroll = Mathf.Repeat(lavaScroll, 1);

            lavaMaterial.mainTextureOffset = new Vector3(lavaScroll, lavaScroll);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                Camera.main.transform.parent = null;

                Debug.Log("Killed by Lava");

                SceneTransitioner.Instance.attachedAnimator.SetTrigger("FadeOut");
            }
        }
    }
}
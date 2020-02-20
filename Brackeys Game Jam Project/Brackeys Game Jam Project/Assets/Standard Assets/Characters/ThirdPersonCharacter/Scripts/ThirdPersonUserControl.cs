using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson {
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour {
        /// <summary>
        /// The canvas above the player's head used for interaction.
        /// </summary>
        public GameObject interactionCanvas;
        /// <summary>
        /// The digging tools that are at the player's disposal.
        /// </summary>
        public GameObject[] diggingTools;
        /// <summary>
        /// Return true if controller is able to move, or false if not.
        /// </summary>
        public bool ableToMove = false;
        /// <summary>
        /// The base, default Dig Rate.
        /// </summary>
        public float baseDigRate;

        /// <summary>
        /// The command text childed to the interaction canvas.
        /// </summary>
        public Text commandText { get; set; }
        /// <summary>
        /// Retrun true if the controller should cround, or false if not.
        /// </summary>
        public bool crouch { get; set; }
        /// <summary>
        /// Return true if player is on a pile, or false if not.
        /// </summary>
        public bool onPile { get; set; }
        /// <summary>
        /// The player's current digRate.
        /// </summary>
        public float digRate { get; private set; }
        /// <summary>
        /// The player's current tool ID.
        /// </summary>
        public int currentToolID { get; set; }

        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


        private void Start() {
            // get the transform of the main camera
            if (Camera.main != null) {
                m_Cam = Camera.main.transform;
            }
            else {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

            commandText = interactionCanvas.transform.GetChild(0).GetComponent<Text>();

            digRate = baseDigRate;
            SwitchTools(4);
            currentToolID = 4;
        }
        
        private void Update() {
            //if (!m_Jump) {
            //    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            //}
        }
        
        // Fixed update is called in sync with physics
        private void FixedUpdate() {
            if (ableToMove) {
                // read inputs
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = Mathf.Abs( CrossPlatformInputManager.GetAxis("Vertical"));
                //bool crouch = Input.GetKey(KeyCode.C);

                // calculate move direction to pass to character
                if (m_Cam != null) {
                    // calculate camera relative direction to move:
                    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    m_Move = v * m_CamForward + h * m_Cam.right;
                }
                else {
                    // we use world-relative directions in the case of no main camera
                    m_Move = v * Vector3.forward + h * Vector3.right;
                }
#if !MOBILE_INPUT
                // walk speed multiplier
                if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

                // pass all parameters to the character control script
                m_Character.Move(m_Move, crouch, m_Jump);
                m_Jump = false;
            }
            else {
                m_Character.Move(Vector3.zero, crouch, false);
            }
        }

        /// <summary>
        /// Switches the player's tools.
        /// </summary>
        /// <param name="toolID">
        /// The ID of the Tool being switched to.
        /// </param>
        public void SwitchTools(int toolID) {
            for (int i = 0; i < diggingTools.Length; i++) {
                if (i == toolID) {
                    diggingTools[i].SetActive(true);
                    digRate = baseDigRate * (i + 2);
                    currentToolID = i;
                } else {
                    diggingTools[i].SetActive(false);
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Diggable Pile") {
                onPile = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Diggable Pile") {
                onPile = false;
            }
        }
    }
}
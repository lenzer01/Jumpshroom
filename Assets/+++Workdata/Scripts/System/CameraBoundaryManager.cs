using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Manages the Boundaries for the Screensections in which the Camera will stay as long as the player moves inside them
    /// </summary>
    public class CameraBoundaryManager : MonoBehaviour
    {
        [SerializeField] internal BoxCollider2D bc2_boundaryBox;    // link to the BoxCollider2D of "Boundary Manager" Object
        [SerializeField] private FrameSystem_Manager cl_fsm;        // link to Frame System Manager Instance
        public Transform tr_player;                                 // position of the Player
        public GameObject go_boundary;                              // link to "Boundary" Object

        
        void Start()
        {
            cl_fsm = FindObjectOfType<FrameSystem_Manager>();
            tr_player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        
        void Update()
        {
            if(!cl_fsm.bl_startscreen)
            {
                Boundary();
            }
        }

        /// <summary>
        /// Checks if Player is within the Bounds of the Boundary Manager
        /// <br>if yes, go_Boundary is active</br>
        /// <br>if no, go_Boundary is inactive</br>
        /// </summary>
        private void Boundary()
        {
            if(bc2_boundaryBox.bounds.min.x < tr_player.position.x && tr_player.position.x < bc2_boundaryBox.bounds.max.x &&
               bc2_boundaryBox.bounds.min.y < tr_player.position.y && tr_player.position.y < bc2_boundaryBox.bounds.max.y)
            {
                go_boundary.SetActive(true);
            }
            else
            {
                go_boundary.SetActive(false);
            }
        }
    }
}
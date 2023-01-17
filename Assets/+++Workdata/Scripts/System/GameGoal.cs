using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// detects Player and ends the game
    /// </summary>
    public class GameGoal : MonoBehaviour
    {
        [SerializeField]
        private FrameSystem_Manager cl_fsm; // link to FrameSystem Manager Instance
        [SerializeField]
        private UI_Manager cl_uim;          // link to UI Manager Instance
        

        /// <summary>
        /// Activates the End Functions of the frame system manager
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                cl_fsm.bl_end = true;
                cl_uim.tr_inputContainer.gameObject.SetActive(true);
            }
        }
    }
}
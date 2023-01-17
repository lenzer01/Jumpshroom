using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Gets called in the Player Animations
    /// </summary>
    public class ReleaseJumpBehaviour : MonoBehaviour
    {
        [SerializeField] internal Player_Manager cl_pm;

        /// <summary>
        /// Manages Instant Jumps
        /// </summary>
        public void ReleaseJump()
        {
            cl_pm.ReleaseJump();
        }

        /// <summary>
        /// Manages Debugs
        /// </summary>
        public void DebugAnimationReset()
        {
            gameObject.GetComponent<Animator>().SetInteger("Move Index", 4);

            gameObject.GetComponent<Animator>().SetBool("Debug", false);
        }
    }
}
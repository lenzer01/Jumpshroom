using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Holds Methods to call in Animation Events
    /// </summary>
    public class AnimEvent_StartScreen : MonoBehaviour
    {
        [SerializeField] internal FrameSystem_Manager cl_fsm;   // Link to Framesystem Manager Instance
        [SerializeField] internal Animator anim_Click;          // Link to Animator of "Click" Object
        [SerializeField] internal AudioSource as_Sound;         // Audio Source for the Game Title Sound Effect
        [SerializeField] internal Music_Manager cl_mm;          // Link to Music Manager Instance

        /// <summary>
        /// Enables the main menu after the titlescreen
        /// </summary>
        public void SetStartAnimation()
        {
            cl_fsm.bl_startscreen = false;
            cl_fsm.bl_menu = true;
            cl_mm.arr_gameMusic[1].Play();
        }

        /// <summary>
        /// enables the possibillity to continue from title screen to main menu
        /// </summary>
        public void IntroBool()
        {
            anim_Click.SetBool("Click", true);
            cl_mm.arr_gameMusic[0].Play();
        }

        public void IntroSound()
        {
            as_Sound.Play();
        }
    }
}
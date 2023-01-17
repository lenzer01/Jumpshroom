using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FinnLenz
{
    /// <summary>
    /// Detects User Inputs and Manages Reactions to them
    /// </summary>
    public class Key_Manager : MonoBehaviour
    {
        [SerializeField] private Player_Manager cl_pm;                  // link to Player_Manager instance
        [SerializeField] private UI_Manager cl_uim;                     // link to UI_Manager instance
        [SerializeField] private Camera c_UI;                           // link to CameraManager instance
        [SerializeField] private FrameSystem_Manager cl_fsm;            // link to FrameSystem Manager Instance
        [SerializeField] internal bool bl_gotSpace;                     // bool to indicate a full jump
        [SerializeField] internal Player_Sounds cl_ps;                  // link to Player_Sounds Instance
        [SerializeField] private Music_Manager cl_mm;                   // link to Music Manager Instance



        void Update()
        {
            // Title Screen Click function
            if(cl_fsm.bl_startscreen && cl_uim.anim_click.GetBool("Click"))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    cl_fsm.bl_titleVisible = false;
                    Camera.main.GetComponent<Animation>().Play();
                    cl_mm.arr_gameMusic[0].Stop();
                }
            }

            if (!cl_fsm.bl_startscreen && !cl_fsm.bl_menu && !cl_fsm.bl_end && !cl_fsm.bl_pause)
            {
                // save current position
                if(Input.GetKeyUp(KeyCode.T))
                {
                    cl_fsm.v3_playerPosition = cl_pm.gameObject.transform.position;
                }

                // take saved position
                if(Input.GetKeyUp(KeyCode.G))
                {
                    cl_pm.gameObject.transform.position = cl_fsm.v3_playerPosition;
                }

                // using walkDirection to control the direction of the next jump
                if(cl_pm.bl_onGround)
                {
                    cl_pm.fl_jumpDirection = Input.GetAxisRaw("Horizontal");
                    cl_pm.anim_player.SetInteger("Direction", ((int)cl_pm.fl_jumpDirection));
                }

                // holding space increases the float jumpForce
                // jumpForce can't get higher than jumpForceLimit
                if (Input.GetKey(KeyCode.Space) && cl_pm.bl_onGround)
                {
                    cl_pm.anim_player.SetInteger("Move Index", 1);
                    if (cl_pm.fl_powerIncrease <= cl_pm.fl_powerMax)
                    {
                        cl_pm.fl_powerIncrease += cl_pm.fl_power * Time.deltaTime;
                        cl_pm.anim_player.SetFloat("Jumpvalue", cl_pm.fl_powerIncrease);
                    }

                    if (cl_pm.fl_jumpX <= cl_pm.fl_limitX)
                    {
                        cl_pm.fl_jumpX = Mathf.Sqrt(cl_pm.fl_powerIncrease) + cl_pm.fl_slopeX * cl_pm.fl_powerIncrease;
                    }

                    if (cl_pm.fl_jumpY <= cl_pm.fl_limitY)
                    {
                        cl_pm.fl_jumpY = Mathf.Sqrt(cl_pm.fl_powerIncrease) + cl_pm.fl_slopeY * cl_pm.fl_powerIncrease;
                    }
                }

                // releasing space releases the jumpForce into the velocity of the Rigidbody of the player object
                // velocity x of the Rigidbody is calculated with walkInput, walkSpeed, and jumpWidthScale
                if (Input.GetKeyUp(KeyCode.Space) && cl_pm.bl_onGround)
                {
                    if(cl_pm.anim_player.GetFloat("Jumpvalue") > 2)
                    {
                        cl_pm.anim_player.SetInteger("Move Index", 2);
                    }
                    cl_pm.fl_powerIncrease = 0;
                    cl_pm.anim_player.SetFloat("Jumpvalue", 0);
                    bl_gotSpace = true;
                    cl_ps.arr_audios[1].Stop();

                }
            }



            // states pause bool true or false
            if (Input.GetKeyUp(KeyCode.Escape) && !cl_fsm.bl_startscreen && !cl_fsm.bl_menu && !cl_fsm.bl_end)
            {
                cl_fsm.bl_pause = !cl_fsm.bl_pause;
                if(cl_fsm.bl_pause)
                {
                    cl_mm.arr_gameMusic[4].Play();
                    cl_mm.arr_gameMusic[2].Stop();
                }
                else
                {
                    cl_mm.arr_gameMusic[2].Play();

                }
            }
        }

    }
}
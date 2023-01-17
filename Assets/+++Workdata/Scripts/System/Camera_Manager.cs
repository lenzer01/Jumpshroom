using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Manages the Movement and Animation of the Camera
    /// <br>From Tutorial: https://www.youtube.com/watch?v=3qfbJ-JSrOc&t=210s </br>
    /// </summary>
    public class Camera_Manager : MonoBehaviour
    {
        [SerializeField] internal BoxCollider2D bc2_camera;     // link to Boxcollider of the Camera
        [SerializeField] internal Transform tr_player;          // position of the Player
        [SerializeField] internal FrameSystem_Manager cl_fsm;   // link to Framesystem_Manager Instance

        private void Start()
        {
            transform.position = new Vector3(0, 112, -10);
        }

        void Update()
        {
            AspectRatioAdjustment();
            if(!cl_fsm.bl_startscreen)
            {
                CamFollowPlayer();
            }
        }

        /// <summary>
        /// Adjust bc2_camera to the Current Aspect Ratio of the main camera
        /// </summary>
        void AspectRatioAdjustment()
        {
            bc2_camera.size = new Vector2((Camera.main.orthographicSize * 2) * Camera.main.aspect, Camera.main.orthographicSize * 2);

        }

        /// <summary>
        /// Manages the Camera following the Player
        /// </summary>
        void CamFollowPlayer()
        {
            if(GameObject.Find("Boundary"))
            {
                transform.position = new Vector3(Mathf.Clamp(tr_player.position.x, 
                                                             GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + bc2_camera.size.x / 2, 
                                                             GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - bc2_camera.size.x / 2),

                                                 Mathf.Clamp(tr_player.position.y, 
                                                             GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + bc2_camera.size.y / 2, 
                                                             GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - bc2_camera.size.y / 2),

                                                 transform.position.z);
            }
        }

    }
}
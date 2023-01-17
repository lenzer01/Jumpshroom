using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Manages properties of the player object
    /// </summary>
    public class Player_Manager : MonoBehaviour
    {
        [SerializeField]
        internal float    fl_power, fl_slopeX, fl_slopeY, fl_minX,              //floats to control the jumping parable
                        fl_minY, fl_limitX, fl_limitY, fl_jumpX, fl_jumpY;
        [HideInInspector]
        public float fl_powerIncrease, fl_powerMax;                             // float to increase the power float to by time
        //[HideInInspector]
        public float fl_jumpDirection;                                          // int to determine the direction of the jump
        [SerializeField]
        internal bool bl_onGround, bl_onSlope, bl_coll_left, bl_coll_right;     // bools to check if the player is able to jump and to check collision
        [SerializeField]
        internal Rigidbody2D rb_player;                                         // the rigidbody of the player Object
        [SerializeField]
        internal LayerMask lm_groundCheck, lm_slopeCheck;                       // the Layermask on which the Ground is placed
        [SerializeField]
        internal PhysicsMaterial2D mat_playerMaterial, mat_slidingMaterial, 
            mat_bounceMaterial;                                                 // material for sliding off from slopes and standard material
        internal Vector2 v2_lastVelocity;                                       // Vector2 to control wall bouncing
        [SerializeField]
        private float fl_groundcheckBox_xSize, fl_groundcheckBox_ySize;         // floats to control size of ground overlap box
        [SerializeField]
        private float fl_collcheckBox_xSize, fl_collcheckBox_ySize;             // floats to control size of collision overlap box
        [SerializeField]
        private float fl_collcheckBox_xPos, fl_collcheckBox_yPos;               // floats to control position of collision overlap box
        [SerializeField]
        private float fl_groundBox_yposition, fl_groundBox_xposition;           // float to control displacement of ground overlap box
        [SerializeField]
        internal Animator anim_player;                                          // link to Aniamtor Component of the Player
        public bool bl_isJumping;                                               // bool to indicate the players jump
        public Vector2 direction;                                               // vector used to direct raycast
        public float rayCastYPosition;                                          // float to reposition the origin point of raycast
        public bool bl_stuck;                                                   // bool to debug when player can't move
        public float lastHitDistance;                                           // saves Distance of Raycast Hit
        public float fl_fallingThreshold;                                       // minimum velocity value to trigger the "falling down" animation
        public bool bl_falling;                                                 // bool to check if the falling threshold was surpassed



        /// <summary>
        /// Generates a boxtrigger and checks, if it's hitting into an object of a specified Layermask. 
        /// First vector is position, second vector is dimension, third is angle, last is Layermask
        /// </summary>
        void Update()
        {
            bl_onGround = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - fl_groundBox_xposition, gameObject.transform.position.y - fl_groundBox_yposition),
                                                 new Vector2(fl_groundcheckBox_xSize, fl_groundcheckBox_ySize), 0f, lm_groundCheck);

            bl_onSlope = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - fl_groundBox_xposition, gameObject.transform.position.y - fl_groundBox_yposition),
                                                 new Vector2(1, fl_groundcheckBox_ySize), 0f, lm_slopeCheck);


            // Cast a ray in the direction specified in the inspector.
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + fl_jumpDirection/3, transform.position.y - rayCastYPosition), direction, lm_groundCheck);

            Debug.DrawLine(new Vector2(transform.position.x + fl_jumpDirection / 3, transform.position.y - rayCastYPosition), direction);
            
            lastHitDistance = hit.distance;

            
            // starts landing Coroutine when player is near ground
            if (rb_player.sharedMaterial == mat_bounceMaterial && hit.distance < 2f && bl_isJumping && rb_player.velocity.normalized.y < 0)
            {
                rb_player.sharedMaterial = mat_playerMaterial;
                anim_player.SetInteger("Move Index", 4);
                bl_isJumping = false;
                if(bl_falling)
                {
                    anim_player.SetTrigger("Hit Ground");
                    bl_falling = false;
                }
            }
            else if(hit.distance < 1f && bl_isJumping && rb_player.velocity.normalized.y < 0)
            {
                anim_player.SetInteger("Move Index", 4);
                bl_isJumping = false;
                if (bl_falling)
                {
                    anim_player.SetTrigger("Hit Ground");

                    bl_falling = false;
                }
            }


            if (bl_onSlope && !bl_onGround)
            {
                rb_player.sharedMaterial = mat_slidingMaterial;
            }
            else if(bl_onGround && rb_player.sharedMaterial == mat_slidingMaterial && hit.distance < 2)
            {
                rb_player.sharedMaterial = mat_playerMaterial;
                rb_player.velocity = new Vector2(0, 0);
                if (bl_falling)
                {
                    anim_player.SetTrigger("Hit Ground");

                    bl_falling = false;
                }
            }

            if(bl_onGround && bl_falling)
            {
                anim_player.SetTrigger("Hit Ground");
                bl_falling = false;
            }

            // managing players sprite direction
            if (fl_jumpDirection == 1)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else if (fl_jumpDirection == -1)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }


            if(rb_player.velocity.y < fl_fallingThreshold)
            {
                bl_falling = true;
            }
        }
        
        /// <summary>
        /// Draws the generated overlapBox
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - fl_groundBox_xposition, gameObject.transform.position.y - fl_groundBox_yposition),
                                                 new Vector2(fl_groundcheckBox_xSize, fl_groundcheckBox_ySize));
            
        }

        /// <summary>
        /// transfers build up jump values to velocity of rb_player
        /// </summary>
        public void ReleaseJump()
        {
            if (fl_jumpX < fl_minX)
            {
                fl_jumpX = fl_minX;
            }

            if (fl_jumpY < fl_minY)
            {
                fl_jumpY = fl_minY;
            }

            rb_player.velocity = new Vector2(fl_jumpDirection * fl_jumpX, fl_jumpY);

            bl_isJumping = true;

            if (fl_jumpY > fl_minY)
            {
                IsJumping();
            }

            fl_jumpX = 0;

            fl_jumpY = 0;

        }

        /// <summary>
        /// Gives bouncing material to player while jumping
        /// </summary>
        private void IsJumping()
        {
            rb_player.sharedMaterial = mat_bounceMaterial;
            anim_player.SetInteger("Move Index", 3);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonShooter
{

    public class MoveMentController : MonoBehaviour
    {
        [Header("Setting")]
        public float MoveVelocity;
        public float JumpHeight;
        public float GravityScale;

        /// <summary>
        /// move character toward the direction relative to character's local cordinate
        /// </summary>
        public void Move(Vector2 direction)
        {

        }

        /// <summary>
        /// character jump
        /// </summary>
        public void Jump()
        {

        }
    }

}

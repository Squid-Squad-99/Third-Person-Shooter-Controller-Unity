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

        [Header("Reference")]
        [SerializeField] Transform VCamTarget;

        /// <summary>
        /// move character toward the direction relative to character's local cordinate
        /// </summary>
        public void Move(Vector2 direction)
        {
            // get world direction
            Vector3 worldDirection;
            VCamTarget.forward
        }

        /// <summary>
        /// character jump
        /// </summary>
        public void Jump()
        {

        }
    }

}

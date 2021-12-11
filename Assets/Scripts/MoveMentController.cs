using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonShooter
{

    public class MovementController : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] float _moveVelocity;
        [SerializeField] float _jumpHeight;
        [SerializeField] float _jumpBufferSec;
        [SerializeField] float _gravityScale;
        [SerializeField] float _groundOffset;
		[SerializeField] float _groundedRadius;
		[SerializeField] LayerMask _groundLayers;
        [SerializeField] float RotationSmoothTime;
        public MoveMethodEnum MoveMethod;

        [Header("State")]
        [SerializeField] bool _grounded;

        [Header("Reference")]
        [SerializeField] Transform _vCamTarget;
        [SerializeField] Animator _animator;

        private Rigidbody _rigidbody;
        // move handling
        private Vector2 _direction;
        private Vector3 _targetVelocity;
        private float _targetRotation;
        // jump handling
        float _jumpBufferDelta;
        private bool _jump;
        private Vector3 _jumpVelocity;
        // animation IDs
		private int _animIDSpeed;
		private int _animIDJump;
		private int _animIDGround;

        public enum MoveMethodEnum{
            RigiLike,
            KinematicLike,
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _jumpVelocity = Mathf.Sqrt(2 * 9.8f * _gravityScale * _jumpHeight) * Vector3.up;

        }

        private void Start() {
            AssignAnimationIDs();
        }

        private void FixedUpdate() {
            GroundedCheck();
            FaceFront();
            if(MoveMethod == MoveMethodEnum.KinematicLike) MoveHandling();
            JumpHandling();
            ApplyGravity();

            SetAnimationParam();

        }

        /// <summary>
        /// move character toward the direction relative to character's local cordinate
        /// </summary>
        public void Move(Vector2 direction)
        {
            _direction = direction;
        }

        /// <summary>
        /// character jump
        /// </summary>
        public void Jump()
        {
            _jump = true;
        }

        private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDGround = Animator.StringToHash("Ground");
		}
        
        [SerializeField] float AnimSpeedChangeRate;
        float currentAnimSpeed = 0f;
        private void SetAnimationParam()
        {
            // speed
            float horizontalVelocityMagnitude = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).magnitude;
            currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, horizontalVelocityMagnitude, Time.fixedDeltaTime * AnimSpeedChangeRate);
            _animator.SetFloat(_animIDSpeed, currentAnimSpeed);
            // ground
            _animator.SetBool(_animIDGround, _grounded);
        }

        private void MoveHandling(){
            if(_direction != Vector2.zero){
                // get rotation & direction
                _targetRotation = _vCamTarget.eulerAngles.y + Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
			    Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                _targetVelocity = targetDirection * _moveVelocity; 

            }
            else{
                _targetVelocity = Vector3.zero;
            }

            // change velocity
            Vector3 diffVelocity = _targetVelocity - new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(diffVelocity, ForceMode.VelocityChange);

        }

        private void FaceFront(){
            _targetRotation = _vCamTarget.eulerAngles.y + Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
            float _rotationVelocity = 0.0f;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
            // face direction
            _rigidbody.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            _rigidbody.angularVelocity = Vector3.zero;
        }
        
        private void JumpHandling()
        {
            _animator.SetBool(_animIDJump, false);

            if(_grounded){
                _jumpBufferDelta = 0;

                if(_jump){
                    Vector3 diffVelocity = _jumpVelocity - Vector3.Scale(Vector3.up, _rigidbody.velocity);
                    // Jump
                    _rigidbody.AddForce(diffVelocity, ForceMode.VelocityChange);
                    //animation
                    _animator.SetBool(_animIDJump, true);
                }
                _jump = false;            
            }
            else{
                if(_jump){
                    // want to jump but not grounded
                    _jumpBufferDelta += Time.fixedDeltaTime;
                    if(_jumpBufferDelta >= _jumpBufferSec){
                        _jump = false;
                    }
                    else{
                        return;
                    }
                }
            }

        }

        private void ApplyGravity(){
            _rigidbody.AddForce(9.8f * _gravityScale * Vector3.down, ForceMode.Acceleration);
        }

        private void GroundedCheck(){
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundOffset, transform.position.z);
			_grounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
        }

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (_grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;
			
			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - _groundOffset, transform.position.z), _groundedRadius);
		}
    }

}

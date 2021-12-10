using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ThirdPersonShooter
{

    public class CameraMovementByCharacterInput : MonoBehaviour
    {

        [Header("Setting")]
        public float MouseSensitivity;
        public int TopClamp;
        public int BottomClamp;

        [Header("Reference")]
        [SerializeField] Transform _playerVCamTarget;

        private Vector2 _inputValue;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private ICharacterInputBroadcaster characterInputBroadcaster;
        private UnityEvent<Vector2> _inputLookEvent;

        private void Awake()
        {
            // Get character input broadcaster
            characterInputBroadcaster = GetComponent<ICharacterInputBroadcaster>();
        }

        private void Start()
        {
            // hook input event
            _inputLookEvent = characterInputBroadcaster.InputLookEvent;
            _inputLookEvent.AddListener(OnInputLook);
        }


        private void LateUpdate()
        {
            // rotate camera
            RotateCamera(_inputValue);
        }

        private void OnInputLook(Vector2 inputValue)
        {
            _inputValue = inputValue;
        }

        private void RotateCamera(Vector2 inputValue)
        {
            _cinemachineTargetYaw += inputValue.x * Time.deltaTime * MouseSensitivity;
            _cinemachineTargetPitch += inputValue.y * Time.deltaTime * MouseSensitivity;

            // clamp angle
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -BottomClamp, TopClamp);

            _playerVCamTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }

}

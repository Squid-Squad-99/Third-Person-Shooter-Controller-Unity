using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ThirdPersonShooter{

    public class MovementByInput : MonoBehaviour
    {
        private MovementController _moveMentController;

        private UnityEvent<Vector2> _inputMoveEvent;
        private UnityEvent _inputJumpEvent;


        private void OnEnable() {
            // get reference
            ICharacterInputBroadcaster characterInputBroadcaster = GetComponent<ICharacterInputBroadcaster>();
            _inputMoveEvent = characterInputBroadcaster.InputMoveEvent;
            _inputJumpEvent = characterInputBroadcaster.InputJumpEvent;
            _moveMentController = GetComponent<MovementController>();

            // hook input events
            _inputMoveEvent.AddListener(OnInputMove);
            _inputJumpEvent.AddListener(OnInputJump);
        }

        private void OnDisable() {
            _inputMoveEvent.RemoveListener(OnInputMove);
            _inputJumpEvent.RemoveListener(OnInputJump);
        }

        private void OnInputJump()
        {
            _moveMentController.Jump();
        }


        private void OnInputMove(Vector2 inputValue)
        {
            _moveMentController.Move(inputValue);
        }
    }

}

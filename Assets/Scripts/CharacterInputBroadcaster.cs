using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ThirdPersonShooter
{

    public class CharacterInputBroadcaster : MonoBehaviour, ICharacterInputBroadcaster
    {

        public UnityEvent InputFireEvent => _inputFireEvent;
        public UnityEvent InputJumpEvent => _inputJumpEvent;
        public UnityEvent<Vector2> InputLookEvent => _inputLookEvent;
        public UnityEvent<Vector2> InputMoveEvent => _inputMoveEvent;

        public UnityEvent _inputFireEvent;
        public UnityEvent _inputJumpEvent;
        public UnityEvent<Vector2> _inputLookEvent;
        public UnityEvent<Vector2> _inputMoveEvent;

        void OnMove(InputValue inputValue)
        {
            InputMoveEvent.Invoke(inputValue.Get<Vector2>());
        }

        void OnLook(InputValue inputValue)
        {
            InputLookEvent.Invoke(inputValue.Get<Vector2>());
        }

        void OnJump(InputValue inputValue)
        {
            InputJumpEvent.Invoke();
        }

        void OnFire(InputValue inputValue)
        {
            InputFireEvent.Invoke();
        }
    }
}


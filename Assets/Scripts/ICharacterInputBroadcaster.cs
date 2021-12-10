using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ThirdPersonShooter
{

    public interface ICharacterInputBroadcaster
    {
        public UnityEvent InputFireEvent { get;}
        public UnityEvent InputJumpEvent { get;}
        public UnityEvent<Vector2> InputLookEvent { get;}
        public UnityEvent<Vector2> InputMoveEvent { get;}
    }

}


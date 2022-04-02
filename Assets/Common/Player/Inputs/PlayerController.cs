using System;
using Common.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Player.Inputs
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        public static event Action OnPause;

        public event Action OnSprint;
        public event Action OnAction1;
        public event Action OnAction2;
        
        private PlayerInputs _inputs;

        private void OnEnable()
        {
            _inputs = new PlayerInputs();
            _inputs.Gameplay.Enable();

            _inputs.Gameplay.Sprint.performed  += HandleSprint;
            _inputs.Gameplay.Action1.performed += HandleAction1;
            _inputs.Gameplay.Action2.performed += HandleAction2;
            _inputs.Gameplay.Pause.performed   += HandlePause;
        }

        private void OnDisable()
        {
            _inputs.Gameplay.Sprint.performed  -= HandleSprint;
            _inputs.Gameplay.Action1.performed -= HandleAction1;
            _inputs.Gameplay.Action2.performed -= HandleAction2;
            _inputs.Gameplay.Pause.performed   -= HandlePause;
        }

        private void HandleSprint(InputAction.CallbackContext  obj) => OnSprint?.Invoke();
        private void HandleAction1(InputAction.CallbackContext obj) => OnAction1?.Invoke();
        private void HandleAction2(InputAction.CallbackContext obj) => OnAction2?.Invoke();
        private void HandlePause(InputAction.CallbackContext   obj) => OnPause?.Invoke();

        public Vector2 GetDirection() => _inputs.Gameplay.Move.ReadValue<Vector2>();
    }
}
using Common.Scripts;
using UnityEngine;

namespace Common.Player.Scripts
{
    public class GooseMotor : Motor
    {
        public GooseMotor(Rigidbody rb, Transform modelTransform, Animator animator, float maxSpeed) : base(rb, modelTransform, animator, maxSpeed)
        {
        }
    }
    
    public class PlayerMotor : Motor
    {
        private const float SPRINT_SCALE    = 3f;
        private const float SPRINT_DURATION = 2.5f;
        private const float SPRINT_COOLDOWN = 4f;
        
        private float _lastSprint = -SPRINT_COOLDOWN;
        private bool  _isSprinting;
        
        public void Sprint()
        {
            if(Time.time <= _lastSprint + SPRINT_COOLDOWN)
                return;
            
            ScaleSpeed(SPRINT_SCALE);
            _isSprinting = true;
            _lastSprint  = Time.time;
        }

        public PlayerMotor(Rigidbody rb, Transform modelTransform, Animator animator, float maxSpeed) 
            : base(rb, modelTransform, animator, maxSpeed)
        {
        }

        public override void Move(Vector2 direction)
        {
            base.Move(direction);

            if(_isSprinting && Time.time >= _lastSprint + SPRINT_DURATION)
            {
                _isSprinting = false;
                ScaleSpeed();
            }
        }

        protected override Vector2 GetDisplacement(Vector2 direction)
        {
            // TODO: Smooth out movement
            return base.GetDisplacement(direction);
        }

        protected override void SetMoving(bool moving)
        {
            base.SetMoving(moving);
            // TODO: Add dust particles of some sort
        }
    }
}
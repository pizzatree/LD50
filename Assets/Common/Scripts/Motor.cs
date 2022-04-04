using UnityEngine;

namespace Common.Scripts
{
    public abstract class Motor
    {
        private static readonly int   MovingAnimatorProperty = Animator.StringToHash("Moving");
        private static readonly int   SpeedAnimatorProperty  = Animator.StringToHash("Speed");
        private const           float MOVING_CUTOFF_SPEED    = 0.5f;

        public float Speed    { get; private set; }
        public float Distance { get; private set; }

        private readonly Rigidbody _rb;
        private readonly Transform _modelTransform;
        private readonly Animator  _animator;

        private float _maxSpeed;
        private float _moveScale;
        private bool  _moving;

        private Quaternion _targetRotation;

        protected Motor(Rigidbody rb, Transform modelTransform, Animator animator, float maxSpeed)
        {
            _moveScale = 1f;

            _maxSpeed       = maxSpeed;
            _rb             = rb;
            _modelTransform = modelTransform;
            _animator       = animator;
        }

        public virtual void Move(Vector2 direction)
        {
            if(direction.magnitude > 1f)
                direction = direction.normalized;
    
            var displacement = GetDisplacement(direction);
            Speed = _moveScale * displacement.magnitude * _maxSpeed;
            
            UpdateModelRotation(direction);
            UpdateAnimator(Speed / _maxSpeed);
            
            Distance += Mathf.Abs(Speed                                               * Time.deltaTime);
            _rb.MovePosition(_rb.position + new Vector3(direction.x, 0f, direction.y) * (Speed * Time.deltaTime));
        }

        public bool MoveTowards(Vector3 position, float distance = 1.5f)
        {
            var dist = position - _rb.position;

            if(dist.magnitude <= distance)
                return true;
            
            dist.Normalize();
            Move(new Vector2(dist.x, dist.z));
            return false;
        }

        public void ScaleSpeed(float newScale = 1f) => _moveScale = newScale;

        protected virtual void SetMoving(bool moving) 
            => _animator?.SetBool(MovingAnimatorProperty, moving);
        
        private void UpdateAnimator(float displacement)
        {
            var absDisplacement = Mathf.Abs(displacement);
            var nowMoving       = absDisplacement > MOVING_CUTOFF_SPEED;
            
            if(nowMoving)
                _animator?.SetFloat(SpeedAnimatorProperty, absDisplacement);

            if(_moving == nowMoving)
                return;

            _moving = nowMoving;
            SetMoving(nowMoving);
        }

        private void UpdateModelRotation(Vector2 direction)
        {
            if(direction.magnitude >= MOVING_CUTOFF_SPEED)
                _targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.y), _rb.transform.up);
            
            _modelTransform.rotation = Quaternion.RotateTowards(_modelTransform.rotation, _targetRotation, 720f * Time.deltaTime);
        }

        protected virtual Vector2 GetDisplacement(Vector2 direction) => direction;
    }
}
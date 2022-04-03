using System.Linq;
using _Plugins.TopherUtils;
using UnityEngine;

namespace Common.Goose.Scripts
{
    public class Bonker : Goose, IBonker
    {
        private Bonkable  _target;
        private Transform _targetTransform;

        protected override void Start()
        {
            base.Start();

            FindNewTarget();
        }

        protected override void Update()
        {
            base.Update();

            if(IsStunned())
                return;
            
            if(!_target)
            {
                FindNewTarget();
                return;
            }
            
            HandleMovement();
            
            if(IsWithinRange() && Bonk())
                FindNewTarget();
        }

        private bool IsWithinRange() => Vector3.Distance(transform.position, _targetTransform.position) <= 3f;

        private void HandleMovement()
        {
            var nextPos = _rb.position    + transform.forward * (_speed * Time.deltaTime);
            _rb.rotation = Quaternion.RotateTowards(_rb.rotation, 
                                                    Quaternion.LookRotation(Vector3.MoveTowards(_rb.position, _targetTransform.position, 1f)), 
                                                    90f * Time.deltaTime );
            _rb.MovePosition(nextPos);
            // var dir      = transform.forward * .2f + (_targetTransform.position - transform.position) * .8f;
            // var nextMove = new Vector2(dir.x, dir.z).normalized;
            // _motor.Move(nextMove * _speed);
        }

        private bool Bonk()
        {
            _animator.SetTrigger("Bonk");
            return false;
        }

        private void FindNewTarget()
        {
            _speed = _speedRange.RandomValue() * 1.25f;

            _target = GameManager.Instance
                                 .Bonkables
                                 .Where(b => b != _target)
                                 .OrderBy(b => Vector3.Distance(b.transform.position, transform.position))
                                 .FirstOrDefault();

            if(!_target)
                return;
            
            _targetTransform = _target.transform;
        }

        public int GetBonkValue()
        {
            return 9001;
        }
    }
}
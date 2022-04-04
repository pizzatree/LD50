using _Plugins.TopherUtils;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Goose.Scripts
{
    public class Honker : Goose
    {
        private Vector3 _targetDestination;

        protected override void Start()
        {
            base.Start();
            _targetDestination = FindNewDestination();
        }

        protected override void HandleThrown()
        {
            base.HandleThrown();
            FindNewDestination();
        }

        protected override void Update()
        {
            base.Update();
            
            if(IsStunned())
                return;
            
            HandleMovement();
        }

        private void HandleMovement()
        {
            if(ReachedDestination())
                _targetDestination = FindNewDestination();

            var dir      = _targetDestination - transform.position;
            var nextMove = new Vector2(dir.x, dir.z).normalized;
            _motor.Move(nextMove * _speed);
        }
        
        private bool ReachedDestination() => Vector3.Distance(transform.position, _targetDestination) <= 3f;

        private Vector3 FindNewDestination()
        {
            _speed = _speedRange.RandomValue();
            return new Vector3(Random.Range(-10f, 10f), 0f, (Random.Range(-10f, 10f)));
        }
    }
}
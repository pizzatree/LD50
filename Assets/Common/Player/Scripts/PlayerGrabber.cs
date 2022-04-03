using Common.Scripts;
using UnityEngine;

namespace Common.Player.Scripts
{
    public class PlayerGrabber : MonoBehaviour
    {
        private static readonly int Grab  = Animator.StringToHash("Grab");
        private static readonly int Throw = Animator.StringToHash("Throw");

        [SerializeField] private Transform _handsPoint;

        private Animator       _animator;
        private Motor          _motor;
        private ICanBePickedUp _entityInHands;

        public void Init(Animator animator, Motor motor)
        {
            _animator = animator;
            _motor    = motor;
        }

        public void HandleGrabThrow()
        {
            if(_entityInHands == null)
            {
                _animator.SetTrigger(Grab);
                return;
            }

            _animator.SetTrigger(Throw);
            _entityInHands?.Throw(transform.forward, Mathf.Max(3f, Mathf.Min(10f, 2f * _motor.Speed)));
            _entityInHands = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_entityInHands != null)
                return;

            var entity = other.gameObject.GetComponentInParent<ICanBePickedUp>();
            if(entity == null)
                return;

            _entityInHands = entity.Grab(_handsPoint);
        }
    }
}
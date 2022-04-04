using System;
using System.Linq;
using System.Threading.Tasks;
using _Plugins.TopherUtils;
using UnityEngine;

namespace Common.Goose.Scripts
{
    public class Bonker : Goose
    {
        private BonkBox   _bonkBox;
        private Bonkable  _target;
        private Transform _targetTransform;

        protected override void Start()
        {
            base.Start();

            _bonkBox        =  GetComponentInChildren<BonkBox>();
            _bonkBox.OnBonk += HandleBonk;
            FindNewTarget();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _bonkBox.OnBonk -= HandleBonk;
        }

        private void HandleBonk()
        {
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
            
            if(IsWithinRange())
                Bonk();
                
        }

        private bool IsWithinRange() => Vector3.Distance(transform.position, _targetTransform.position) <= 3f;

        private void HandleMovement()
        {
            var nextPos = _rb.position + transform.forward * (_speed * Time.deltaTime);
            var rot     = Quaternion.LookRotation(_targetTransform.position - transform.position);
            _rb.rotation = Quaternion.RotateTowards(_rb.rotation,
                                                    rot,
                                                    65f * Time.deltaTime );
            
            _rb.MovePosition(nextPos);

        }

        private void Bonk()
        {
            if(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Bonk")
                _animator.SetTrigger("Bonk");
        }

        private void FindNewTarget()
        {
            _speed = _speedRange.RandomValue();

            _target = GameManager.Instance
                                 .Bonkables
                                 .Where(b => b != _target)
                                 .OrderBy(b => Vector3.Distance(b.transform.position, transform.position))
                                 .FirstOrDefault();

            if(!_target)
                return;
            
            _targetTransform = _target.transform;
        }
    }
}
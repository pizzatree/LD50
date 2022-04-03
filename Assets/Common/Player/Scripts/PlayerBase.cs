using System.Threading;
using Common.Player.Inputs;
using Common.Scripts;
using UnityEngine;

namespace Common.Player.Scripts
{
    public class PlayerBase : Bonkable
    {
        [SerializeField] private float _maxSpeed = 5f;

        private Transform _modelTransform;
        private Animator  _animator;
        private Rigidbody _rb;

        private PlayerInteractor  _interactor;
        private PlayerGrabber     _grabber;
        private PlayerMotor       _motor;
        private IEntityController _controller;

        private CancellationTokenSource _cts;

        protected override void Start()
        {
            base.Start();
            
            _animator   = GetComponentInChildren<Animator>();
            _interactor = GetComponentInChildren<PlayerInteractor>(true);
            _grabber    = GetComponentInChildren<PlayerGrabber>(true);
            _controller = gameObject.AddComponent<PlayerController>();
            _rb         = GetComponent<Rigidbody>();
         
            _modelTransform = transform.Find("Model");

            _cts   = new CancellationTokenSource();
            _motor = new PlayerMotor(_rb, _modelTransform, _animator, _maxSpeed);

            _interactor.Init(_cts.Token, _animator);
            _grabber.Init(_animator, _motor);

            _controller.OnSprint  += _motor.Sprint;
            _controller.OnAction1 += _grabber.HandleGrabThrow;
            _controller.OnAction2 += _interactor.FindInteraction;
        }

        protected override void OnDisable()
        {
            _cts.Cancel();

            _controller.OnSprint  -= _motor.Sprint;
            _controller.OnAction1 -= _grabber.HandleGrabThrow;
            _controller.OnAction2 -= _interactor.FindInteraction;
        }

        private void Update()
        {
            _motor.Move(_controller.GetDirection());
        }

        public override void OnBonk(IBonker bonker)
        {
            // TODO: Finish me
        }
    }
}
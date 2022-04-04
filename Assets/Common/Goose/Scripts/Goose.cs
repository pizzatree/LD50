using System.Threading;
using System.Threading.Tasks;
using _Plugins.TopherUtils;
using Common.Player.Scripts;
using Common.Scripts;
using UnityEngine;
using UnityEngine.Events;
using VistorNavDev.Scripts;

namespace Common.Goose.Scripts
{
    [RequireComponent(typeof(Grabbable))]
    public class Goose : MonoBehaviour
    {
        [Header("Customization")] [SerializeField]
        protected Vector2 _speedRange = new Vector2(1f, 3f);

        [SerializeField] protected Vector2 _eatingDelayRange = new Vector2(2f,   5f);
        [SerializeField] private   Vector2 _pooBufferRange   = new Vector2(1.5f, 3f);

        [Header("Events")] [SerializeField] private UnityEvent _onPoop;
        [SerializeField]                    private UnityEvent _onThrown;
        [SerializeField]                    private UnityEvent _onGrabbed;

        [Header("Dependencies")] [SerializeField]
        private GameObject _pooPrefab;

        [SerializeField] protected GameObject _bat;
        [SerializeField] protected bool       _usesBat;
        [SerializeField] private   Transform  _pooPoint;

        private   Grabbable       _grabbable;
        private   GooseCollisions _gooseCollisions;
        private   Transform       _modelTransform;
        protected Animator        _animator;
        protected Rigidbody       _rb;
        protected Motor           _motor;

        [Header("Other")] [SerializeField] protected bool _held, _eating, _pooping;

        private   CancellationTokenSource _cts;
        protected float                   _speed;

        protected bool IsStunned() => _held || _eating || _pooping;

        protected virtual void Start()
        {
            _cts            = new CancellationTokenSource();
            _modelTransform = transform.Find("Model");

            _animator        = GetComponentInChildren<Animator>();
            _rb              = GetComponent<Rigidbody>();
            _grabbable       = GetComponent<Grabbable>();
            _gooseCollisions = gameObject.AddComponent<GooseCollisions>();
            _motor           = new GooseMotor(_rb, _modelTransform, _animator, _speedRange.y);

            _grabbable.OnThrown  += _gooseCollisions.ClapThemGeese;
            _grabbable.OnThrown  += HandleThrown;
            _grabbable.OnGrabbed += HandleGrabbed;

            Invoke(nameof(Poo), _pooBufferRange.RandomValue());
        }

        protected virtual void OnDisable()
        {
            _grabbable.OnThrown  -= _gooseCollisions.ClapThemGeese;
            _grabbable.OnThrown  -= HandleThrown;
            _grabbable.OnGrabbed -= HandleGrabbed;

            _cts.Cancel();
        }

        protected virtual void Update()
        {
        }

        protected virtual void HandleThrown()
        {
            _onThrown?.Invoke();

            _animator.SetTrigger("Thrown");
            _held = false;
        }

        private void HandleGrabbed()
        {
            _onGrabbed?.Invoke();

            _held = true;
        }

        protected async void Poo()
        {
            _pooping = true;
            _onPoop?.Invoke();
            // if over sidewalk

            _animator.SetTrigger("Poopoo");
            Instantiate(_pooPrefab, _pooPoint.position, transform.rotation);

            await Task.Delay(400);
            if(_cts.IsCancellationRequested)
                return;

            _pooping = false;
            Invoke(nameof(Poo), _pooBufferRange.RandomValue());
        }

        public async void EatBread(Bread bread)
        {
            if(!bread)
            {
                _eating = false;
                return;
            }

            var breadT = bread.transform;
            _eating = true;
            while(!_cts.IsCancellationRequested && !_motor.MoveTowards(breadT.position))
                await Task.Delay(10);

            while(!_cts.IsCancellationRequested
               && bread
               && Vector3.Distance(transform.position, breadT.position) <= 5f)
            {
                _animator.SetTrigger("Poopoo");
                bread?.GetEaten();
                await Task.Delay(2000);
            }

            _eating = false;
        }

        private void OnValidate() => _bat.SetActive(_usesBat);
    }
}
using _Plugins.TopherUtils;
using Common.Player.Scripts;
using Common.Scripts;
using UnityEngine;

namespace Common.Goose.Scripts
{
    [RequireComponent(typeof(Grabbable))]
    public class Goose : MonoBehaviour
    {
        [Header("Customization")] [SerializeField]
        protected Vector2 _speedRange = new Vector2(1f, 3f);

        [SerializeField] protected Vector2 _eatingDelayRange = new Vector2(2f,   5f);
        [SerializeField] private   Vector2 _pooBufferRange   = new Vector2(1.5f, 3f);

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

        [Header("Other")] [SerializeField] protected bool _held, _eating;

        protected float _speed;

        protected bool IsStunned() => _held || _eating;

        protected virtual void Start()
        {
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
        }

        protected virtual void Update()
        {
        }

        private void HandleThrown()
        {
            _animator.SetTrigger("Thrown");
            _held = false;
        }

        private void HandleGrabbed() => _held = true;

        protected void Eat()
        {
            // TODO: Finish me when bread is made
            // if _curBread is null
            // eating = false
            // return

            // _curBread.Eat()
            // if curBread is null
            _eating = false;

            if(_eating)
                Invoke(nameof(Eat), _eatingDelayRange.RandomValue());
        }

        protected void Poo()
        {
            // if over sidewalk

            _animator.SetTrigger("Poopoo");
            Instantiate(_pooPrefab, _pooPoint.position, transform.rotation);
            Invoke(nameof(Poo), _pooBufferRange.RandomValue());
        }

        private void OnValidate() => _bat.SetActive(_usesBat);
    }
}
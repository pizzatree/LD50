using _Plugins.TopherUtils;
using CleverCrow.Fluid.BTs.Trees;
using Common.Player.Scripts;
using Common.Scripts;
using UnityEngine;

namespace Common.Goose.Scripts
{
    [RequireComponent(typeof(Grabbable))]
    public class Goose : MonoBehaviour
    {
        [Header("Customization")] 
        [SerializeField] protected Vector2 _speedRange = new Vector2(1f, 3f);
        [SerializeField] protected Vector2 _eatingDelayRange = new Vector2(2f, 5f);
        
        [Header("Dependencies")] 
        [SerializeField] private GameObject _pooPrefab;
        [SerializeField] protected GameObject _bat;
        [SerializeField] protected bool       _usesBat;
        [SerializeField] private   Transform  _pooPoint;
        
        [Header("Behavior Tree")] 
        [SerializeField] protected BehaviorTree _bt;

        protected bool Stunned() => _held || _eating;
        
        private   Grabbable       _grabbable;
        private   GooseCollisions _gooseCollisions;
        private   Transform       _modelTransform;
        private   Animator        _animator;
        private   Rigidbody       _rb;
        protected Motor           _motor;

        protected                  float _speed;
        [SerializeField] protected bool  _held, _eating;

        private void Start()
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
            
            _bt = CreateBehaviorTree();
            
            InvokeRepeating(nameof(Poo), 1f, 5f);
        }

        private void OnDisable()
        {
            _grabbable.OnThrown  -= _gooseCollisions.ClapThemGeese;
            _grabbable.OnThrown  -= HandleThrown;
            _grabbable.OnGrabbed -= HandleGrabbed;           
        }
        protected virtual BehaviorTree CreateBehaviorTree()
        {
            Debug.LogWarning($"Give {name} a behaviour tree.");
            return null;
        }

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

        private void Update()
        {
            _bt?.Tick();
        }

        private void HandleThrown()
        {
            _held = false;
        }

        private void HandleGrabbed()
        {
            _held = true;
        }

        private void Poo()
        {
            Instantiate(_pooPrefab, _pooPoint.position, transform.rotation);
        }
        
        private void OnValidate()
        {
            _bat.SetActive(_usesBat);
        }
    }
}
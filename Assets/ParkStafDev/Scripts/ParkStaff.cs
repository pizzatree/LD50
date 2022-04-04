using System;
using System.Threading;
using Common.Player.Scripts;
using Common.Scripts;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Grabbable))]
public abstract class ParkStaff : Bonkable, ICanBePickedUp
{
    public event Action OnDestroyed;

    [SerializeField] Grabbable _grabbable;
    [SerializeField] Vector2 _speedRange = new Vector2(1f, 3f);
    [SerializeField] protected RangedDetector _detector;
    [SerializeField] float _interactDistance = 1;
    [SerializeField] int _hp = 1;
    [SerializeField] int _staffCost = 1;
    
    protected PlayerGrabber _grabber;
    protected PlayerInteractor _interactor;
    protected Motor _motor;
    protected bool _isGrabbed = false;
    
    bool _isThrown = false;
    Transform _target = null;
    Rigidbody _rb;
    Transform _modelTransform;
    Animator _animator;
    CancellationTokenSource _cts;
    
    protected override void Start()
    {
        base.Start();
        _grabbable = GetComponent<Grabbable>();
        _grabbable.OnThrown += OnThrown;
        _rb = GetComponent<Rigidbody>();
        _modelTransform = transform.Find("Model");
        _animator = GetComponentInChildren<Animator>();
        _detector.transform.position = transform.position;
        _motor = new StaffMotor(_rb, _modelTransform, _animator, _speedRange.y);
        _cts   = new CancellationTokenSource();
        _grabber = GetComponentInChildren<PlayerGrabber>();
        _grabber.Init(_animator, _motor);
        _interactor = GetComponentInChildren<PlayerInteractor>();
        _interactor.Init(_cts.Token, _animator);
        _detector = Instantiate(_detector);
        GameManager.Instance.RegisterStaff(this);
    }

    protected virtual void Update()
    {
        if (_isGrabbed) { return;}
        if (!_target) { _target = null;}
        if (_target == null)
        {
            ReturnHome();
            FindTarget();
        }
        else if(Vector3.Distance(transform.position, _target.position) < _interactDistance){PerformStaffAction();}
        else if (Vector3.Distance(_detector.transform.position, _target.position) > _detector.Range) { _target = null;}
        else { MoveToTarget(); }
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
        GameManager.Instance.UnRegisterStaff(this);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<StaffSurface>() != null && _isThrown)
        {
            Debug.Log("StaffSurface");
            _isThrown = false;
            _detector.transform.position = transform.position;
        }
    }

    protected override void OnDisable() { _cts.Cancel(); }

    public override void OnBonk(int bonkValue, Vector3 bonkDirection)
    {
        _hp -= bonkValue;
        if (_hp <= 0) { Destroy(gameObject); }
    }
    
    public int StaffCost { get => _staffCost; }
    public ICanBePickedUp Grab(Transform grabber)
    {
        _isGrabbed = true;
        return _grabbable.Grab(grabber);
    }
    public void Throw(Vector3 direction, float magnitude)
    {
        throw new System.NotImplementedException();
    }
    public void OnThrown()
    {
        _isGrabbed = false;
        _isThrown = true;
    }

    protected void MoveToTarget()
    {
        Vector2 direction = new Vector2(_target.position.x - transform.position.x, _target.position.z - transform.position.z);
        _motor.Move(direction);
    }

    void FindTarget()
    {
        float minDistance = float.PositiveInfinity;
        Detectable detectable = null;
        
        for (int i = 0; i < _detector.Detectables.Count;)
        {
            if (!_detector.Detectables[i])
            {
                _detector.Detectables.RemoveAt(i);
                if (_detector.Detectables.Count == 0) { return; }
            }
            else
            {
                float distance = Vector3.Distance(transform.position, _detector.Detectables[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    detectable = _detector.Detectables[i];
                }
                i++;
            }
        }
        
        if (detectable != null) { _target = detectable.transform; } 
    }

    void ReturnHome()
    {
        if (Vector3.Distance(transform.position, _detector.transform.position) > 0.5f)
        {
            Vector2 direction = new Vector2(_detector.transform.position.x - transform.position.x, _detector.transform.position.z - transform.position.z);
            _motor.Move(direction);
            return;
        }
        
    }

    protected abstract void PerformStaffAction();
}
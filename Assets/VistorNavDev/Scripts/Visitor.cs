
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(VistorNavigator))]
public abstract class Visitor : MonoBehaviour, IBonker
{
    [SerializeField] GameObject _virtualCamera;
    [SerializeField] int _minSitAtBenchTime;
    [SerializeField] int _maxSitAtBenchTime;
    [SerializeField] int _bonkValue = 1;
    VistorNavigator _navigator;

    void Start() { GameManager.Instance.RegisterVisitor(); }

    public void Initialize() { _navigator = GetComponent<VistorNavigator>(); }

    void OnDestroy() { GameManager.Instance.UnRegisterVisitor(); }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Turd"))
        {
            _navigator.MoveSpeed = 0;
            VirtualCameraManager.Instance.PanToVisitor(_virtualCamera);
            return;
        }
        ParkBench parkBench = other.GetComponent<ParkBench>();
        if(parkBench){InteractWithParkBench(parkBench);}
    }
    
    public Transform ReturnPoint { set => _navigator.ReturnPoint = value; }

    protected abstract void InteractWithParkBench(ParkBench parkBench);

    public IEnumerator SitAtBench(ParkBench parkBench)
    {
        if (parkBench.Occupied) { yield break;}
        transform.position = parkBench.VisitorAnimationStartSpot.position;
        transform.rotation = parkBench.VisitorAnimationStartSpot.rotation;
        parkBench.Occupied = true;
        
        _navigator.MoveSpeed = 0;
        yield return new WaitForSeconds(Random.Range(_minSitAtBenchTime, _maxSitAtBenchTime));
        _navigator.ResetMoveSpeed();
        parkBench.Occupied = false;
    }
    public int GetBonkValue() { return _bonkValue; }
}
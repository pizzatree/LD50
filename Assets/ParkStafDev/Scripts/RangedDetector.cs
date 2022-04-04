using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedDetector : MonoBehaviour
{
    [SerializeField] DetectableType _detectableType;
    [SerializeField] float _range = 10;
    
    List<Detectable> _detectables = new List<Detectable>();

    void Start()
    {
        transform.localScale = new Vector3(_range, _range, _range);
    }

    void OnTriggerEnter(Collider other)
    {
        Detectable _detectable;
        if (other.gameObject.TryGetComponent<Detectable>(out _detectable))
        {
            if (_detectable.GetType() == _detectableType)
            {
                _detectables.Add(_detectable);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Detectable _detectable;
        if (other.gameObject.TryGetComponent<Detectable>(out _detectable))
        {
            if (_detectable.GetType() == _detectableType)
            {
                _detectables.Remove(_detectable);
            }
        }
    }

    public List<Detectable> Detectables { get => _detectables; }
    public float Range { get => _range; }
}
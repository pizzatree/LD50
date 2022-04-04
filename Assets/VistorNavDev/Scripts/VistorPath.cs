using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class VistorPath : MonoBehaviour
{
    [SerializeField] Transform _endPoint;

    protected NavMeshSurface _navMeshSurface;

    public virtual void Initialize()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public Transform EndPoint { get => _endPoint; }
    public NavMeshSurface NavMeshSurface { get => _navMeshSurface; }

    public virtual void BuildNavMesh() { }
}
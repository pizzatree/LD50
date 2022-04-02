using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParkBench : MonoBehaviour
{
    [SerializeField] Transform _visitorAnimationStartSpot;
    public bool Occupied;
    
    public Transform VisitorAnimationStartSpot { get => _visitorAnimationStartSpot; }
}
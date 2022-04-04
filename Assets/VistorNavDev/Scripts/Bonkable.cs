using System;
using UnityEngine;

public abstract class Bonkable : MonoBehaviour
{
    public static event Action<Bonkable, bool> OnSpawnEvent;

    public abstract   void OnBonk(int bonkValue, Vector3 bonkDirection);
    protected virtual void Start()     => OnSpawnEvent?.Invoke(this, true);
    protected virtual void OnDisable() => OnSpawnEvent?.Invoke(this, false);
}
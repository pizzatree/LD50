using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkBox : MonoBehaviour
{
    public event Action OnBonk;

    private void OnTriggerEnter(Collider other)
    {
        var bonkable = other.GetComponentInParent<Bonkable>();
        if(bonkable == null)
            return;
        
        bonkable.OnBonk(9001, bonkable.transform.position - transform.position);
        OnBonk?.Invoke();
    }
}

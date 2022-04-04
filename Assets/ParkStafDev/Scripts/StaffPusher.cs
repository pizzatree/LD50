using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffPusher : ParkStaff
{
    [SerializeField] float _grabCooldown = 1;
    bool _cooledDown = true;
    protected override void Start() { base.Start(); }
    
    protected override void Update()
    {
        if (_isGrabbed) { return;}
        if (_grabber.HasEntityInHands())
        {
            PerformStaffAction();
            MoveToTarget();
        }
        else
        {
            base.Update();
        }
    }
    
    protected override void PerformStaffAction() { if (_cooledDown) { StartCoroutine(GrabThrow()); } }

    IEnumerator GrabThrow()
    {
        _cooledDown = false;
        _grabber.HandleGrabThrow();
        yield return new WaitForSeconds(_grabCooldown);
        _cooledDown = true;
    }
}

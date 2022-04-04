using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffCleaner : ParkStaff
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void PerformStaffAction()
    {
        _interactor.FindInteraction();
    }
}

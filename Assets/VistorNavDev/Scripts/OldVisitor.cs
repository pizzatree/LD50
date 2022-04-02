using UnityEngine;

public class OldVisitor : Visitor
{
    protected override void InteractWithParkBench(ParkBench parkBench)
    {
        StartCoroutine(SitAtBench(parkBench));
    }
}

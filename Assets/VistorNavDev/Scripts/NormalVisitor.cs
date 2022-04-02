using UnityEngine;

public class NormalVisitor : Visitor
{
    [SerializeField] int _percentChanceToInteractWithBench;
    protected override void InteractWithParkBench(ParkBench parkBench)
    {
        int chance = Random.Range(0, 100);
        if (chance <= _percentChanceToInteractWithBench)
        {
            StartCoroutine(SitAtBench(parkBench));
        }
    }
}
using UnityEngine;

public class VisitorEntrancePath : VistorPath
{
    [SerializeField] Transform _startPoint;
    public override void Initialize()
    {
        base.Initialize();
        VistorPathManager.Instance.RegisterEntrance(_startPoint);
    }

    public override void BuildNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using _Plugins.TopherUtils;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class VistorPathManager : MonoBehaviour
{
    public static VistorPathManager Instance;

    public float VisitorSpawnFrequency = 5;

    [SerializeField] Visitor[] _vistorPrefabs;
    [SerializeField] VistorPath[] _paths1;
    [SerializeField] VistorPath[] _paths2;
    [SerializeField] VistorPath[] _paths3;
    [SerializeField] VistorPath[] _paths4;
    [SerializeField] VistorPath[] _paths5;
    [SerializeField] VistorPath[] _paths6;
    [SerializeField] float _pathSpawnFrequency;
    VistorPath[][] _paths;
    
    List<Transform> _validParkEntrances = new List<Transform>();
    List<Transform> _visitorDestinations = new List<Transform>();
    int _pathIndex = 0;

    void Awake()
    {
        if (!Instance)
        {
            _paths = new[] {_paths1, _paths2, _paths3, _paths4, _paths5, _paths6};
            Instance = this;
            AddPath();
            StartCoroutine(SpawnVisitors());
            
        }
        else { Destroy(gameObject); }
    }
    
    void OnDestroy() { if (Instance == this) { Instance = null; } }

    public void RegisterEntrance(Transform entrance) {_validParkEntrances.Add(entrance); }

    public void SpawnRandomVisitor()
    {
        int index = Random.Range(0, _validParkEntrances.Count);
        Visitor visitor = Instantiate(_vistorPrefabs.RandomElement(), _validParkEntrances[index].position, _validParkEntrances[index].rotation);
        visitor.Initialize();
        visitor.ReturnPoint = _validParkEntrances[index];
    }

    public void AddPath()
    {
        if (_pathIndex >= _paths.Length) { return;}
        for (int i = 0; i < _paths[_pathIndex].Length; i++)
        {
            _paths[_pathIndex][i].gameObject.SetActive(true);
            _visitorDestinations.Add(_paths[_pathIndex][i].EndPoint);
            _paths[_pathIndex][i].Initialize();
        }
        _pathIndex++;
        for (int j = 0; j < _pathIndex; j++)
        {
            for (int i = 0; i < _paths[_pathIndex].Length; i++)
                _paths[j][i].BuildNavMesh();
        }
    }

    public Transform GetRandomDestination()
    {
        if (_visitorDestinations.Count == 0) { throw new Exception("visitorDestinations is empty");}
        int index = Random.Range(0, _visitorDestinations.Count);
        return _visitorDestinations[index];
    }

    IEnumerator SpawnVisitors()
    {
        yield return new WaitForSeconds(VisitorSpawnFrequency);
        while (true)
        {
            SpawnRandomVisitor();
            yield return new WaitForSeconds(VisitorSpawnFrequency);
        }
    }
}
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
    [SerializeField] VistorPath[] _paths;
    [SerializeField] float _pathSpawnFrequency;
    
    List<Transform> _validParkEntrances = new List<Transform>();
    List<Transform> _visitorDestinations = new List<Transform>();
    int _pathIndex = 0;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            AddPath();
            StartCoroutine(SpawnVisitors());
            
        }
        else { Destroy(gameObject); }
    }

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
        _paths[_pathIndex].gameObject.SetActive(true);
        _visitorDestinations.Add(_paths[_pathIndex].EndPoint);
        _paths[_pathIndex].Initialize();
        _pathIndex++;
        for (int i = 0; i < _pathIndex; i++)
        {
            _paths[i].BuildNavMesh();
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
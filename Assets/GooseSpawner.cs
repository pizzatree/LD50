using System;
using System.Collections;
using System.Collections.Generic;
using _Plugins.TopherUtils;
using UnityEngine;
using Random = UnityEngine.Random;

public class GooseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _honkers,               _bonkers,               _imposters;
    [SerializeField] private float      _spawnRateHonkers = 3f, _spawnRateBonkers = 7f, _spawnRateImposters = 20f;
    [SerializeField] private Vector2    _bounds;
    
    private void Start()
    {
        for(int i = 0; i < 15; i++) 
            SpawnHonkers();

        InvokeRepeating(nameof(SpawnHonkers), _spawnRateHonkers, _spawnRateHonkers);
        InvokeRepeating(nameof(SpawnBonkers), _spawnRateBonkers, _spawnRateBonkers);
        //InvokeRepeating(nameof(SpawnImposters), _spawnRateImposters, _spawnRateImposters);
    }
    
    private void Spawn(GameObject prefab)
    {
        var nextPos = transform.position;
        nextPos.x += Random.Range(-_bounds.x, _bounds.x);
        nextPos.z += Random.Range(-_bounds.y, _bounds.y);
        
        Instantiate(prefab, nextPos, transform.rotation);
    }

    private void SpawnHonkers() => Spawn(_honkers);
    private void SpawnBonkers() => Spawn(_bonkers);
    private void SpawnImposters() => Spawn(_imposters);
}

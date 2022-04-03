using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


[RequireComponent(typeof(NavMeshAgent))]
public class VistorNavigator : MonoBehaviour
{
    [Tooltip("Max amount of destinations visitor could visit before leaving park")]
    [SerializeField] int _maxDestinations = 3;
    [SerializeField] float _moveSpeed;
    
    NavMeshAgent _agent;
    int _destinations;
    public Transform ReturnPoint;
    
    
    void Start()
    {
        // choose random number of destinations to visit before leaving park.
        _destinations = Random.Range(1, _maxDestinations);
        
        // set up agent
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
        _agent.destination = VistorPathManager.Instance.GetRandomDestination().position;
    }

    void Update()
    {
        // if agent reached destination
        if (_agent.remainingDistance < 0.5f)
        {
            // if agent left park
            if(_destinations == 0){Destroy(gameObject); return; }
            
            _destinations--;
            
            // if agent has visited all destinations then leave park
            if (_destinations == 0)
            {
                // find an exit point of the park
                _agent.destination = ReturnPoint.position; 
                return;
            }
            
            // pick new random point in park
            _agent.destination = VistorPathManager.Instance.GetRandomDestination().position;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Vistor Blocker"))
        {
            StartCoroutine(TemporarilyGoHome());
        }
    }
    
    public float MoveSpeed { set => _agent.speed = value; }
    public void ResetMoveSpeed() { _agent.speed = _moveSpeed; }

    IEnumerator TemporarilyGoHome()
    {
        _agent.destination = ReturnPoint.position;
        yield return new WaitForSeconds(2);
        _agent.destination = VistorPathManager.Instance.GetRandomDestination().position;
    }
}

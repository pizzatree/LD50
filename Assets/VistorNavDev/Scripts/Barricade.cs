using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barricade : Bonkable, ICanBePickedUp
{
    public event Action OnDestroyed;
    
    [SerializeField] int _hp = 20;
    [SerializeField] int _destroyTime = 5;
    [SerializeField] Grabbable _grabbable;

    List<Transform> children = new List<Transform>();
    Transform _grabber;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
                children.Add(transform.GetChild(i).GetChild(j));
        }
    }
    
    public override void OnBonk(int bonkValue, Vector3 dir)
    {
        _hp -= bonkValue;
        if (_hp <= 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                GetComponent<Collider>().isTrigger = true;
                Rigidbody rb = children[i].GetComponent<Rigidbody>();
                rb.constraints &= ~RigidbodyConstraints.FreezeAll;
                
                float x = Random.Range(0f, 360f);
                float y = Random.Range(0f, 360f);
                float z = Random.Range(0f, 360f);

                Vector3 velocity = new Vector3(x, y, z).normalized * bonkValue;
                rb.AddForce(velocity, ForceMode.Impulse);
            }
            StartCoroutine(DestroyAfterTime());
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }


    public ICanBePickedUp Grab(Transform grabber)
    {
        _grabber = grabber;
        return _grabbable.Grab(grabber);
    }
    public void Throw(Vector3 direction, float magnitude)
    {
        transform.position = _grabber.position + _grabber.forward;
        transform.rotation = _grabber.rotation;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barricade : MonoBehaviour, IBonkable, ICanBePickedUp
{
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

    void OnCollisionEnter(Collision other)
    {
        IBonker bonker;
        if(other.gameObject.TryGetComponent<IBonker>(out bonker))
            OnBonk(bonker);
    }
    public void OnBonk(IBonker bonker)
    {
        _hp -= bonker.GetBonkValue();
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

                Vector3 velocity = new Vector3(x, y, z).normalized * bonker.GetBonkValue();
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
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] int _hp;

    void OnCollisionEnter(Collision other)
    {
        _hp--;
        if(_hp == 0){Destroy(gameObject);}
    }
}

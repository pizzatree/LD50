using UnityEngine;

public class Bread : MonoBehaviour
{
    [SerializeField] float _attractionRange = 10;
    void OnCollisionStay(Collision other)
    {
        //TODO if goose, tell goose bread exists
    }
}
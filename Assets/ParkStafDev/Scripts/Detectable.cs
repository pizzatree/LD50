using UnityEngine;

public enum DetectableType{Goose, Poop}
public class Detectable : MonoBehaviour
{
    [SerializeField] DetectableType _type;
    public DetectableType GetType()
    {
        return _type;
    }
}

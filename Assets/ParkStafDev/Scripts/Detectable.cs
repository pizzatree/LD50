using UnityEngine;

public enum DetectableType{Goose, Poop}
public class Detectable : MonoBehaviour
{
    [SerializeField] DetectableType _type;
    public new DetectableType GetType()
    {
        return _type;
    }
}

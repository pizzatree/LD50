using UnityEngine;



public interface IDetectable
{
    DetectableType GetType();
    Transform GetTransform();
}

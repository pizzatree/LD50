using UnityEngine;

namespace Common.Scripts
{
    public interface ICanBePickedUp
    {
        ICanBePickedUp Grab(Transform grabber);

        void Throw(Vector3 direction, float magnitude);
    }
}
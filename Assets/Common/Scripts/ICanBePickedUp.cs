using System;
using UnityEngine;

namespace Common.Scripts
{
    public interface ICanBePickedUp
    {
        event Action   OnDestroyed;
        ICanBePickedUp Grab(Transform grabber);

        void Throw(Vector3 direction, float magnitude);
    }
}
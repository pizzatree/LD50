using System;
using UnityEngine;

namespace Common.Scripts
{
    public interface IEntityController
    {
        event Action OnSprint;
        event Action OnAction1;
        event Action OnAction2;
        Vector2      GetDirection();
    }
}
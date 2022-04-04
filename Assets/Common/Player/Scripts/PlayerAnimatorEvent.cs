using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimatorEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFootstep;

    private void Footstep() => _onFootstep?.Invoke();
}

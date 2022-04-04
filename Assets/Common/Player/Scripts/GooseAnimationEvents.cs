using System.Collections;
using System.Collections.Generic;
using _Plugins.TopherUtils.Audio;
using UnityEngine;

public class GooseAnimationEvents : MonoBehaviour
{
    [SerializeField] private AudioEvent    _step1, _step2;
    [SerializeField] private AudioReaction _audioReaction;

    public void Step1() => _audioReaction.PlayEventOneShot(_step1);
    public void Step2() => _audioReaction.PlayEventOneShot(_step2);
}
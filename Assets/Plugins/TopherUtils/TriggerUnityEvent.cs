using UnityEngine;
using UnityEngine.Events;

namespace _Plugins.TopherUtils
{
    public class TriggerUnityEvent : MonoBehaviour
    {
        [Tooltip("Event is called when triggered via code.")]
        [SerializeField] private UnityEvent _onTrigger;
        
        [Tooltip("Event is called when this object is enabled.")]
        [SerializeField] private UnityEvent _onEnable;
        
        [Tooltip("Event is called when this object is disabled.")]
        [SerializeField] private UnityEvent _onDisable;

        public  void Trigger()   => _onTrigger?.Invoke();
        private void OnEnable()  => _onEnable?.Invoke();
        private void OnDisable() => _onDisable?.Invoke();
    }
}
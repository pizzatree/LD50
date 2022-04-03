using UnityEngine;

namespace Common.Scripts
{
    public class Poop : MonoBehaviour, IInteractable
    {
        public IInteractable Interact()
        {
            return this;
        }
    }
}
using UnityEngine;

namespace Common.Scripts
{
    public class Shed : MonoBehaviour, IInteractable
    {
        public IInteractable Interact()
        {
            return this;
        }
    }
}
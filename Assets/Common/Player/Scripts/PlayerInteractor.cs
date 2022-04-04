using System.Threading;
using System.Threading.Tasks;
using Common.Poo;
using Common.Scripts;
using UnityEngine;

namespace Common.Player.Scripts
{
    public class PlayerInteractor : MonoBehaviour
    {
        private static readonly int Clean = Animator.StringToHash("Clean");

        private Animator          _animator;
        private Collider          _collider;
        private CancellationToken _ct;

        public void Init(CancellationToken ct, Animator animator)
        {
            _ct       = ct;
            _animator = animator;
            _collider = GetComponent<Collider>();
        }

        public async void FindInteraction()
        {
            _collider.enabled = true;
            await Task.Delay(250);

            if(_ct.IsCancellationRequested || !_collider.enabled)
                return;
        
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.gameObject.GetComponentInParent<IInteractable>();
            if(interactable == null)
                return;

            _collider.enabled = false;

            if(interactable is Poop)
            {
                _animator.SetTrigger(Clean);
                Destroy(other.gameObject);
            }     
            else if(interactable is Shed)
            {
                PurchaseWindow.OpenWindow();
            }
        }
    }
}
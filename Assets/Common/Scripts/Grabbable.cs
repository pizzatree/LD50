using _Plugins.TopherUtils;
using UnityEngine;

namespace Common.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : MonoBehaviour, ICanBePickedUp
    {
        private Rigidbody  _rb;
        private Collider[] _colliders;

        private void Start()
        {
            _rb        = GetComponent<Rigidbody>();
            _colliders = GetComponentsInChildren<Collider>();
        }

        public ICanBePickedUp Grab(Transform grabber)
        {
            transform.parent = grabber;
            _rb.isKinematic  = true;
            _colliders.SetAllActive(false);

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            return this;
        }

        public void Throw(Vector3 direction, float magnitude)
        {
            transform.parent = null;
            _colliders.SetAllActive(true);
        
            _rb.isKinematic = false;
            _rb.AddForce((Vector3.up + direction) * magnitude, ForceMode.Impulse);
        }
    }
}
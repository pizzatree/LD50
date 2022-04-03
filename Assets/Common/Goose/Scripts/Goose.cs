using Common.Scripts;
using UnityEngine;

namespace Common.Goose.Scripts
{
    [RequireComponent(typeof(Grabbable))]
    public class  Goose : MonoBehaviour
    {
        [Header("Customization")]
        [SerializeField] protected GameObject _bat;
        [SerializeField] protected bool      _usesBat;
        [SerializeField] private   Transform _pooPoint;

        [Header("Dependencies")] 
        [SerializeField] private GameObject _pooPrefab;
        
        private Grabbable       _grabbable;
        private GooseCollisions _gooseCollisions;
        
        private void Start()
        {
            _grabbable       = GetComponent<Grabbable>();
            _gooseCollisions = gameObject.AddComponent<GooseCollisions>();

            _grabbable.OnThrown += _gooseCollisions.ClapThemGeese;
            
            InvokeRepeating(nameof(Poo), 1f, 5f);
        }

        private void Poo()
        {
            Instantiate(_pooPrefab, _pooPoint.position, transform.rotation);
        }

        private void OnDisable()
        {
            _grabbable.OnThrown -= _gooseCollisions.ClapThemGeese;
        }

        private void OnValidate()
        {
            _bat.SetActive(_usesBat);
        }
    }
}
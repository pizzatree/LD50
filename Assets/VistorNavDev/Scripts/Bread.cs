using System;
using DG.Tweening;
using UnityEngine;

namespace VistorNavDev.Scripts
{
    public class Bread : MonoBehaviour
    {
        [SerializeField] private int _startingHealth = 15;
        [SerializeField] private int _health;

        private Transform _model;
    
        private void Start()
        {
            _model  = transform.Find("Model");
            _health = _startingHealth;
        }
    
        private void OnDisable()
        {
            DOTween.Kill(_model);
        }
    
        public void GetEaten(int numBites = 1)
        {
            _health -= numBites;

            var size = (float)_health  / _startingHealth;
            _model.DOScale(Vector3.one * size, 0.5f);
        
            if(_health <= 0)
                Destroy(gameObject);
        }
    }
}
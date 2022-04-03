using System;
using System.Threading;
using System.Threading.Tasks;
using _Plugins.TopherUtils;
using Common.Scripts;
using DG.Tweening;
using UnityEngine;

namespace Common.Poo
{
    public class Poop : MonoBehaviour, IInteractable
    {
        private Transform _model;
        private bool      _landed;

        private void Start()
        {
            _model            = transform.Find("Model");
            _model.localScale = Vector3.one * 0.2f;

            _model.DOScale(Vector3.one, 0.5f);

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(_landed)
                return;

            _landed = true;
            DOTween.Kill(_model);
            _model.localScale = Vector3.one;
            _model.DOPunchScale(Vector3.one, 0.5f, elasticity:0.75f);
        }

        public IInteractable Interact()
        {
            return this;
        }
    }
}
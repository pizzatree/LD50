using System;
using Common.Goose.Scripts;
using UnityEngine;

namespace VistorNavDev.Scripts
{
    public class BreadBox : MonoBehaviour
    {
        private Bread _bread;
    
        private void Start() => _bread = GetComponentInParent<Bread>();

        private void OnTriggerEnter(Collider other)
        {
            var goose = other.GetComponentInParent<Goose>();
            if(!goose)
                return;
        
            goose.EatBread(_bread);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Goose.Scripts
{
    public class GooseCollisions : MonoBehaviour
    {
        private GameObject[] _myObjects;

        private void Start()
        {
            var objs = new List<GameObject> { gameObject };
            objs.AddRange(from Transform t in transform select t.gameObject);

            _myObjects = objs.ToArray();
            ClipThemGeese();
        }
        
        public void ClapThemGeese()
        {
            foreach(var go in _myObjects) 
                go.layer = LayerMask.NameToLayer("GooseClapping");
            
            Invoke(nameof(ClipThemGeese), 5f);
        }
    
        public void ClipThemGeese()
        {
            foreach(var go in _myObjects) 
                go.layer = LayerMask.NameToLayer("GooseClipping");
        }
    }
}
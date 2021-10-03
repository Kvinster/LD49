using System.Collections.Generic;
using UnityEngine;

namespace LD49.Behaviour
{
    public class CarryRigidBodies : MonoBehaviour
    {
        private List<Rigidbody2D> _rigidBodies = new List<Rigidbody2D>();
        private  Vector3 _lastPosition;
        private Transform _transform;
        
        private void Start()
        {
            _transform = transform;
            _lastPosition = _transform.position;
        }

        private void LateUpdate()
        {
            if (_rigidBodies.Count == 0)
            {
                return;
            }
            
            for (var i = 0; i < _rigidBodies.Count; i++)
            {
                var rb = _rigidBodies[i];
                var velocity = _transform.position - _lastPosition;
                rb.transform.Translate(velocity, _transform);
            }

            _lastPosition = _transform.position;
        }

        public void Add(Rigidbody2D rb)
        {
            if (!_rigidBodies.Contains(rb))
            {
                _rigidBodies.Add(rb);
            }
        }

        public void Remove(Rigidbody2D rb)
        {
            if (_rigidBodies.Contains(rb))
            {
                _rigidBodies.Remove(rb);
            }
        }
        
    }
}

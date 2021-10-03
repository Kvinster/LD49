using System;
using UnityEngine;

namespace LD49.Behaviour.Platform
{
    public class CarryRigidBodiesSensor : MonoBehaviour
    {
        private CarryRigidBodies _carryRigidBodies;

        private void Awake()
        {
            _carryRigidBodies = GetComponentInParent<CarryRigidBodies>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            var bomb = other.gameObject.GetComponent<Bomb>();
            
            if (bomb != null && !bomb.IsActive)
            {
                return;
            }
            
            if (rb != null)
            {
                _carryRigidBodies.Add(rb);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && _carryRigidBodies != null)
            {
                _carryRigidBodies.Remove(rb);
            }
        }
    }
}

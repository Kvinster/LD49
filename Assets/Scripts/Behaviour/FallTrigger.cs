using System;
using UnityEngine;

namespace LD49.Behaviour
{
    public class FallTrigger : MonoBehaviour
    {
        public event Action LevelFailed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Bomb"))
            {
                LevelFailed?.Invoke();
            } 
        }
    }
}

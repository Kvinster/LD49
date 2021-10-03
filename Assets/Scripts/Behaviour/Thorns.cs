using LD49.Manager;
using UnityEngine;

namespace LD49.Behaviour
{
    public class Thorns : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player") || other.CompareTag("Bomb"))
            {
               LevelManager.Instance.FailLevel();
            }
        }
    }
}

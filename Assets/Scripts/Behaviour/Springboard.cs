using UnityEngine;

namespace LD49.Behaviour
{
    public class Springboard : MonoBehaviour
    {
        [SerializeField]
        private float _springForce = 1200.0f;
        
        private Animator _animator;
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var bomb = other.gameObject.GetComponent<Bomb>();
            
            if (bomb != null && !bomb.IsActive)
            {
                return;
            }
            
            if(other.CompareTag("Player") || other.CompareTag("Bomb"))
            {
                Tossing(other.gameObject);
            }
        }
        
        private void Tossing(GameObject gameObject)
        {
            _animator.SetTrigger("Tossing");
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _springForce);
        }
    }
}

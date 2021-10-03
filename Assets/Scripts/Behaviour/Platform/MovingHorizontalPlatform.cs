using UnityEngine;

namespace LD49.Behaviour
{
    public class MovingHorizontalPlatform : MonoBehaviour
    {
        [SerializeField]
        private float _distance; 
        [SerializeField]
        private float _speed = 2f;

        private bool _backwardMovement;
        private float _startPositionX;
        
        private void Awake()
        {
            _startPositionX = transform.position.x;
        }
        
        private void Update()
        {
            if (transform.position.x > _startPositionX + _distance)
            {
                _backwardMovement = false;
            }
            else if (transform.position.x < _startPositionX -_distance)
            {
                _backwardMovement = true;
            }

            if (_backwardMovement)
            {
                var position = transform.position;
                position = new Vector2(position.x + _speed * Time.deltaTime, position.y);
                transform.position = position;
            }
            else
            {
                var position = transform.position;
                position = new Vector2(position.x - _speed * Time.deltaTime, position.y);
                transform.position = position;
            }
        }
        
    }
}

using System;
using UnityEngine;

namespace LD49.Behaviour
{
    public class MovingVerticalPlatform : MonoBehaviour
    {
        [SerializeField]
        private float _distance; 
        [SerializeField]
        private float _speed = 2f;

        private bool _backwardMovement;
        private float _startPositionY;

        private void Awake()
        {
            _startPositionY = transform.position.y;
        }

        private void Update()
        {
            if (transform.position.y > _startPositionY + _distance)
            {
                _backwardMovement = false;
            }
            
            else if (transform.position.y < _startPositionY - _distance)
            {
                _backwardMovement = true;
            }

            if (_backwardMovement)
            {
                var position = transform.position;
                position = new Vector2(position.x, position.y + _speed * Time.deltaTime);
                transform.position = position;
            }
            else
            {
                var position = transform.position;
                position = new Vector2(position.x, position.y - _speed * Time.deltaTime);
                transform.position = position;
            }
        }
        
    }
}

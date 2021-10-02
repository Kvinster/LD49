﻿using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField]
        private float _startExplosionTime;
        [SerializeField]
        private Text _timerText;
        [SerializeField]
        private SpriteRenderer _renderer;

        private float _explosionTime;
        private bool _isActive;

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            _explosionTime -= Time.deltaTime;
            _timerText.text = _explosionTime.ToString("F2");
            
             var progress = _explosionTime / _startExplosionTime;
             _renderer.color = new Color(1, progress, progress);
        }

        public void ActiveBomb()
        {
            _timerText.gameObject.SetActive(true);
            _explosionTime = _startExplosionTime;
            _isActive = true;
        }
        
        public void BombDeactivated()
        {
            _isActive = false;
            _renderer.color = new Color(1, 1, 1);
            
            _timerText.gameObject.SetActive(false);
        }
    }
}

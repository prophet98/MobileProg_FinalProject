using System;
using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class HealthComponent : MonoBehaviour, IDamageable<int>
    {
        public float maxHp;
        private float _currentHp;
        [HideInInspector] public bool isInvincible;
        public event Action<float> OnHealthPctChange;
        public event Action OnEntityDeath;
        public  static event Action OnPlayerDeath;
        private void Start()
        {
            _currentHp = maxHp;
        }

        public void RemoveHealth(int damageValue)
        {
            if (isInvincible)
            {
                return;
            }
            _currentHp -= damageValue;
            if (_currentHp>maxHp)
            {
                _currentHp = maxHp;
            }
            var currentHealthPct = _currentHp / maxHp;
            OnHealthPctChange?.Invoke(currentHealthPct);
            
            switch (_currentHp<=0)
            {
                //is IA?
                case true when this.GetComponent<AiController>():
                    OnEntityDeath?.Invoke();
                    Destroy(gameObject);
                    break;
                //is Player?
                case true:
                    gameObject.SetActive(false);
                    OnPlayerDeath?.Invoke();
                    break;
            }
        }
        
    }
}

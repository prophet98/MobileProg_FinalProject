using System;
using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class HealthComponent : MonoBehaviour, IDamageable<int>
    {
        public int maxHp;
        private int _currentHp;
        [HideInInspector] public bool isInvincible;
        public event Action<float> OnHealthPctChange;
        public event Action OnEntityDeath;
        private void Awake()
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
            var currentHealthPct = (float)_currentHp / maxHp;
            OnHealthPctChange?.Invoke(currentHealthPct);
            
            if (_currentHp<=0 && this.GetComponent<AiController>()) //is IA?
            {
                PlayerWeaponComponent.TriggerList.Remove(this.GetComponentInChildren<Collider>());
                OnEntityDeath?.Invoke();
                Destroy(gameObject);
            }
            else if (_currentHp<=0 ) //is Player?
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}

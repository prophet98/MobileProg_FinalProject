using System;
using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class HealthComponent : MonoBehaviour, IDamageable<int>
    {
        private HealthBar _healthBar;
        public int maxHp;
        private int _currentHp;

        public event Action<float> OnHealthPctChange;
        private void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            _currentHp = maxHp;
        }

        public void RemoveHealth(int damageValue)
        {
            _currentHp -= damageValue;
            float currentHealthPct = (float)_currentHp / (float)maxHp;
            OnHealthPctChange?.Invoke(currentHealthPct);

            if (_currentHp<=0 && this.GetComponent<AiController>())
            {
                PlayerWeaponComponent.TriggerList.Remove(this.GetComponentInChildren<Collider>());
                gameObject.SetActive(false);
            
            }
            else if (_currentHp<=0 )
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}

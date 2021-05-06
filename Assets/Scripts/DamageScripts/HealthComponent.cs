using System;
using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class HealthComponent : MonoBehaviour, IDamageable<int>
    {
        public int maxHp;
        private int _currentHp;

        public event Action<float> OnHealthPctChange;
        private void Awake()
        {
            _currentHp = maxHp;
        }

        public void RemoveHealth(int damageValue)
        {
            _currentHp -= damageValue;
            var currentHealthPct = (float)_currentHp / maxHp;
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

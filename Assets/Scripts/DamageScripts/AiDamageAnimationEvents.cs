using System;
using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class AiDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction(int damage);
        public static event DamageAction OnPlayerDamagedSuccess;
        public static event DamageAction OnShoot;

        private int _attackDamage;

        private void Awake()
        {
            _attackDamage = GetComponentInParent<AiController>().agentStats.attackDamage;
        }

        public void AiMeleeAttack()
        {
            OnPlayerDamagedSuccess?.Invoke(_attackDamage);
        }
        public void AiRangedAttack()
        { 
            OnShoot?.Invoke(_attackDamage);
        }
    }
}

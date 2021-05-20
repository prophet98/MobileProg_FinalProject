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
        public Sound.Names enemyAttackSound;
        private void Awake()
        {
            _attackDamage = GetComponentInParent<AiController>().agentStats.attackDamage;
        }

        public void AiMeleeAttack()
        {
            OnPlayerDamagedSuccess?.Invoke(_attackDamage);
            if (SoundManager.instance != null)
            {
                SoundManager.instance.Play(enemyAttackSound);
            }
        }
        public void AiRangedAttack()
        { 
            OnShoot?.Invoke(_attackDamage);
            if (SoundManager.instance != null)
            {
                SoundManager.instance.Play(enemyAttackSound);
            }
        }
    }
}

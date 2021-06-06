using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class AiDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction(int damage);

        public static event DamageAction OnPlayerDamagedSuccess;
        public event DamageAction OnShoot;

        private int _attackDamage;
        public Sound.Names enemyAttackSound;

        private void Awake()
        {
            _attackDamage = GetComponentInParent<AiController>().agentStats.attackDamage;
        }

        public void AiMeleeAttack() //called inside the animator asset of AI
        {
            OnPlayerDamagedSuccess?.Invoke(_attackDamage);
            if (SoundManager.instance != null)
            {
                SoundManager.instance.Play(enemyAttackSound);
            }
        }

        public void AiRangedAttack() //called inside the animator asset of AI
        {
            OnShoot?.Invoke(_attackDamage);
            if (SoundManager.instance != null)
            {
                SoundManager.instance.Play(enemyAttackSound);
            }
        }
    }
}
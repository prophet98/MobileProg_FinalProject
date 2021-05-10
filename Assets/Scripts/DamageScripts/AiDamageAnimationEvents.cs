using UnityEngine;

namespace DamageScripts
{
    public class AiDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction(int damage);
        public static event DamageAction OnPlayerDamagedSuccess;
        public static event DamageAction OnShoot;

        public int attackDamage;

        public void AiMeleeAttack()
        {
            OnPlayerDamagedSuccess?.Invoke(attackDamage);
        }
        public void AiRangedAttack()
        { 
            OnShoot?.Invoke(attackDamage);
        }
    }
}

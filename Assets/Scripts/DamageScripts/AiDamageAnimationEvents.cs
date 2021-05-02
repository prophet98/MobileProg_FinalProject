using UnityEngine;

namespace DamageScripts
{
    public class AiDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction(int damage);
        public static event DamageAction OnPlayerDamagedSuccess;

        public void AiAttack(int damage)
        {
            OnPlayerDamagedSuccess?.Invoke(damage);
        }
    }
}

using UnityEngine;

namespace DamageScripts
{
    public class PlayerDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction();
        public event DamageAction OnDamagedSuccess;
        public event DamageAction OnDamagedFail;
        public Sound.Names[] attackSound;

        private void SwordAttack(int counter)
        {
            if (ActionsEventsHandler.comboCounter == counter)
            {
                OnDamagedSuccess?.Invoke();
                if (SoundManager.instance != null)
                {
                    SoundManager.instance.Play(attackSound[Random.Range(0,attackSound.Length)]);
                }
            }
            else
            {
                OnDamagedFail?.Invoke();
            }
        }

        private void ResetCounter()
        {
            ActionsEventsHandler.comboCounter = 0;
        }
        
    }
}

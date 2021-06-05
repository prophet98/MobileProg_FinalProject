using UnityEngine;

namespace DamageScripts
{
    public class PlayerDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction();
        public event DamageAction OnDamagedSuccess;
        public event DamageAction OnDamagedFail;
        public Sound.Names[] attackSound;

        private void SwordAttack(int counter) //event called by the ai animations attached to them 
        {
            if (ActionsEventsHandler.comboCounter == counter) //check if player combo counter is following the current counter to apply damage, else you fail to attack.
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

        private void ResetCounter() //event called by the ai animations attached to them
        {
            ActionsEventsHandler.comboCounter = 0;
        }
        
    }
}

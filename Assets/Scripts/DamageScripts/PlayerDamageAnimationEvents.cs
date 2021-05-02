using UnityEngine;

namespace DamageScripts
{
    public class PlayerDamageAnimationEvents : MonoBehaviour
    {
        public delegate void DamageAction();
        public static event DamageAction OnDamagedSuccess;
        public static event DamageAction OnDamagedFail;
    
        public void FirstAttack()
        {
            if (ActionsEventsHandler.comboCounter == 1)
            {
                OnDamagedSuccess?.Invoke();
            }
            else
            {
                OnDamagedFail?.Invoke();
            }
        }
        public void SecondAttack()
        {
            if (ActionsEventsHandler.comboCounter == 2)
            {
                OnDamagedSuccess?.Invoke();
            }
            else
            {
                OnDamagedFail?.Invoke();
            }
        }
        public void ThirdAttack()
        {
            if (ActionsEventsHandler.comboCounter == 3)
            {
                OnDamagedSuccess?.Invoke();
            }
            else
            {
                OnDamagedFail?.Invoke();
            }
        }

        void ResetCounter()
        {
            ActionsEventsHandler.comboCounter = 0;
        }
    }
}

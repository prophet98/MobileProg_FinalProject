using AiScripts;
using UnityEngine;

namespace DamageScripts
{
    public class DamageComponent : MonoBehaviour, IDamageable<int>
    {
        public int hpValue;
        public void RemoveHealth(int damageValue)
        {
            hpValue -= damageValue;
            if (hpValue<=0 && this.GetComponent<AiController>())
            {
                PlayerWeaponComponent.TriggerList.Remove(this.GetComponentInChildren<Collider>());
                gameObject.SetActive(false);
            
            }
            else if (hpValue<=0 )
            {
                gameObject.SetActive(false);
            }
        }
    }
}

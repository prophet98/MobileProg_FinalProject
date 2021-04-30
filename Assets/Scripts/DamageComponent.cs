using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    public int hpValue;
    public void RemoveHealth(int damageValue)
    {
        hpValue -= damageValue;
        if (hpValue<=0)
        {
            PlayerWeaponComponent.TriggerList.Remove(this.GetComponentInChildren<Collider>());
            gameObject.SetActive(false);
            
        }
    }
}

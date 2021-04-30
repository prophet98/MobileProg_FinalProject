using System.Collections.Generic;
using UnityEngine;


public class PlayerWeaponComponent : MonoBehaviour
{
    // [SerializeField] private int enemiesInRange;
    public static readonly List<Collider> TriggerList = new List<Collider>();
    public int weaponDamage;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.CompareTag("Enemy"))
        {
            if (!TriggerList.Contains(other))
            {
                TriggerList.Add(other);
            }
        }
        // enemiesInRange = triggerList.Count;
    }
    private void OnTriggerExit(Collider other)
    {
        // enemiesInRange = 0;
        if (TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }
    }
}

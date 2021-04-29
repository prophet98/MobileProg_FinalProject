using System.Collections.Generic;
using UnityEngine;


public class WeaponComponent : MonoBehaviour
{
    public bool isEnemyInRange;
    [SerializeField] private int enemiesInRange;
    public readonly List<Collider> triggerList = new List<Collider>();
    public int weaponDamage;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.CompareTag("Enemy"))
        {
            isEnemyInRange = true;
            if (!triggerList.Contains(other))
            {
                triggerList.Add(other);
            }
        }
        enemiesInRange = triggerList.Count;
    }
    private void OnTriggerExit(Collider other)
    {
        isEnemyInRange = false;
        enemiesInRange = 0;
        if (triggerList.Contains(other))
        {
            triggerList.Remove(other);
        }
    }
}


using System;
using System.Collections.Generic;
using UnityEngine;


public class WeaponRange : MonoBehaviour
{
    public bool isEnemyInRange;
    [SerializeField] private int enemiesInRange = 0;
    private readonly List<Collider> _triggerList = new List<Collider>();
    

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.CompareTag("Enemy"))
        {
            isEnemyInRange = true;
        }

        if (!_triggerList.Contains(other))
        {
            _triggerList.Add(other);
        }

        enemiesInRange = _triggerList.Count;
    }
    private void OnTriggerExit(Collider other)
    {
        isEnemyInRange = false;
        enemiesInRange = 0;
        if (_triggerList.Contains(other))
        {
            _triggerList.Remove(other);
        }
    }
}

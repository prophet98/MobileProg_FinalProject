using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponComponent : MonoBehaviour
{
    // [SerializeField] private int enemiesInRange;
    public static readonly List<Collider> TriggerList = new List<Collider>();
    public int weaponDamage;
    public int killCounter;
    public GameObject weaponParticle;
    [HideInInspector] public GameObject weaponParticleInstance;
    private void Awake()
    {
        Door.OnEnvChange += ResetKillCounter;
        weaponParticleInstance = Instantiate(weaponParticle.gameObject, transform.position,
            transform.rotation);
        weaponParticleInstance.gameObject.transform.SetParent(transform);
    }

    private void ResetKillCounter()
    {
        killCounter = 0;
        GetComponentInParent<BattleRewardSystem>().canPassGate = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.CompareTag("Enemy"))
        {
            if (!TriggerList.Contains(other))
            {
                TriggerList.Add(other);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= ResetKillCounter;
    }
}

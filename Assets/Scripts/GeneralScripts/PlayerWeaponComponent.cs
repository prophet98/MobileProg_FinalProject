using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponComponent : MonoBehaviour
{
    public readonly List<Collider> triggerList = new List<Collider>();
    public int weaponDamage;
    public int killCounter;
    public GameObject weaponParticle;
    [HideInInspector] public GameObject weaponParticleInstance;

    private void Awake() //instantiate once the particle for the player to use and attach it to him.
    {
        Door.OnEnvChange += ResetKillCounter;
        weaponParticleInstance = Instantiate(weaponParticle.gameObject, transform.position, transform.rotation);
        weaponParticleInstance.gameObject.transform.SetParent(transform);
    }

    private void ResetKillCounter() //resets the kills made by player.
    {
        killCounter = 0;
        GetComponentInParent<BattleRewardSystem>().canPassGate = false;
    }

    private void OnTriggerStay(Collider other) //adds to list a unique Enemy tagged GO and removes it on exit. 
    {
        if (other.transform.parent.CompareTag("Enemy"))
        {
            if (!triggerList.Contains(other))
            {
                triggerList.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerList.Contains(other))
        {
            triggerList.Remove(other);
        }
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= ResetKillCounter;
    }
}
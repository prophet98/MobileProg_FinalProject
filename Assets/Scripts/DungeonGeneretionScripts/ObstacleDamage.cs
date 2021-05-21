using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageScripts;

public class ObstacleDamage : MonoBehaviour
{
    private GameObject playerGO;
    private const string PLAYER_TAG = "Player";
    
    [SerializeField]
    private int damage = 3;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }

    private void Damage()
    {
        playerGO.GetComponent<HealthComponent>().RemoveHealth(damage);
        Debug.Log("obs damage");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == PLAYER_TAG)
        {
            InvokeRepeating(nameof(Damage), 0f, 0.5f); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PLAYER_TAG)
        {
            CancelInvoke();
        }
    }
}

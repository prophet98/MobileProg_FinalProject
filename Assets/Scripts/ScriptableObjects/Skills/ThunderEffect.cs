using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageScripts;

public class ThunderEffect : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private List<GameObject> enemies;
    [SerializeField]
    private int damage = 3;

    private void Damage(GameObject enemy)
    {
        enemy.GetComponentInParent<HealthComponent>().RemoveHealth(damage);
    }
    private void OnTriggerStay(Collider other) //TODO: change
    {
       if (other.transform.parent.CompareTag(ENEMY_TAG))
          Damage(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag(ENEMY_TAG))
            enemies.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.CompareTag(ENEMY_TAG))
            enemies.Remove(other.gameObject);
    }
}

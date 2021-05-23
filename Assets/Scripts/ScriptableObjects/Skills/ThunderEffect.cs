using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageScripts;

public class ThunderEffect : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private List<GameObject> enemiesList;
    [SerializeField]
    private int damage = 15;

    private void Start()
    {
        enemiesList = new List<GameObject>();
    }

    private void Damage(GameObject enemy)
    {
        enemy.GetComponentInParent<HealthComponent>().RemoveHealth(damage);
    }

    private void EnemiesParse()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (enemiesList[i] != null) 
            { 
                Damage(enemiesList[i]);
            }
            else
            {
                enemiesList.Remove(enemiesList[i]);
            }
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.parent)&&(other.transform.parent.CompareTag(ENEMY_TAG))) 
        {
            if (!enemiesList.Contains(other.gameObject))
            {
                enemiesList.Add(other.gameObject);
                InvokeRepeating(nameof(EnemiesParse), 0f, 0.5f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.CompareTag(ENEMY_TAG))
        {
            if (enemiesList.Contains(other.gameObject))
            {
                enemiesList.Remove(other.gameObject);
            }
        }
    }
}


using System;
using System.Collections;
using DamageScripts;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [HideInInspector] public int bulletDamage;
    private const float LifeTime = 4;

    private void OnEnable()
    {
        StartCoroutine(SelfDestruct());
    }
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthComponent>().RemoveHealth(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}

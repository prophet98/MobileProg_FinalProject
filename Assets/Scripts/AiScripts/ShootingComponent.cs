using System;
using DamageScripts;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunSocket;
    private AiDamageAnimationEvents _aiDamageAnimationEvents;

    private void Awake()
    {
        _aiDamageAnimationEvents = GetComponentInChildren<AiDamageAnimationEvents>();
    }

    private void OnEnable()
    {
        _aiDamageAnimationEvents.OnShoot += ShootProjectile;
    }

    private void ShootProjectile(int damage)
    {
        var bulletObj = Instantiate(bullet, gunSocket.position, gunSocket.rotation);
        bulletObj.GetComponent<BulletBehaviour>().bulletDamage = damage;
    }

    private void OnDisable()
    {
        _aiDamageAnimationEvents.OnShoot -= ShootProjectile;
    }
}
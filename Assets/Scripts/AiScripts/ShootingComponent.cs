
using DamageScripts;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunSocket;
    private void OnEnable()
    {
        AiDamageAnimationEvents.OnShoot += ShootProjectile;
    }

    private void ShootProjectile(int damage)
    {
        var bulletObj = Instantiate(bullet, gunSocket.position, gunSocket.rotation);
        bulletObj.GetComponent<BulletBehaviour>().bulletDamage = damage;
    }
    
    private void OnDisable()
    {
        AiDamageAnimationEvents.OnShoot -= ShootProjectile;
    }
}

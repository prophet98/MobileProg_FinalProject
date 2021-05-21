using DamageScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSkill", menuName = "ScriptableObjects/CreateShieldSkill")]
public class ShieldSkill : BaseSkill
{
    public GameObject shieldParticle;
    private GameObject _particleInstance;

    public override void Activate(GameObject skillUser)
    {
        _particleInstance = Instantiate(shieldParticle.gameObject, skillUser.transform.position,
            skillUser.transform.rotation);
        _particleInstance.gameObject.transform.SetParent(skillUser.transform);
        _particleInstance.SetActive(true);
        skillUser.GetComponent<HealthComponent>().isInvincible = true;
    }

    public override void StartCooldown(GameObject skillUser)
    {
        Destroy(_particleInstance);
        skillUser.GetComponent<HealthComponent>().isInvincible = false;
    }
}

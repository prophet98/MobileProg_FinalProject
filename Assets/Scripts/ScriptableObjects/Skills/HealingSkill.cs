
using DamageScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "ScriptableObjects/CreateHealingSkill")]
public class HealingSkill : BaseSkill
{
    public GameObject healingParticle;
    public int healingValue;
    private GameObject _particleInstance;
    
    public override void Activate(GameObject skillUser)
    {
        _particleInstance = Instantiate(healingParticle.gameObject, skillUser.transform.position,
            skillUser.transform.rotation);
        _particleInstance.gameObject.transform.SetParent(skillUser.transform);
        _particleInstance.transform.localScale = new Vector3(4, 4, 4);
        _particleInstance.SetActive(true);
        skillUser.GetComponent<HealthComponent>().RemoveHealth(-healingValue);
    }

    public override void StartCooldown(GameObject skillUser)
    {
        Destroy(_particleInstance);
        skillUser.GetComponent<HealthComponent>().isInvincible = false;
    }
    
}

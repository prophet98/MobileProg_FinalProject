
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderSkill", menuName = "ScriptableObjects/CreateThunderSkill")]
public class ThunderSkill : BaseSkill
{
    public GameObject thunderParticle;
    private GameObject _particleInstance;

    public override void Activate(GameObject skillUser)
    {
        _particleInstance = Instantiate(thunderParticle.gameObject, skillUser.transform.position,
            skillUser.transform.rotation);
        _particleInstance.gameObject.transform.SetParent(skillUser.transform);
        _particleInstance.SetActive(true);
    }

    public override void StartCooldown(GameObject skillUser)
    {
        base.StartCooldown(skillUser);
        Destroy(_particleInstance);
    }
}

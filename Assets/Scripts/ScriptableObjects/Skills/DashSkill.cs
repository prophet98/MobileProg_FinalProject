
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill", menuName = "ScriptableObjects/CreateDashSkill")]
public class DashSkill : BaseSkill
{
    public float dashSpeed;
    private CharacterController _characterController;
    public override void Activate(GameObject skillUser)
    {
        _characterController = skillUser.GetComponent<CharacterController>();
        _characterController.Move(skillUser.transform.forward.normalized * dashSpeed);
    }

    public override void StartCooldown(GameObject skillUser)
    {
        
    }

}

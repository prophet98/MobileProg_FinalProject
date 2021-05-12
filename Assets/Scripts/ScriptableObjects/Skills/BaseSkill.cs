
using UnityEngine;
public class BaseSkill : ScriptableObject
{
    public string skillName;
    public float cooldownTime;
    public float activeTime;
    public enum AbilityState
    {
        Ready,
        Activated,
        Cooldown
    }

    public AbilityState state = AbilityState.Ready;
    public virtual void Activate(GameObject skillUser){}
    public virtual void StartCooldown(GameObject skillUser){}
}

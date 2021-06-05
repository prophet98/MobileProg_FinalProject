using System;
using UnityEngine;

public class SkillSlotsController : MonoBehaviour
{
    public BaseSkill lowerSlotSkill;
    public BaseSkill upperSlotSkill;

    private float _lowerActiveTime, _lowerCoolDownTime, _upperActiveTime, _upperCooldownTime;
    private bool _isLowerSlotSkillNotNull;
    private bool _isUpperSlotSkillNotNull;

    private void Start()
    {
        _isUpperSlotSkillNotNull = upperSlotSkill != null;
        _isLowerSlotSkillNotNull = lowerSlotSkill != null;
    }

    public void SetUpSkillTimers() //if skill slot are not null, set up their timers accordingly
    {
        if (lowerSlotSkill != null)
        {
            _lowerActiveTime = lowerSlotSkill.activeTime;
            _lowerCoolDownTime = lowerSlotSkill.cooldownTime;
        }

        if (upperSlotSkill != null)
        {
            _upperActiveTime = upperSlotSkill.activeTime;
            _upperCooldownTime = upperSlotSkill.cooldownTime;
        }
    }


    private void LateUpdate()
    {
        if (_isLowerSlotSkillNotNull)
        {
            LowerCooldownRoutine();
        }

        if (_isUpperSlotSkillNotNull)
        {
            UpperCooldownRoutine();
        }
    }

    private void LowerCooldownRoutine() //lower and upper cooldown routine manage the state of the corresponding skill.
    {
        switch (lowerSlotSkill.state)
        {
            case BaseSkill.AbilityState.Ready:
                break;
            case BaseSkill.AbilityState.Activated:
                if (_lowerActiveTime > 0)
                {
                    _lowerActiveTime -= Time.deltaTime;
                }
                else
                {
                    lowerSlotSkill.state = BaseSkill.AbilityState.Cooldown;
                    lowerSlotSkill.StartCooldown(gameObject);
                }

                break;
            case BaseSkill.AbilityState.Cooldown:
                if (_lowerCoolDownTime > 0)
                {
                    _lowerCoolDownTime -= Time.deltaTime;
                }
                else
                {
                    lowerSlotSkill.state = BaseSkill.AbilityState.Ready;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpperCooldownRoutine()
    {
        switch (upperSlotSkill.state)
        {
            case BaseSkill.AbilityState.Ready:
                break;
            case BaseSkill.AbilityState.Activated:
                if (_upperActiveTime > 0)
                {
                    _upperActiveTime -= Time.deltaTime;
                }
                else
                {
                    upperSlotSkill.state = BaseSkill.AbilityState.Cooldown;
                    upperSlotSkill.StartCooldown(gameObject);
                }

                break;
            case BaseSkill.AbilityState.Cooldown:
                if (_upperCooldownTime > 0)
                {
                    _upperCooldownTime -= Time.deltaTime;
                }
                else
                {
                    upperSlotSkill.state = BaseSkill.AbilityState.Ready;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
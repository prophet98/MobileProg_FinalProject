
using System;
using System.Collections;
using UnityEngine;

public class SkillSlotsController : MonoBehaviour
{
    public BaseSkill lowerSlotSkill;
    public BaseSkill upperSlotSkill;

    private float _lowerActiveTime, _lowerCoolDownTime, _upperActiveTime, _upperCooldownTime;

    public bool isLowerNull, isUpperNull;
    private void Start()
    {
        if (lowerSlotSkill is null)
        {
            isLowerNull = true;
        }
        else if (lowerSlotSkill != null )
        {
            isLowerNull = false;
        }

        if (upperSlotSkill is null)
        {
            isUpperNull = true;
        }
        else if (upperSlotSkill != null)
        {
            isUpperNull = false;
        }
    }

    public void SetUpSkillTimers()
    {
        if (!isLowerNull)
        {
            _lowerActiveTime = lowerSlotSkill.activeTime;
            _lowerCoolDownTime = lowerSlotSkill.cooldownTime;
        }

        if (!isUpperNull)
        {
            _upperActiveTime = upperSlotSkill.activeTime;
            _upperCooldownTime = upperSlotSkill.cooldownTime;
        }
        
    }
    

    private void LateUpdate()
    {
        if (!isLowerNull)
        {
            LowerCooldownRoutine();
        }

        if (!isUpperNull)
        {
            UpperCooldownRoutine();
        }
    }

    private void LowerCooldownRoutine()
    {
        switch (lowerSlotSkill.state)
        {
            case BaseSkill.AbilityState.Ready:
                break;
            case BaseSkill.AbilityState.Activated:
                if(_lowerActiveTime > 0)
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
                if(_upperActiveTime > 0)
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
        }
    }
}

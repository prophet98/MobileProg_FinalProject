
using System;
using System.Collections;
using UnityEngine;

public class SkillSlotsController : MonoBehaviour
{
    public BaseSkill lowerSlotSkill;
    public BaseSkill upperSlotSkill;

    private float _lowerActiveTime, _lowerCoolDownTime, _upperActiveTime, _upperCooldownTime;

    public void SetUpSkillTimers()
    {
        _lowerActiveTime = lowerSlotSkill.activeTime;
        _lowerCoolDownTime = lowerSlotSkill.cooldownTime;
        _upperActiveTime = upperSlotSkill.activeTime;
        _upperCooldownTime = upperSlotSkill.cooldownTime;
    }
    

    private void Update()
    {
        if(lowerSlotSkill == null || upperSlotSkill == null)
        {
            return;
        }

        LowerCooldownRoutine();
        UpperCooldownRoutine();
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

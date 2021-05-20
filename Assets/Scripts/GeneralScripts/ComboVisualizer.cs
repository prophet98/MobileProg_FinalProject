using System;
using System.Collections;
using System.Collections.Generic;
using DamageScripts;
using UnityEngine;
using UnityEngine.UI;

public class ComboVisualizer : MonoBehaviour
{
    private PlayerDamageAnimationEvents _playerInputComponent;
    private Image fillImage;
    private void Start()
    {
        _playerInputComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionsEventsHandler>().PlayerDamageAnimationEvents;
        _playerInputComponent.OnDamagedSuccess += IncreaseValue;
        _playerInputComponent.OnDamagedFail += DecreaseValue;
        fillImage = GetComponent<Image>();
    }

    private void DecreaseValue()
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseBarValue(.0f));
    }

    private void IncreaseValue()
    {
        StopAllCoroutines();
        GetComponent<Image>().fillAmount += .34f;
        StartCoroutine(DecreaseOnIdle(3.0f));
    }

    private IEnumerator DecreaseOnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        DecreaseValue();
    }
    private void OnDisable()
    {
        _playerInputComponent.OnDamagedSuccess -= IncreaseValue;
        _playerInputComponent.OnDamagedFail -= DecreaseValue;
    }
    
    private IEnumerator DecreaseBarValue(float value)
    {
        var preChangePct = fillImage.fillAmount;
        var elapsed = 0.0f;
        
        while (elapsed < .5f)
        {
            elapsed += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(preChangePct, value, elapsed / .5f);
            yield return null;
        }
        
        fillImage.fillAmount = value;
    } 
    
    
}


using System;
using System.Collections;
using DamageScripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds = 0.5f;
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private bool isPlayerBar;
    private void Awake()
    {
        foregroundImage = GetComponentInChildren<Image>();
        healthComponent.OnHealthPctChange += HandleHealthChange;
    }

    private void HandleHealthChange(float pct)
    {
        if (foregroundImage.IsActive())
        {
            StartCoroutine(ChangeHealthBarImagePct(pct));
        }
    }

    private IEnumerator ChangeHealthBarImagePct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0.0f;
        
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            if (isPlayerBar) foregroundImage.color = Color.red;
            yield return null;
        }
        if (isPlayerBar) foregroundImage.color = Color.cyan;
        foregroundImage.fillAmount = pct;
    }
    private void LateUpdate()
    {
        if (isPlayerBar)
        {
            return;
        }
        transform.LookAt(Camera.main.transform);
    }

    private void OnDisable()
    {
        healthComponent.OnHealthPctChange += HandleHealthChange;
    }
}

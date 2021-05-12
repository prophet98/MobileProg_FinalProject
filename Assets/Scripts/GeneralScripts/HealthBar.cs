
using System.Collections;
using DamageScripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds = 0.5f;
    private void Awake()
    {
        foregroundImage = GetComponentInChildren<Image>();
        GetComponentInParent<HealthComponent>().OnHealthPctChange += HandleHealthChange;
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
            yield return null;
        }
        
        foregroundImage.fillAmount = pct;
    }
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}

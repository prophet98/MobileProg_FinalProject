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
    private Camera _mainCamera;

    private void Awake() //get all needed components and subscribe the event to the health change. 
    {
        foregroundImage = GetComponentInChildren<Image>();
        healthComponent.OnHealthPctChange += HandleHealthChange;
        _mainCamera = Camera.main;
    }

    private void HandleHealthChange(float pct)
    {
        if (foregroundImage.IsActive())
        {
            StartCoroutine(ChangeHealthBarImagePct(pct));
        }
    }

    private IEnumerator ChangeHealthBarImagePct(float healthPercentage) //change health percentage with a smooth lerp.
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0.0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, healthPercentage, elapsed / updateSpeedSeconds);
            if (isPlayerBar) foregroundImage.color = Color.red;
            yield return null;
        }

        if (isPlayerBar) foregroundImage.color = Color.cyan;
        foregroundImage.fillAmount = healthPercentage;
    }

    private void LateUpdate()
    {
        if (isPlayerBar) //make the health bar face the camera, unless its the player bar.
        {
            return;
        }

        transform.LookAt(_mainCamera.transform);
    }

    private void OnDisable()
    {
        healthComponent.OnHealthPctChange += HandleHealthChange;
    }
}
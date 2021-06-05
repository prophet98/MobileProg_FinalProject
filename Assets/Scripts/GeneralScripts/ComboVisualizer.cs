using System.Collections;
using DamageScripts;
using UnityEngine;
using UnityEngine.UI;

public class ComboVisualizer : MonoBehaviour
{
    private PlayerDamageAnimationEvents _playerInputComponent;
    private Image _fillImage;

    private void Start()
    {
        _playerInputComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionsEventsHandler>()
            .PlayerDamageAnimationEvents;
        _fillImage = GetComponent<Image>();
        _playerInputComponent.OnDamagedSuccess += IncreaseValue;
        _playerInputComponent.OnDamagedFail += DecreaseValue;
    }

    private void DecreaseValue()
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseBarValue(.0f));
    }

    private void IncreaseValue() //increases bar value by a third
    {
        StopAllCoroutines();
        GetComponent<Image>().fillAmount += .34f;
        StartCoroutine(DecreaseOnIdle(2.5f));
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

    private IEnumerator DecreaseBarValue(float value) //lerp the current value to zero inside the combo button
    {
        var preChangePct = _fillImage.fillAmount;
        var elapsed = 0.0f;
        const float duration = .5f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _fillImage.fillAmount = Mathf.Lerp(preChangePct, value, elapsed / duration);
            yield return null;
        }

        _fillImage.fillAmount = value;
    }
}
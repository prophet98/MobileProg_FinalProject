using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private string volumeParameter;
    [SerializeField] private Toggle toggle;
    private bool _disableToggleEvent;

    private void Awake()
    {
        AdjustVolumeMixer();
    }

    public void AdjustVolumeMixer()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(HandleSliderValueChange);
        toggle.onValueChanged.AddListener(HandleToggleValueChange);
        _slider.value = PlayerPrefs.GetFloat(volumeParameter, _slider.value);
    }

    private void HandleToggleValueChange(bool enableSound)
    {
        if (_disableToggleEvent)
        {
            return;
        }
        _slider.value = enableSound ? _slider.maxValue : _slider.minValue;
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, _slider.value);
    }

    private void HandleSliderValueChange(float volume)
    {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(volume) * 30.0f);
        _disableToggleEvent = true;
        toggle.isOn = _slider.value > _slider.minValue;
        _disableToggleEvent = false;
        if (isActiveAndEnabled && volumeParameter == "SoundVolume")
        {
            SoundManager.instance?.Play(Sound.Names.UiSound);
        }
    }
}

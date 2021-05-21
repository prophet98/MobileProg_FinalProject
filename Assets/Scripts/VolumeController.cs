using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider _slider;
    public AudioMixer audioMixer;
    [SerializeField] private string volumeParameter;
    [SerializeField] private Toggle toggle;
    private bool _disableToggleEvent;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(HandleSliderValueChange);
        toggle.onValueChanged.AddListener(HandleToggleValueChange);
    }

    private void HandleToggleValueChange(bool enableSound)
    {
        if (_disableToggleEvent)
        {
            return;
        }
        _slider.value = enableSound ? _slider.maxValue : _slider.minValue;
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(volumeParameter, _slider.value);
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
    }
}

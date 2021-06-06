
using UnityEngine;

public class EnvLightCycle : MonoBehaviour
{
    [Tooltip("number of room to pass before the light changes")]
    [SerializeField]
    private int requiredRoomAmount = 3;
    private int _currentRoomAmount;

    [Tooltip("main light color variations")]
    [SerializeField]
    private Color[] colors;
    [Tooltip("main light intensity variations, must be the same lenght of colors array")]
    [SerializeField]
    private float[] intensities;
    private int _colorsIndex;

    [Tooltip("main light, this light will change color and orientation during the game")]
    [SerializeField]
    private Light sunSource;
    [Tooltip("rotate main light in degrees, must be the same lenght of colors array")]
    [SerializeField]
    private float[] xLightAngle;

    private Vector3 _sunAdjustAngle;

    private void Start()
    {
        _currentRoomAmount = 0;
        _sunAdjustAngle = new Vector3();
        RotateSun();
    }

    private void OnEnable()
    {
        Door.OnEnvChange += ChangeLight;
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= ChangeLight;
    }

    private void ChangeLight()
    {
        _currentRoomAmount++;
        if (_currentRoomAmount % requiredRoomAmount == 0)
        {
            _currentRoomAmount = 0;

            _colorsIndex++;
            if (_colorsIndex >= colors.Length)
                _colorsIndex = 0;

            sunSource.color = colors[_colorsIndex];
            sunSource.intensity = intensities[_colorsIndex];
            RotateSun();
        }
    }

    private void RotateSun()
    {
        _sunAdjustAngle.x = xLightAngle[_colorsIndex];
        _sunAdjustAngle.y = sunSource.transform.eulerAngles.y;
        _sunAdjustAngle.z = sunSource.transform.eulerAngles.z;

        sunSource.transform.eulerAngles = _sunAdjustAngle;
    }
}

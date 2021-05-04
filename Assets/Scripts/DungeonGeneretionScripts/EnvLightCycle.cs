using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvLightCycle : MonoBehaviour
{
    [Tooltip("number of room to pass before the light changes")]
    [SerializeField]
    private int requiredRoomAmount = 3;
    private int currentRoomAmount;

    [Tooltip("main light color variations")]
    [SerializeField]
    private Color[] colors;
    [Tooltip("main light intensity variations, must be the same lenght of colors array")]
    [SerializeField]
    private float[] intenstities;
    private int colorsIndex;

    [Tooltip("main light, this light will change color and orientation during the game")]
    [SerializeField]
    private Light sunSource;
    [Tooltip("add this amount in degrees to main light every time the light change")]
    [SerializeField]
    private float rotateAmount;

    private Vector3 sunAdjustAngle;

    void Start()
    {
        currentRoomAmount = 0;
        sunAdjustAngle = new Vector3();
        RotateSun();
    }
    
    private void OnEnable()
    {
        EnvEventManager.OnEnvChange += ChangeLight;
    }

    private void OnDisable()
    {
        EnvEventManager.OnEnvChange -= ChangeLight;
    }

    private void ChangeLight()
    {
        currentRoomAmount++;
        if (currentRoomAmount % requiredRoomAmount == 0)
        {
            currentRoomAmount = 0;

            colorsIndex++;
            if (colorsIndex >= colors.Length)
                colorsIndex = 0;

            sunSource.color = colors[colorsIndex];
            sunSource.intensity = intenstities[colorsIndex];
            RotateSun();
        }
    }

    private void RotateSun()
    {
        sunAdjustAngle.x = sunSource.transform.eulerAngles.x + rotateAmount;
        sunAdjustAngle.y = sunSource.transform.eulerAngles.y;
        sunAdjustAngle.z = sunSource.transform.eulerAngles.z;

        if(colorsIndex >= colors.Length)
        {
            sunAdjustAngle.x = rotateAmount;
        }

        sunSource.transform.eulerAngles = sunAdjustAngle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

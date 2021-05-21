using UnityEngine;

[System.Serializable]
public class Sound {

    [HideInInspector]
    public AudioSource source;
    public enum Names
    {
        MainTheme,
        FootSteps,
        BladeSwing01,
        BladeSwing02,
        BladeSwing03,
        DashSound,
        SlammerBite,
        LoveGun,
        SoBigMace,
        BossSound
    }
    public Names name = Names.MainTheme;
        
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = .75f;
    // [Range(0f, 1f)]
    // public float volumeVariance = .1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(1f, 2f)]
    public float pitchVariance = .1f;

    public bool loop = false;

    public bool isRandom = false;
        
    public bool canBeOverridden = false;
}
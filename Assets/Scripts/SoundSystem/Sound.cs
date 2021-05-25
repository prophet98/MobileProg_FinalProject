using UnityEngine;

[System.Serializable]
public class Sound {

    [HideInInspector]
    public AudioSource source;
    public enum Names
    {
        MainMenuTheme,
        BattleTheme,
        BossTheme,
        UiSound,
        BladeSwing01,
        BladeSwing02,
        BladeSwing03,
        DashSound,
        SlammerBite,
        LoveGun,
        SoBigMace,
        BossSound,
        MyDoomBlade
    }
    public Names name = Names.MainMenuTheme;
        
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

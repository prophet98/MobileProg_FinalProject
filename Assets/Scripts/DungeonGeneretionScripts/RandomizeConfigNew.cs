using UnityEngine;

public class RandomizeConfigNew : MonoBehaviour
{
    [SerializeField]
    private EnvConfiguration[] environmentConfigurations;
    
    [SerializeField]
    private GameObject walkableArea;
    [SerializeField]
    private GameObject penaltyArea;

    private EnvConfiguration _currentEnvConfig;

    public EnvConfiguration GetCurrentConfig { get => _currentEnvConfig; }

    #region init
    private void Start()
    {
        ChangeEnv();
    }

    private void OnEnable()
    {
        Door.OnEnvChange += ChangeEnv;
    }

    private void OnDisable()
    {
        Door.OnEnvChange -= ChangeEnv;
    }
    #endregion

    #region Randomization
    private void ChangeEnv()
    {
        SetNewConfig(randomConfig());
    }

    private EnvConfiguration randomConfig()
    {
        int index = Random.Range(0, environmentConfigurations.Length);
        EnvConfiguration config = environmentConfigurations[index];
        return config;
    }

    private void SetNewConfig(EnvConfiguration config)
    {
        _currentEnvConfig = config;
        
        MatChange(walkableArea, config.GroundMaterial);
        MatChange(penaltyArea, config.MalusMaterial);
    }

    private void MatChange(GameObject goToChange, Material newMat)
    {
        MeshRenderer mr = goToChange.GetComponent<MeshRenderer>();
        mr.material = newMat;
    }
    #endregion
}

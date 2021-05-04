using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeConfig : MonoBehaviour
{
    [SerializeField]
    private EnvConfiguration[] environmentConfigurations;

    [SerializeField]
    private GameObject[] walls;
    [SerializeField]
    private GameObject walkableArea;
    [SerializeField]
    private GameObject penaltyArea;

    // Start is called before the first frame update
    void Start()
    {
        ChangeEnv();
    }

    public void ChangeEnv()
    {
        SetNewConfig(randomConfig());
    }

    private EnvConfiguration randomConfig()
    {
        EnvConfiguration config;
        int index = Random.Range(0, environmentConfigurations.Length);
        config = environmentConfigurations[index];
        return config;
    }

    private void SetNewConfig(EnvConfiguration config)
    {
        for(int i = 0; i<walls.Length; i++)
        {
            MatChange(walls[i], config.WallsMaterial);
        }
        MatChange(walkableArea, config.GroundMaterial);
        MatChange(penaltyArea, config.MalusMaterial);
    }

    private void MatChange(GameObject goToChange, Material newMat)
    {
        MeshRenderer mr = goToChange.GetComponent<MeshRenderer>();
        mr.material = newMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

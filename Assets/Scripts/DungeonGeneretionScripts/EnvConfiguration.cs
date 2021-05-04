using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Environment Data Config", menuName = "Environment Data Config")]
[System.Serializable]
public class EnvConfiguration : ScriptableObject
{
    [SerializeField] private Material _groundMaterial;
    [SerializeField] private Material _wallsMaterial;
    [SerializeField] private Material _malusMaterial;


    public Material GroundMaterial { get => _groundMaterial; }
    public Material WallsMaterial { get => _wallsMaterial; }
    public Material MalusMaterial { get => _malusMaterial; }
}

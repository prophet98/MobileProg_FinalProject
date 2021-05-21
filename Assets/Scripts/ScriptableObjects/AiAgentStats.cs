using UnityEngine;

[CreateAssetMenu(fileName = "AiAgentStat", menuName = "ScriptableObjects/CreateAgentStat")]
public class AiAgentStats : ScriptableObject
{
    public float attackDistance;
    public float rotationSpeed;
    public float sightAngle;
    public int attackDamage;
}

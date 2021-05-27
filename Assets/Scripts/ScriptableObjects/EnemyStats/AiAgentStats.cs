using UnityEngine;

[CreateAssetMenu(fileName = "AiAgentStat", menuName = "ScriptableObjects/CreateAgentStat")]
public class AiAgentStats : ScriptableObject
{
    public float attackDistance;
    public float agentMovementSpeed;
    public float agentRotationSpeed;
    public float sightAngle;
    public int attackDamage;
}

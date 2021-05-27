using UnityEngine;
[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/CreatePlayerStats")]
public class PlayerStats : ScriptableObject
{
    public BaseSkill upperSkill;
    public int playerMoney;
    public float playerSpeed;
    public int playerHealth;
    public int playerWeaponDamage;
}

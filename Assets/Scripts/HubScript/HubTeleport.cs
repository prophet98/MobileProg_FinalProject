using UnityEngine;
public class HubTeleport : MonoBehaviour
{
    private string PLAYER_TAG = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            GameplayManager.instance?.LoadLevel("Dungeon_Final");
        }   
    }
}

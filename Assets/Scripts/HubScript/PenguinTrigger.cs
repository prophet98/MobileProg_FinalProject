using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            Debug.Log("Player in sigth");
        }
    }
}

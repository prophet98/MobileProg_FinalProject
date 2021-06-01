using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPenalty : MonoBehaviour
{
    [SerializeField]
    private GameObject penaltyParticles;
    [SerializeField]
    private GameObject slowDownImage;
    private float speed;
    private const string PLAYER_TAG = "Player";

    private void Start()
    {
        slowDownImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            slowDownImage.SetActive(true);
            
            speed = other.GetComponent<PlayerController>().PlayerSpeed;
            other.GetComponent<PlayerController>().PlayerSpeed = speed/2;


            GameObject particlesInstance = Instantiate(penaltyParticles, other.transform);
            Destroy(particlesInstance, 1f);
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<PlayerController>().PlayerSpeed = speed;
            slowDownImage.SetActive(false);
        }
    }
}

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
            SoundManager.instance?.Play(Sound.Names.SlowDown);
            slowDownImage.SetActive(true);
            
            speed = other.GetComponent<PlayerController>().PlayerSpeed;
            other.GetComponent<PlayerController>().PlayerSpeed = speed/2;
            Debug.Log(this.gameObject.name );

            GameObject particlesInstance = Instantiate(penaltyParticles, other.transform);
            Destroy(particlesInstance, 1f);
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        { 
            other.GetComponent<PlayerController>().PlayerSpeed = speed;
            Debug.Log(this.gameObject.name );
            slowDownImage.SetActive(false);
        }
    }
}

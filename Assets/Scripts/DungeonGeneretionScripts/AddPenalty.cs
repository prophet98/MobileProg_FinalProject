using UnityEngine;

public class AddPenalty : MonoBehaviour
{
    [SerializeField]
    private GameObject penaltyParticles;
    [SerializeField] 
    private GameObject slowDownImage;
    
    private float _speed;
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

            _speed = other.GetComponent<PlayerController>().PlayerSpeed;
            other.GetComponent<PlayerController>().PlayerSpeed = _speed / 2;

            GameObject particlesInstance = Instantiate(penaltyParticles, other.transform);
            Destroy(particlesInstance, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<PlayerController>().PlayerSpeed = _speed;
            slowDownImage.SetActive(false);
        }
    }
}
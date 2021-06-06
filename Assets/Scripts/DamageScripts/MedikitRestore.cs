
using UnityEngine;
using DamageScripts;

public class MedikitRestore : MonoBehaviour
{
    [SerializeField]
    private GameObject healParticles;
    
    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<HealthComponent>().RemoveHealth(-15);
            SoundManager.instance?.Play(Sound.Names.HealSound);
            GameObject particlesInstance = Instantiate(healParticles, other.transform);
            Destroy(particlesInstance, 1f);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right);
    }
}


using System.Collections;
using DamageScripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyType;

    private GameObject enemyInstance;
    public int enemyCoinValue;
    public GameObject deathParticle;
    private GameObject _deathParticleInstance;
    private void OnEnable()
    {
        SpawnEnemy();
    }
    
    private void SpawnEnemy()
    {
        enemyInstance = Instantiate(enemyType, transform.position, transform.rotation);
        _deathParticleInstance = Instantiate(deathParticle, transform.position, transform.rotation);
        enemyInstance.GetComponent<HealthComponent>().OnEntityDeath += StartDeathParticleCoroutine;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void StartDeathParticleCoroutine()
    {
        StartCoroutine(PlayDeathParticle(_deathParticleInstance));
    }
    IEnumerator PlayDeathParticle(GameObject particle)
    {
        particle.GetComponent<AudioSource>().outputAudioMixerGroup = SoundManager.instance?.soundEffectsMixer;
        particle.transform.position = enemyInstance.transform.position;
        particle.GetComponent<ParticleSystem>().Play();
        particle.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration);
        Destroy(particle);
    }
}

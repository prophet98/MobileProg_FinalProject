using System.Collections;
using DamageScripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyType;

    private GameObject _enemyInstance;
    public int enemyCoinValue;
    public GameObject deathParticle;
    private GameObject _deathParticleInstance;

    private void OnEnable()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy() //spawns an enemy and binds to him a death particle.
    {
        _enemyInstance = Instantiate(enemyType, transform.position, transform.rotation);
        _deathParticleInstance = Instantiate(deathParticle, transform.position, transform.rotation);
        _enemyInstance.GetComponent<HealthComponent>().OnEntityDeath += StartDeathParticleCoroutine;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void StartDeathParticleCoroutine()
    {
        StartCoroutine(PlayDeathParticle(_deathParticleInstance));
    }

    private IEnumerator PlayDeathParticle(GameObject particle) //particle is set to the enemy position, gets played and destroyed.
    {
        particle.GetComponent<AudioSource>().outputAudioMixerGroup = SoundManager.instance?.soundEffectsMixer;
        particle.transform.position = _enemyInstance.transform.position;
        particle.GetComponent<ParticleSystem>().Play();
        particle.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration);
        Destroy(particle);
    }
}
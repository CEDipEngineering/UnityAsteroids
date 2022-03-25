using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 8.0f;
    public float maxLifetime = 10.0f;

    public GameManager gm;


    private void Start(){
        StartCoroutine(Spawner());
    }

    private void Spawn(){
        for (int i = 0; i<this.spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
            Destroy(asteroid, this.maxLifetime);
        }
    }

    private IEnumerator Spawner(){
        float asteroidPerSec = 0.5f;
        while(true){
            asteroidPerSec = (FindObjectOfType<GameManager>().score / 1000)/10 + 0.5f;
            Debug.Log(FindObjectOfType<GameManager>().score / 1000);
            this.Spawn();
            yield return new WaitForSeconds(1.0f/asteroidPerSec);

        }
    }
    
}

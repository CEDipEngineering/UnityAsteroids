using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public Missile missilePrefab;
    public float spawnRate = 5.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;
    public float maxLifetime =20.0f;


    private void Start(){
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn(){
        for (int i = 0; i<this.spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            Missile missile = Instantiate(this.missilePrefab, spawnPoint, Quaternion.identity);
            Destroy(missile, this.maxLifetime);
        }
    }
}

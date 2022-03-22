using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public Bomb bombPrefab;
    public float spawnRate = 3.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 1.0f;
    public float maxLifetime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
        
    }

    // Update is called once per frame
    private void Spawn(){
        for (int i = 0; i<this.spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            Bomb bomb = Instantiate(this.bombPrefab, spawnPoint, Quaternion.identity);
            Destroy(bomb.gameObject, this.maxLifetime);
        }
    }
}

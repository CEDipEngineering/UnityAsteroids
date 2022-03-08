using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityWindow = 3.0f;
    public int lives = 3;
    public ParticleSystem explosion;
    public int score = 0;

    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;
        if(this.lives <= 0){
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid){
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        // TODO: Score
        if (asteroid.size < 0.75f){
            this.score += 150;
        } else if (asteroid.size <= 1.1f){
            this.score += 100;
        } else {
            this.score += 50;
        }


    }

    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Noclip");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(ResetCollisionMask), this.respawnInvulnerabilityWindow);
    }

    private void ResetCollisionMask(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){
        // TODO
    }
}

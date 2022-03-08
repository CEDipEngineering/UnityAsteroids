using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityWindow = 3.0f;
    
    public int lives = 3;
    public Text livesText;
    public int score = 0;
    public Text scoreText;

    public void Start(){
        SetScore(0);
        SetLives(3);
    }

    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        SetLives(lives-1);
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
            SetScore(score + 150);
        } else if (asteroid.size <= 1.1f){
            SetScore(score + 100);
        } else {
            SetScore(score + 50);
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

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}

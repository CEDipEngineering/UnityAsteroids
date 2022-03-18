using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float thrustSpeed = 5.0f;
    // public float turnSpeed = 0.1f;
    public float fireRate = 3.0f; // Shots per second
    public float starFireCooldown = 5.0f; // Star shots per second
    public int numStarShots = 12;
    private bool thrusting;
    private bool shots_fired = false;
    private bool star_shots_fired = false;
    private float turnDirection;
    private Rigidbody2D rigidbody;
    public Bullet bulletPrefab;

    private GameObject player;

    public GameObject shield;

    
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        this.shield.SetActive(false);
    }

    private void Update(){
        thrusting = Input.GetKey(KeyCode.W);
        
        
        // Follow mouse 
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward); 
        
        // Space-bar or left-click shoot
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)){
            Shoot();
        }

        // Bomb
        if(Input.GetKeyDown(KeyCode.R)){
            FindObjectOfType<GameManager>().useBomb();
        }

        // Star shot
        if(Input.GetMouseButton(1)){
            StarShoot();
        }   

        // Shield
        if(Input.GetKeyDown(KeyCode.Q)){
            FindObjectOfType<GameManager>().useShield();
        }   

    }

    private void FixedUpdate(){
        if(thrusting){
            rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
    }

    private void Shoot(){
        if (!shots_fired){
            Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
            shots_fired = true;
            Invoke(nameof(shootCooldown), 1.0f/fireRate);
        }
    }

    private void StarShoot(){
        if (!star_shots_fired){
            for(int i = 0; i<numStarShots; i++){
                float theta = i * 2 * Mathf.PI / numStarShots;
                float x = Mathf.Sin(theta);
                float y = Mathf.Cos(theta);
                Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, Quaternion.identity);
                bullet.Project(new Vector2(x,y));
                star_shots_fired = true;
            }
            Invoke(nameof(starShootCooldown), starFireCooldown);
        }
    }
 
    private void shootCooldown(){
        shots_fired = false;
    }

    private void starShootCooldown(){
        star_shots_fired = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Asteroid"){
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
        if (collision.gameObject.tag == "Missile"){
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }



    public IEnumerator changeAlpha(int flashCount){
        var playerRenderer = player.GetComponent<Renderer>();
        for(int i=0; i < flashCount; i++)
        {
            playerRenderer.material.SetColor("_Color", new Color(1.0f,1.0f,1.0f,0.5f));
            yield return new WaitForSeconds(0.25f);
            playerRenderer.material.SetColor("_Color", new Color(1.0f,1.0f,1.0f,1.0f));
            yield return new WaitForSeconds(0.25f);
        }
    }

}

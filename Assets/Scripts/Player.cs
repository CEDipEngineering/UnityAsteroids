using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrustSpeed = 5.0f;
    // public float turnSpeed = 0.1f;
    public float fireRate = 3.0f; // Shots per second
    private bool thrusting;
    private bool shots_fired = false;
    private float turnDirection;
    private Rigidbody2D rigidbody;
    public Bullet bulletPrefab;
    
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate(){
        if(thrusting){
            rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        // if (turnDirection != 0.0f){
        //     rigidbody.AddTorque(turnDirection*this.turnSpeed);
        // }
    }

    private void Shoot(){
        if (!shots_fired){
            Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
            shots_fired = true;
            Invoke(nameof(shootCooldown), 1.0f/fireRate);
        }
    }

    private void shootCooldown(){
        shots_fired = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Asteroid"){
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

}

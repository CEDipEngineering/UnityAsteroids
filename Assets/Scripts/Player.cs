using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrustSpeed = 1f;
    public float turnSpeed = 0.1f;
    private bool thrusting;
    private float turnDirection;
    private Rigidbody2D rigidbody;
    public Bullet bulletPrefab;
    
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        thrusting = Input.GetKey(KeyCode.W);
        if (Input.GetKey(KeyCode.A)){
            turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D)){
            turnDirection = -1.0f;
        } else {
            turnDirection = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }   
    }

    private void FixedUpdate(){
        if(thrusting){
            rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (turnDirection != 0.0f){
            rigidbody.AddTorque(turnDirection*this.turnSpeed);
        }
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
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

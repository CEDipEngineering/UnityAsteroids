using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject target;
    public float speed = 15.0f;
    public float rotationSpeed=10f;
    Vector3 upAxis = new Vector3 (0f, 0f, -1f);
    private Rigidbody2D rigidbody;

    void Start(){
        this.target = GameObject.Find("Player");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(this.target.activeSelf){
            var dir = this.transform.position - this.target.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
        }
    }

    void FixedUpdate(){
        rigidbody.AddForce(this.transform.up * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullet"){
            FindObjectOfType<GameManager>().MissileDestroyed(this);
        }
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 600.0f;
    public float maxLifetime = 10.0f;
    private Rigidbody2D rigidbody;
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction){
        rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }
    // Destroy on collision with wall/asteroid
    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(this.gameObject);
    }
}

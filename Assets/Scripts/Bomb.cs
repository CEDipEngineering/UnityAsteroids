using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    public Sprite sprite;
    public float size = 0.5f;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprite;
        rigidbody.mass = this.size;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            FindObjectOfType<GameManager>().BombCaptured(this);
        }
    }

    // Update is called once per frame
}

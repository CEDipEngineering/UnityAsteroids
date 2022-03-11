using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Play : MonoBehaviour
{
    public AudioSource explosion;
    public AudioSource collide;

    public void playExplosion()
    {
        explosion.Play();
    }

    public void playCollide()
    {
        collide.Play();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Brick"))
        {
            playExplosion();
        } 
        else if (col.gameObject.CompareTag("Player"))
        {
            playCollide();
        }
    }
}

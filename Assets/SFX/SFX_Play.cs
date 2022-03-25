using UnityEngine;

public class SFX_Play : MonoBehaviour
{
    public AudioSource explosion;
    public AudioSource shot;

    public void playExplosion()
    {
        explosion.Play();
    }
    public void playShot(){
        shot.Play();
    }
}

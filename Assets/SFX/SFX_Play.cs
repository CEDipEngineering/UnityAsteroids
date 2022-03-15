using UnityEngine;

public class SFX_Play : MonoBehaviour
{
    public AudioSource explosion;

    public void playExplosion()
    {
        explosion.Play();
    }
}

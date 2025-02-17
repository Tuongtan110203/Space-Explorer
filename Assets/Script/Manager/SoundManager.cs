using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    [SerializeField] private AudioClip starClip;
    [SerializeField] private AudioClip asteroidClip;


    private void Awake()
    {

        source = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void PlayStarSound()
    {
        source.PlayOneShot(starClip);
    }

    public void PlayHitAsteroidSound()
    {
        source.PlayOneShot(asteroidClip);
    }

}

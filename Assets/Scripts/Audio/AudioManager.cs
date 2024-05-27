using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip background;
    public AudioClip enemyDamage;
    public AudioClip playerDamage;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void soundEnemyDamage()
    {
        SFXSource.clip = enemyDamage;
        SFXSource.Play();
    }

    public void soundPlayerDamage()
    {
        SFXSource.clip = playerDamage;
        SFXSource.Play();
    }
}

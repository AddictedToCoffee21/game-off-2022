using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip music;
    public AudioClip DeathMusic;

    public GameObject Heartbeat;

    private float timeGone;

    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = music;
        _source.Play();
    }

    public void PlayBackground() 
    {
        Heartbeat.SetActive(false);
        _source.volume = 1f;
        _source.clip = music;
        _source.time = timeGone;
        _source.Play();

    }

    public void PlayDeathMusic() 
    {
        Heartbeat.SetActive(false);
        _source.volume = 1f;
        _source.clip = DeathMusic;
        _source.Play();
    }

    public void PlayHeartbeat()
    {
        Heartbeat.SetActive(true);
        _source.volume = 0.1f;
    }
}

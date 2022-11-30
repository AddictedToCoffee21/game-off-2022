using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip music;
    public AudioClip DeathMusic;

    public GameObject Heartbeat;

    public Options options;

    private float timeGone;

    private AudioSource _source;

    private bool isHeartBeatPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = music;
        _source.Play();
    }

    void Update() 
    {
        if(!isHeartBeatPlaying)
            _source.volume = Options.musicVolume;
        else
            _source.volume = Options.musicVolume * 0.3f;

        Heartbeat.GetComponent<AudioSource>().volume = Options.soundVolume;
    }

    public void PlayBackground() 
    {
        Heartbeat.SetActive(false);
        _source.volume = Options.musicVolume;
        _source.clip = music;
        _source.time = timeGone;
        _source.Play();
    }

    public void PlayDeathMusic() 
    {
        Heartbeat.SetActive(false);
        _source.volume = Options.musicVolume * 0.5f;
        _source.clip = DeathMusic;
        _source.Play();
    }

    public void PlayHeartbeat()
    {
        isHeartBeatPlaying = true;
        Heartbeat.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip BeeMusic;
    public AudioClip ButterflyMusic;
    public AudioClip DeathMusic;

    private float timeGone;

    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = ButterflyMusic;
        _source.Play();
    }

    public void FadeToState(PlayerState state) 
    {
        if(state == PlayerState.Butterfly) 
        {
            timeGone = _source.time;
            _source.clip = ButterflyMusic;
            _source.time = timeGone;
            _source.Play();
        }
        else if(state == PlayerState.Bee) 
        {
            timeGone = _source.time;
            _source.clip = BeeMusic;
            _source.time = timeGone;
            _source.Play();
        }
    }

    public void PlayDeathMusic() 
    {
        _source.clip = DeathMusic;
        _source.Play();
    }
}

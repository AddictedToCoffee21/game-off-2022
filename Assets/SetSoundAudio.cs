using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = Options.soundVolume;
    }

}

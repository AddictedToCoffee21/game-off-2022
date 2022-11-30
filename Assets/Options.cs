using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static float musicVolume = 0.5f;
    public static float soundVolume = 0.5f;

    public void SetMusicVolume(Scrollbar scrollbar) {
        musicVolume = scrollbar.value;
    }

    public void SetSoundVolume(Scrollbar scrollbar) {
        soundVolume = scrollbar.value;
    }
}

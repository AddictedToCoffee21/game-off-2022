using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveInformation : MonoBehaviour
{
    private int _waveCount, _enemyCount;
    public TMP_Text text;

    public void SetWaveCount(int waveCount) 
    {
        _waveCount = waveCount;
        UpdateTextDisplay();
    }

    public void SetEnemyCount(int enemyCount) 
    {
        _enemyCount = enemyCount;
        UpdateTextDisplay();
    }

    public void UpdateTextDisplay() 
    {
        text.text = "Wave " + _waveCount + "\n" + "Enemies left: " + _enemyCount;
    }


}

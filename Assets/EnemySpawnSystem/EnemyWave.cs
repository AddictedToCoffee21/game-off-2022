using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves")]
public class EnemyWave : ScriptableObject
{
    public List<GameObject> enemies;

    public int maxEnemyCount = 5;
    public float timeBetweenEnemySpawn = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

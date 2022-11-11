using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnSystem : MonoBehaviour
{
    public enum GameState
    {
        InWave,
        AfterWave
    }

    public Camera playerCamera;
    public Rigidbody2D playerRigidbody2D;
    public GameObject fly;

    [Space(10)] public List<EnemyWave> enemyWaves;
    public int maxEnemyCount;
    public int timeBetweenEnemySpawn = 1;
    public int timeBetweenWaves = 5;


    private List<Queue<GameObject>> _enemyWaveQueue;

    private float _rand1;
    private float _rand2;

    private float _xInnerBorderLeft;
    private float _xInnerBorderRight;
    private float _yInnerBorderDown;
    private float _yInnerBorderUp;

    private float _xOuterBorderLeft;
    private float _xOuterBorderRight;
    private float _yOuterBorderDown;
    private float _yOuterBorderUp;
    
    private int _currentWave;

    private float _currentTime;
    private int _maxTime;
    private GameState _currentGameState;
    private bool _canSpawnEnemy;
    private float _currentTimeBetweenWaves;

    private void Start()
    {
        Vector2 bottomLeft = playerCamera.ScreenToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        Vector2 topRight = playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, playerCamera.pixelHeight, playerCamera.nearClipPlane));

        _xInnerBorderLeft = bottomLeft.x - 1;
        _xInnerBorderRight = topRight.x + 1;
        _yInnerBorderDown = bottomLeft.y - 1;
        _yInnerBorderUp = topRight.y + 1;

        _xOuterBorderLeft = _xInnerBorderLeft - 3;
        _xOuterBorderRight = _xInnerBorderRight + 3;
        _yOuterBorderDown = _yInnerBorderDown - 3;
        _yOuterBorderUp = _yInnerBorderUp + 3;

        _enemyWaveQueue = new List<Queue<GameObject>>();
        _currentWave = 0;
        _currentTime = 0;
        _canSpawnEnemy = false;

        _currentGameState = GameState.InWave;

        for (int i = 0; i < enemyWaves.Count; i++)
        {
            _enemyWaveQueue.Add(new Queue<GameObject>(enemyWaves[i].enemies));
        }
    }

    private void Update()
    {
        Vector2 bottomLeft = playerCamera.ScreenToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        Vector2 topRight = playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, playerCamera.pixelHeight, playerCamera.nearClipPlane));

        _xInnerBorderLeft = bottomLeft.x - 1;
        _xInnerBorderRight = topRight.x + 1;
        _yInnerBorderDown = bottomLeft.y - 1;
        _yInnerBorderUp = topRight.y + 1;

        _xOuterBorderLeft = _xInnerBorderLeft - 3;
        _xOuterBorderRight = _xInnerBorderRight + 3;
        _yOuterBorderDown = _yInnerBorderDown - 3;
        _yOuterBorderUp = _yInnerBorderUp + 3;
        
        if (transform.childCount < maxEnemyCount)
            _currentTime += Time.deltaTime;

        if (_currentTime >= timeBetweenEnemySpawn)
        {
            _currentTime = 0;
            _canSpawnEnemy = true;
        }

        if (_enemyWaveQueue[_currentWave].Count == 0 && transform.childCount == 0)
        {
            _currentGameState = GameState.AfterWave;
        }

        switch (_currentGameState)
        {
            case GameState.InWave:
            {
                if (_canSpawnEnemy && transform.childCount < maxEnemyCount && _enemyWaveQueue[_currentWave].Count != 0)
                {
                    while (_rand1 >= _xInnerBorderLeft && _rand1 <= _xInnerBorderRight &&
                           _rand2 >= _yInnerBorderDown && _rand2 <= _yInnerBorderUp)
                    {
                        _rand1 = Random.Range(_xOuterBorderLeft, _xOuterBorderRight);
                        _rand2 = Random.Range(_yOuterBorderDown, _yOuterBorderUp);
                    }

                    Instantiate(_enemyWaveQueue[_currentWave].Dequeue(), new Vector3(_rand1, _rand2, 1),
                        Quaternion.identity, transform).GetComponent<Enemy>().target = playerRigidbody2D;

                    _rand1 = 0;
                    _rand2 = 0;
                    _canSpawnEnemy = false;
                }

                break;
            }

            case GameState.AfterWave:
            {
                Debug.Log("After wave");
                
                _currentTimeBetweenWaves += Time.deltaTime;

                if (_currentTimeBetweenWaves >= timeBetweenWaves)
                {
                    _currentTimeBetweenWaves = 0;
                    _currentWave++;
                    _currentGameState = GameState.InWave;
                }
                
                break;
            }
        }
        
        // if (_enemyWaveQueue[_currentWave].Count == 0)
        // {
        //     _currentWave++;
        //     _spawnWave = true;
        // }


        // if(_spawnWave)
        // {
        //     StartCoroutine(SpawnEnemy());
        //     _spawnWave = false;
        // }
        //
        //     var bottomLeft = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        //     var topLeft = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(0, playerCamera.pixelHeight, playerCamera.nearClipPlane));
        //     var topRight = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, playerCamera.pixelHeight, playerCamera.nearClipPlane));
        //     var bottomRight = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, 0, playerCamera.nearClipPlane));
        //     
        //     Vector2 offset1 = new Vector2(4, 4);
        //     Vector2 offset2 = new Vector2(4, -4);
        //     
        //     Debug.DrawLine(bottomLeft - offset1, topLeft - offset2);
        //     Debug.DrawLine(bottomLeft - offset1, bottomRight + offset2);
        //     Debug.DrawLine(topRight + offset1, topLeft - offset2);
        //     Debug.DrawLine(topRight + offset1, bottomRight + offset2);
    }
}
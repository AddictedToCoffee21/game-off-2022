using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody2D playerRigidbody2D;
    public GameObject fly;

    private void Start()
    {
        Vector3 test = new Vector3(0, 0, 0);
        Instantiate(fly, new Vector3(0, 0, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;
        Instantiate(fly, new Vector3(0, 10, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;
        Instantiate(fly, new Vector3(10, 0, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;
        Instantiate(fly, new Vector3(10, 10, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnSystem : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody2D playerRigidbody2D;
    public GameObject fly;

    private void Start()
    {
        Vector2 bottomLeft = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        Vector2 topRight = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, playerCamera.pixelHeight, playerCamera.nearClipPlane));

        // Instantiate(fly, new Vector3(bottomLeft.x, bottomLeft.y, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;
        // Instantiate(fly, new Vector3(topRight.x, topRight.y, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;

        float xInnerBorderLeft = bottomLeft.x - 1;
        float xInnerBorderRight = topRight.x + 1;
        float yInnerBorderDown = bottomLeft.y - 1;
        float yInnerBorderUp = topRight.y + 1;

        float xOuterBorderLeft = xInnerBorderLeft - 3;
        float xOuterBorderRight = xInnerBorderRight + 3;
        float yOuterBorderDown = yInnerBorderDown - 3;
        float yOuterBorderUp = yInnerBorderUp + 3;

        float rand1 = 0;
        float rand2 = 0;

        for (int i = 0; i < 5000; i++)
        {
            while ((rand1 >= xInnerBorderLeft && rand1 <= xInnerBorderRight) && (rand2 >= yInnerBorderDown && rand2 <= yInnerBorderUp))
            {
                rand1 = Random.Range(xOuterBorderLeft, xOuterBorderRight);
                rand2 = Random.Range(yOuterBorderDown, yOuterBorderUp);
            }

            
            Instantiate(fly, new Vector3(rand1, rand2, 1), Quaternion.identity).GetComponent<Enemy>().target = playerRigidbody2D;

            rand1 = 0;
            rand2 = 0;
        }


    }

    private void Update()
    {
        var bottomLeft = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        var topLeft = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(0, playerCamera.pixelHeight, playerCamera.nearClipPlane));
        var topRight = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, playerCamera.pixelHeight, playerCamera.nearClipPlane));
        var bottomRight = (Vector2)playerCamera.ScreenToWorldPoint(new Vector3(playerCamera.pixelWidth, 0, playerCamera.nearClipPlane));
        
        Vector2 offset1 = new Vector2(4, 4);
        Vector2 offset2 = new Vector2(4, -4);
        
        
        Debug.DrawLine(bottomLeft - offset1, topLeft - offset2);
        Debug.DrawLine(bottomLeft - offset1, bottomRight + offset2);
        Debug.DrawLine(topRight + offset1, topLeft - offset2);
        Debug.DrawLine(topRight + offset1, bottomRight + offset2);
    }
}

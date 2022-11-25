using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0,1)]
    public float _smoothTime = 0.125f;

    public Rigidbody2D target;

    private Vector3 _offset = new Vector3(0,0,-10);
    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if(!target.gameObject.GetComponent<PlayerController>().IsPlayerDead()) {
            Vector3 desiredPosition = (Vector3) target.position + _offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTime, float.PositiveInfinity, Time.deltaTime);
            this.transform.position = smoothedPosition;
        }

    }

}

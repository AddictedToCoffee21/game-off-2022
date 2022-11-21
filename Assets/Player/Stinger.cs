using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : Bullet
{

    public float fadeOutTime = 1.0f;
    public float fadeOutStepSize = 0.2f;

    private Vector3 _scale;
    private Vector2 _startVelocity;

    public override void DestroyBullet() 
    {
        _scale = this.transform.localScale;
        _startVelocity = base._rb2d.velocity;

        StartCoroutine("FadeOutBullet");
    }

    public IEnumerator FadeOutBullet() 
    {
        //Assuming Scale is equal for x and y
        int stepCount = (int) (this.transform.localScale.x / fadeOutStepSize);
        int step = 1;
        while(step <= stepCount) 
        {
            this.transform.localScale = _scale * (1 - fadeOutStepSize);
            _rb2d.velocity = _startVelocity * (1 - fadeOutStepSize) * 0.5f;
            yield return new WaitForSecondsRealtime(fadeOutTime / stepCount);
            step++;
        }
        Destroy(this.gameObject);
    }
}

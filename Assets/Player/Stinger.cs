using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinger : Bullet
{

    public float fadeOutTime = 1.0f;
    public float fadeOutStepSize = 0.2f;

    private Vector3 _scale;

    public override void DestroyBullet() 
    {
        _scale = this.transform.localScale;
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
            yield return new WaitForSecondsRealtime(fadeOutTime / stepCount);
            step++;
        }
        Destroy(this.gameObject);
    }
}

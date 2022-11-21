using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attackbar : MonoBehaviour
{
    private Image _img;

    public Sprite[] animationSprites;

    void Start() {
        _img = GetComponent<Image>();
    }

    public void UpdateAttackDisplay(bool isAttackReady) {
        _img.enabled = isAttackReady;
    }

    public void UpdateAttackDisplay(float attackCooldown, float cooldownLeft) 
    {
        _img.sprite = animationSprites[(animationSprites.Length - 1) -  (int) ((cooldownLeft / attackCooldown) * (animationSprites.Length -1 ))];
    }

}

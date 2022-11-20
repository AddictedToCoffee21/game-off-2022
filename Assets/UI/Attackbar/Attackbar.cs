using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attackbar : MonoBehaviour
{
    private Image _img;

    void Start() {
        _img = GetComponent<Image>();
    }

    public void UpdateAttackDisplay(bool isAttackReady) {
        _img.enabled = isAttackReady;
    }

}

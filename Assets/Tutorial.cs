using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject Movement;
    public GameObject Switch;
    public GameObject Shoot;

    private static bool isTutorialFinished = false;


    void Start()
    {
        if(isTutorialFinished) 
        {
            Movement.active = false;
            Switch.active = false; 
            Shoot.active = false;
        }
    }

    public void HideMovementTutorial() {
        StartCoroutine("FadeOutTutorial", Movement);
    }

    public void HideSwitchTutorial() {
        StartCoroutine("FadeOutTutorial", Switch);
    }

    public void HideShootTutorial() {
        StartCoroutine("FadeOutTutorial", Shoot);
    }

    public bool IsTutorialFinished() {

        if(Movement.active == false && Switch.active == false && Shoot.active == false)
        {
            isTutorialFinished = true;
            return isTutorialFinished;
        }

        return isTutorialFinished;
    }

    IEnumerator FadeOutTutorial(GameObject obj) {

        Image[] img = obj.GetComponentsInChildren<Image>();

        while(img[0].color.a > 0) {

            for(int i = 0; i < img.Length; i++) {
                img[i].color = new Color(img[i].color.r,img[i].color.g,img[i].color.b, img[i].color.a - 0.02f);
            }
        
            yield return new WaitForSecondsRealtime(0.05f);
            
        }  

        obj.active = false;
        IsTutorialFinished();
    }
}

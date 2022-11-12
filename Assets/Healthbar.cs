using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Image heartFull;
    public Image heartEmpty;

    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        Image[] hearts = this.transform.GetComponentsInChildren<Image>();
        
        if(hearts == null) {
            hearts = new Image[0];
        }

        for(int i=0; i < maxHealth; i++)
        {
            if(i < currentHealth)
            {
                if (i < hearts.Length) 
                {
                    hearts[i].sprite = heartFull.sprite;
                }
                else
                {
                    Instantiate(heartFull, this.transform);
                }
            }
            else
            {
                if (i < hearts.Length) 
                {
                    hearts[i].sprite = heartEmpty.sprite;
                }
                else 
                {
                    Instantiate(heartEmpty, this.transform);
                }
            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetSpeed : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    float speed = 1;
    public void ToggleSpeed()
    {
        if(speed < 10) speed++;
        else speed = 1;

        Time.timeScale = speed;
        text.text = "x" + speed;
    }
}

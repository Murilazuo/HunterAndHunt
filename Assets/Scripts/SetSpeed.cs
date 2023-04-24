using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetSpeed : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    float currentSpeed = 1;
    public void ToggleSpeed()
    {
        if(currentSpeed < 10) currentSpeed++;
        else currentSpeed = 1;

        Time.timeScale = currentSpeed;
        text.text = "x" + currentSpeed;
    }
}

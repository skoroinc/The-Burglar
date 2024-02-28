using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text numberOfCircles;
    public TMP_Text timerText;
    private float currentTime;
    void Start()
    {
        
    }

    
    void Update()
    {
        currentTime = Mathf.Round(Time.time);
        timerText.text = currentTime.ToString();
        numberOfCircles.text = Mathf.Round((currentTime - 4) / 10).ToString();
    }
}

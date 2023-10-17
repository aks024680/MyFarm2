using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LightController : MonoBehaviour
{

    //時間系統
    public GameObject timeSystem;
    //獲取光源
    private new Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        CircadianSystem();
    }
    //晝夜系統
    void CircadianSystem()
    {
        //實例化時間系統腳本
        TimeController timeSystemController = timeSystem.GetComponent<TimeController>();
        int minute = timeSystemController.minute;
        int second = timeSystemController.seconds;
        //21:00~05:00
        if (minute >= 21 && minute <= 23)
        {
            light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        if(minute >= 0 && minute <= 4)
        {
            light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        //5:01~12:00
        if(minute >= 5 && minute <= 11) {
            //當前經過多少時間 在這個時段
            int totalSecond = (minute - 5) * 60 + second;
            //經過多少RGB
            float groundRGB = totalSecond * (float)0.605;
            //當前RGB是多少
            float currentRGB =1+groundRGB;
            light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
        //12:01~15:00
        if (minute >= 12 && minute <= 14)
        {
            light.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        }
        //15:01~20:59
        if(minute >= 15 && minute <= 20)
        {
            //當前經過多長時間
            int totalSeconds = (minute - 15) *60 + second;
            //經過多少RGB
            float groundRGB = totalSeconds * (float)0.705;
            float currentRGB =254-groundRGB;
            //
            light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
    }
}

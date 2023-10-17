using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LightController : MonoBehaviour
{

    //�ɶ��t��
    public GameObject timeSystem;
    //�������
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
    //�ީ]�t��
    void CircadianSystem()
    {
        //��ҤƮɶ��t�θ}��
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
            //��e�g�L�h�֮ɶ� �b�o�Ӯɬq
            int totalSecond = (minute - 5) * 60 + second;
            //�g�L�h��RGB
            float groundRGB = totalSecond * (float)0.605;
            //��eRGB�O�h��
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
            //��e�g�L�h���ɶ�
            int totalSeconds = (minute - 15) *60 + second;
            //�g�L�h��RGB
            float groundRGB = totalSeconds * (float)0.705;
            float currentRGB =254-groundRGB;
            //
            light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
    }
}

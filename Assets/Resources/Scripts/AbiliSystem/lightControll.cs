using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lightControll : MonoBehaviour
{
    // Start is called before the first frame update
    // ʱ��ϵͳ
    public GameObject timeSystem;
    // ��ȡ��Դ
    private Light Light;
    void Start()
    {
        Light = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CircadianSystem();
    }

    // ��ҹϵͳ



    void CircadianSystem() {
        /**
            21:00-05:00 ����� RGB(0,0,0)
            06:01-12:00 �𽥱��� RGB(1~254,1~254,1~254)
            12:01-15:00 �������� RGB(255,255,255)
            15:01-20:59 �������� RGB(254~1,254~1,254~1)
            // ����ʱ�䳤�Ⱦ���������������ϱ䰵���ٶ�
            06:01-12:00 6Сʱ 360�� 254/360 = 0.705
        15:01-20:59 6Сʱ 360�� 254/360 = 0.705
        */
        timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        // ʵ����ʱ��ϵͳ�ű�
        TimeSystemContoller timeSystemContoller = timeSystem.GetComponent<TimeSystemContoller>();
        //
        int minutes = timeSystemContoller.showMinute;
        int seconds = timeSystemContoller.showSeconds;
        // 21:00 - 05:00
        if (minutes >=21 && minutes <=23) {
            Light.color = new Color(0/255f,0/255f,0/255f);
        }
        if (minutes>=0 && minutes <=4) {
            Light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        // 06:01-12:00
        if (minutes>=5 && minutes <=11) {
            //��ǰ�����˶೤�������ʱ���
            int totalSeconds = (minutes - 5) * 60 + seconds;
            // ��������RGB
            float groundRGB = totalSeconds * (float)0.705;
            // ��ǰ��RGB�Ƕ���
            float currentRGB = 1 + groundRGB;
            Light.color = new Color(currentRGB/255f, currentRGB / 255f, currentRGB / 255f);
        }
        // 12:01-15:00
        if (minutes>=12 && minutes <=14) {
            Light.color = new Color(255/255f,255/255f,255/255f);
        }
        // 15:01-20:59
        if (minutes>=15 && minutes<=20) {
            // ��ǰ�����೤ʱ��
            int totalSconds = (minutes - 15) * 60 + seconds;
            // ��������RGB
            float groundRGB = totalSconds * (float)0.705;
            // ��ǰRGB
            float currentRGB = 254 - groundRGB;
            // 
            Light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
    }
}

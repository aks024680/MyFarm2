using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circadianSystem : MonoBehaviour
{
    // Start is called before the first frame update
    Light Light;
    // ��ȡʱ�����
    public GameObject time;

    // Update is called once per frame
    void Update()
    {
        CircadianSystem();
    }
    // ��ҹϵͳ
    void CircadianSystem() {
        // ����ʱ���ʼ���ƹ�RGB��ɫ
        // ��ȡʱ��ϵͳ�ű����
        TimeSystemContoller systemContoller = time.GetComponent<TimeSystemContoller>();
        // ��ȡ��ǰʱ��ͷ���
        int minutes = systemContoller.showMinute;
        int seconds = systemContoller.showSeconds;
        // ��ȡ�ƹ����
        Light = GetComponent<Light>();
        // ����ʱ��λ�������
        /**
        21:00-05:00 ����� RGB(0,0,0)
        05:01-12:00 �𽥱��� RGB(1~254,1~254,1~254)
        12:01-15:00 �������� RGB(255,255,255)
        15:01-20:59 �������� RGB(254~1,254~1,254~1)
        // ����ʱ�䳤�Ⱦ���������������ϱ䰵���ٶ�
        05:01-12:00 7Сʱ 420�� 254/420 = 0.605
        15:01-20:59 6Сʱ 360�� 254/360 = 0.705
         */
        print(minutes +",,"+seconds);
        // ��ʱ��Ϊ21:00-05:00
        if (minutes >= 21 && minutes <= 23)
        {
            print("��ʱ��Ϊ21:00-05:00,0/255f"+(0/255f));
            Light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        if (minutes >= 0 && minutes <= 4)
        {
            print("��ʱ��Ϊ21:00-05:00");
            Light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        // ��ʱ��Ϊ05:00-12:00
        if (minutes >= 5 && minutes <= 11)
        {
            // ������������ݵ�ǰʱ���ȡ��ǰ��RGB��ɫ
            // ����
            int totalSeconds = (minutes - 5) * 60 + seconds;
            // ������RGB
            float groundRGB = (float)0.605 * totalSeconds;
            // ��ǰ��RGB 
            float currentRGB = 1 + groundRGB;
            print("5-12����ǰRGB��"+currentRGB);
            // ����RGB
            Light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
        // ��ʱ��Ϊ12:01 - 15:00
        if (minutes >= 12 && minutes <= 14)
        {
            Light.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        }
        // ��ʱ��Ϊ15:01 - 20:59
        if (minutes >= 15 && minutes <= 20)
        {
            // ������������ݵ�ǰʱ���ȡ��ǰ��RGB��ɫ
            // ����
            int totalSeconds = (minutes - 15) * 60 + seconds;
            // ������RGB
            float groundRGB = (float)0.705 * totalSeconds;
            // ��ǰ��RGB
            float currentRGB = 254 - groundRGB;
            print("��ʱ��Ϊ15:01 - 20:59:��ǰRGB" +currentRGB);
            // ����RGB
            Light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
    }

    // ʵ����ҹϵͳ

}

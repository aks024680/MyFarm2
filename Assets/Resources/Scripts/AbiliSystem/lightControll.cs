using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lightControll : MonoBehaviour
{
    // Start is called before the first frame update
    // 时间系统
    public GameObject timeSystem;
    // 获取光源
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

    // 昼夜系统



    void CircadianSystem() {
        /**
            21:00-05:00 保持最暗 RGB(0,0,0)
            06:01-12:00 逐渐变亮 RGB(1~254,1~254,1~254)
            12:01-15:00 保持最亮 RGB(255,255,255)
            15:01-20:59 降低亮度 RGB(254~1,254~1,254~1)
            // 根据时间长度决定白天变亮和晚上变暗的速度
            06:01-12:00 6小时 360秒 254/360 = 0.705
        15:01-20:59 6小时 360秒 254/360 = 0.705
        */
        timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        // 实例化时间系统脚本
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
            //当前经过了多长，在这个时间段
            int totalSeconds = (minutes - 5) * 60 + seconds;
            // 经过多少RGB
            float groundRGB = totalSeconds * (float)0.705;
            // 当前的RGB是多少
            float currentRGB = 1 + groundRGB;
            Light.color = new Color(currentRGB/255f, currentRGB / 255f, currentRGB / 255f);
        }
        // 12:01-15:00
        if (minutes>=12 && minutes <=14) {
            Light.color = new Color(255/255f,255/255f,255/255f);
        }
        // 15:01-20:59
        if (minutes>=15 && minutes<=20) {
            // 当前经过多长时间
            int totalSconds = (minutes - 15) * 60 + seconds;
            // 经过多少RGB
            float groundRGB = totalSconds * (float)0.705;
            // 当前RGB
            float currentRGB = 254 - groundRGB;
            // 
            Light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circadianSystem : MonoBehaviour
{
    // Start is called before the first frame update
    Light Light;
    // 获取时间组件
    public GameObject time;

    // Update is called once per frame
    void Update()
    {
        CircadianSystem();
    }
    // 昼夜系统
    void CircadianSystem() {
        // 根据时间初始化灯光RGB颜色
        // 获取时间系统脚本组件
        TimeSystemContoller systemContoller = time.GetComponent<TimeSystemContoller>();
        // 获取当前时间和分钟
        int minutes = systemContoller.showMinute;
        int seconds = systemContoller.showSeconds;
        // 获取灯光组件
        Light = GetComponent<Light>();
        // 根据时间段划分亮度
        /**
        21:00-05:00 保持最暗 RGB(0,0,0)
        05:01-12:00 逐渐变亮 RGB(1~254,1~254,1~254)
        12:01-15:00 保持最亮 RGB(255,255,255)
        15:01-20:59 降低亮度 RGB(254~1,254~1,254~1)
        // 根据时间长度决定白天变亮和晚上变暗的速度
        05:01-12:00 7小时 420秒 254/420 = 0.605
        15:01-20:59 6小时 360秒 254/360 = 0.705
         */
        print(minutes +",,"+seconds);
        // 当时间为21:00-05:00
        if (minutes >= 21 && minutes <= 23)
        {
            print("当时间为21:00-05:00,0/255f"+(0/255f));
            Light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        if (minutes >= 0 && minutes <= 4)
        {
            print("当时间为21:00-05:00");
            Light.color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        }
        // 当时间为05:00-12:00
        if (minutes >= 5 && minutes <= 11)
        {
            // 定义变量，根据当前时间获取当前的RGB颜色
            // 秒钟
            int totalSeconds = (minutes - 5) * 60 + seconds;
            // 经过的RGB
            float groundRGB = (float)0.605 * totalSeconds;
            // 当前的RGB 
            float currentRGB = 1 + groundRGB;
            print("5-12，当前RGB："+currentRGB);
            // 设置RGB
            Light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
        // 当时间为12:01 - 15:00
        if (minutes >= 12 && minutes <= 14)
        {
            Light.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        }
        // 当时间为15:01 - 20:59
        if (minutes >= 15 && minutes <= 20)
        {
            // 定义变量，根据当前时间获取当前的RGB颜色
            // 秒钟
            int totalSeconds = (minutes - 15) * 60 + seconds;
            // 经过的RGB
            float groundRGB = (float)0.705 * totalSeconds;
            // 当前的RGB
            float currentRGB = 254 - groundRGB;
            print("当时间为15:01 - 20:59:当前RGB" +currentRGB);
            // 设置RGB
            Light.color = new Color(currentRGB / 255f, currentRGB / 255f, currentRGB / 255f);
        }
    }

    // 实现昼夜系统

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour
{
    public TMP_Text textTimeShow;
    public TMP_Text season;
    public TMP_Text day;
    //分鐘8:00
    public int minute = 6;
    //秒鐘
    public int seconds = 0;
    //totalSecond 60秒一個循環
    private float totalSecond;
    //季節 1234 1春天 2夏天 3秋天 4冬天
    public int seasonType = 1;
    //日期1-30
    public int dayTime = 30;

    // Start is called before the first frame update

     void Start()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        totalSecond += Time.fixedDeltaTime;
        //判斷時間經過一秒
        if (totalSecond >= 0.99f) {
            print(totalSecond);
            //運行程序也需要時間，我們無法準確獲取到秒鐘，為了接近，所以設定0.99
            
            //更新時間
            UpdateTime();
            //這裡是更新季節以及日期
            UpdateSeasonAndDay();
            totalSecond = 0;
        }
    }

    //更新時間
     void UpdateTime()
    {
        //23:59
        if (minute == 23) {
            //考慮秒鐘
        if (seconds == 59) { 
            //分鐘和秒鐘都是零
            minute = 0;
                seconds = 0;
                //重製文本為00:00
                textTimeShow.text = "00 : 00";
            }
            else
            {
                //秒鐘自增
                seconds++;
                //秒鐘是一位數還兩位數1,22,一位數需要前面補0
                //將int 轉字符串1/2/3 01 02 03

                if(seconds.ToString().Length ==1){
                //分鐘是23秒鐘補0
                textTimeShow.text = "23 : 0"　+ seconds.ToString();
                }
                else
                {
                    textTimeShow.text = "23 :" + seconds.ToString();
                }
            }
        }
        else
        {
            //分鐘在0~22範圍
            //秒鐘是否為59
          if(seconds == 59)
            {
                minute += 1;
                //second00
                seconds = 0;
                //分鐘是否為一位數
                string showMinute = minute.ToString();

                if(showMinute.Length == 1)
                {
                    showMinute = "0" + showMinute;
                }
                textTimeShow.text = showMinute+": 00";
            }
            else
            {
                seconds++;
                //分鐘和秒鐘都需要進行位數的判斷
                string showMinute = minute.ToString();
                string showSecond = seconds.ToString();
                //判斷分鐘位數
                if (showMinute.Length == 1)
                {
                    showMinute = "0" + showMinute;
                }
                //判斷秒鐘位數
                if (showSecond.Length == 1)
                {
                    showSecond = "0" + showSecond;
                }
                textTimeShow.text = showMinute + " : " + showSecond;
            }
        }
    }
    //更新季節以及日期
    void UpdateSeasonAndDay()
    {
        //00:00
        if (minute ==0 && seconds == 0)
        {
            //判斷季節,當前時刻
            //春天
            if(seasonType == 1) {
            //日期是否等於30
            if(dayTime == 30)
                {
                    //日期變更為1
                    dayTime = 0;
                    //變更為夏天
                    seasonType = 2;
                    //季節文本變動
                    season.text = "夏";
                   
                    season.color = new Color(129 / 255f, 169 / 255f, 169 / 255f, 255 / 255f);
                    //日期
                    day.text = "1天";
                }
                else
                {
                    //dayTime自增
                    dayTime+=1;
                    //更新日期的文本
                    day.text = dayTime.ToString() +"天";
                }
            }
            //夏天
            if (seasonType == 2)
            {
                //日期是否等於30
                if (dayTime == 30)
                {
                    //日期變更為1
                    dayTime = 0;
                    //變更為秋天
                    seasonType = 3;
                    //季節文本變動
                    season.text = "秋";
                    season.color = new Color(234 / 255f, 169 / 255f, 69 / 255f, 255 / 255f);
                    //日期
                    day.text = "1天";
                }
                else
                {
                    //dayTime自增
                    dayTime += 1;
                    //更新日期的文本
                    day.text = dayTime.ToString() + "天";
                }
            }
            //秋天
            if (seasonType == 3)
            {
                //日期是否等於30
                if (dayTime == 30)
                {
                    //日期變更為1
                    dayTime = 0;
                    //變更為冬天
                    seasonType = 4;
                    //季節文本變動
                    season.text = "冬";
                    season.color = new Color(200 / 255f, 9 / 255f, 149 / 255f, 199 / 255f);
                    //日期
                    day.text = "1天";
                }
                else
                {
                    //dayTime自增
                    dayTime += 1;
                    //更新日期的文本
                    day.text = dayTime.ToString() + "天";
                }
            }
            //冬天
            if (seasonType == 4)
            {
                //日期是否等於30
                if (dayTime == 30)
                {
                    //日期變更為1
                    dayTime = 0;
                    //變更為春天
                    seasonType = 1;
                    //季節文本變動
                    season.text = "春";
                    season.color = new Color(29 / 255f, 200 / 255f, 125 / 255f, 255 / 255f);
                    //日期
                    day.text = "1天";
                }
                else
                {
                    //dayTime自增
                    dayTime += 1;
                    //更新日期的文本
                    day.text = dayTime.ToString() + "天";
                }
            }
        }
    }
}

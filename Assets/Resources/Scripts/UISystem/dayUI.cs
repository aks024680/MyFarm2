using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dayUI : MonoBehaviour
{
    // Start is called before the first frame update
    // 获取父级物体
    private Transform daySystem;
    // 获取时间组件脚本
    private TimeSystemContoller timeSystemContoller;


    /// <summary>
    /// 原始图片颜色  (253,224,168)
    /// 原始文本颜色  (0,96,42)
    /// 
    /// 修改后图片颜色 (0,255,255)
    /// 修改后文本颜色 (255,0,0)
    /// 
    ///     ColorUtility.TryParseHtmlString("#5CACEE", out Color nowColor);
    ///     season.color = nowColor;
    /// </summary>
    void Start()
    {
        // 赋值父组件
        daySystem = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDayUI();
        
    }


    // 初始化日期UI显示
    void UpdateDayUI() {
        // 获取时间系统物体
        GameObject time = GameObject.FindGameObjectWithTag("uiSystem");
        // 获取时间系统的脚本
        timeSystemContoller = time.transform.GetChild(0).GetComponent<TimeSystemContoller>();
        // 日期文本显示、设置节日
        // 春
        if (timeSystemContoller.seasonTime == 1) {
            // 初始化
            for (int i=1;i<=30;i++) {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1日\n春节";
                }
                else if (i == 10)
                {
                    daySystem.Find("10").GetChild(0).GetComponent<Text>().text = "10日\n腊八";
                }
                else {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "日";
                }
            }

            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // 当前日期图片变色
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // 文本变色
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // 恢复原本的图片颜色
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // 恢复文本颜色
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }
        // 夏
        if (timeSystemContoller.seasonTime == 2)
        {
            for (int i = 1; i <= 30; i++)
            {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1日\n夏至";
                }
                else if (i == 18)
                {
                    daySystem.Find("18").GetChild(0).GetComponent<Text>().text = "10日\n钓鱼节";
                }
                else
                {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "日";
                }
            }
            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // 当前日期图片变色
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // 文本变色
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // 恢复原本的图片颜色
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // 恢复文本颜色
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }
        // 秋
        if (timeSystemContoller.seasonTime == 3)
        {
            for (int i = 1; i <= 30; i++)
            {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1日\n重阳";
                }
                else if (i == 15)
                {
                    daySystem.Find("15").GetChild(0).GetComponent<Text>().text = "10日\n丰收节";
                }
                else
                {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "日";
                }
            }
            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // 当前日期图片变色
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // 文本变色
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // 恢复原本的图片颜色
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // 恢复文本颜色
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }
        // 冬
        if (timeSystemContoller.seasonTime == 4)
        {
            for (int i = 1; i <= 30; i++)
            {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1日\n冬至";
                }
                else if (i == 22)
                {
                    daySystem.Find("22").GetChild(0).GetComponent<Text>().text = "10日\n感恩节";
                }
                else
                {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "日";
                }
            }
            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // 当前日期图片变色
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // 文本变色
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // 恢复原本的图片颜色
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // 恢复文本颜色
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }




    }

}

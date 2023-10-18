using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFarm
{
    /// <summary>
    /// 日期控制器
    /// </summary>
    public class DayUIController : MonoBehaviour
    {
        //父物件
        private Transform daySystemTest;
        //獲取時間組件腳本
        private TimeController timeController;
        private void Start()
        {
            daySystemTest = GetComponent<Transform>();
            
        }
        private void Update()
        {
            UpdateDayUI();
        }
        //更新日期
        void UpdateDayUI()
        {
            GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem");
            timeController = timeSystem.transform.GetChild(0).GetComponent<TimeController>();
            

            //節日
            //春季
            if(timeController.seasonType == 1) 
            {
                for (int i = 1;i <=30;i++ ) { 
                //節日
                if(i == 1)
                    {
                        daySystemTest.Find("1").GetChild(0).GetComponent<TextMeshProUGUI>().text = "1日\n春節";
                       
                    }
                else if(i == 15) { 
                        daySystemTest.Find("15").GetChild(0).GetComponent<TextMeshProUGUI>().text = "15日\n元宵";
                        
                    }
                else
                    {
                        daySystemTest.Find(i.ToString()).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + "日";
                    }
                }   
                foreach(Transform child in daySystemTest) {
                    if (timeController.dayTime.ToString().Equals(child.name))
                    {
                        //變換顏色
                        //圖片
                        daySystemTest.Find(timeController.dayTime.ToString()).GetComponent<Image>().color = new Color();
                        //文本顏色
                    }
                }
            }

            // 夏季
            if (timeController.seasonType == 2)
            {
                for (int i = 1; i <= 30; i++)
                {
                    //節日
                    if (i == 1)
                    {
                        daySystemTest.Find("1").GetChild(0).GetComponent<TextMeshProUGUI>().text = "1日\n夏至";

                    }
                    else if (i == 22)
                    {
                        daySystemTest.Find("22").GetChild(0).GetComponent<TextMeshProUGUI>().text = "22日\n打魚季";

                    }
                    else
                    {
                        daySystemTest.Find(i.ToString()).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + "日";
                    }
                }
            }
            //秋季
            if (timeController.seasonType == 3)
            {
                for (int i = 1; i <= 30; i++)
                {
                    //節日
                    if (i == 1)
                    {
                        daySystemTest.Find("1").GetChild(0).GetComponent<TextMeshProUGUI>().text = "1日\n秋至";

                    }
                    else if (i == 21)
                    {
                        daySystemTest.Find("21").GetChild(0).GetComponent<TextMeshProUGUI>().text = "21日\n豐收祭";

                    }
                    else
                    {
                        daySystemTest.Find(i.ToString()).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + "日";
                    }
                }
            }
            //冬季
            if (timeController.seasonType == 4)
            {
                for (int i = 1; i <= 30; i++)
                {
                    //節日
                    if (i == 1)
                    {
                        daySystemTest.Find("1").GetChild(0).GetComponent<TextMeshProUGUI>().text = "1日\n冬至";

                    }
                    else if (i == 22)
                    {
                        daySystemTest.Find("22").GetChild(0).GetComponent<TextMeshProUGUI>().text = "22日\n萬聖節";

                    }
                    else
                    {
                        daySystemTest.Find(i.ToString()).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + "日";
                    }
                }
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShowControll : MonoBehaviour
{
    // 定义多长时间变更文本
    public float textChangeTime = 0;
    // 计算
    // 定义第一段剧情的文本
    public string[] text1 =new string[] {"总算到了","记得会有人来接我", "......","往前走走吧", "" };
    // 定义第二段剧情的文本
    public string[] text2 =new string[] {"这边有路标","看看","这样就能知道路了","" };
    // 文本变换下标
    public int index = -1;
    // 定义接收剧情播放的时间
    public float tatolPlayTime = 0;
    //判断是否播放完成
    public bool isPlay = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay == false) {
            textChangeTime += Time.deltaTime;
            tatolPlayTime += Time.deltaTime;
            if (textChangeTime >= 1.4)
            {
                // 第一段剧情内调用剧情的文本
                if (tatolPlayTime <= 10)
                {
                    changeText1();

                }
                else if (tatolPlayTime >= 22.5)
                {
                    changeText2();

                }
                textChangeTime = 0;
            }
        }

    }

    // 文本变换
    public void changeText1() {
        // 获取文本框
        GameObject dialog = GetComponent<Transform>().GetChild(0).gameObject;

        
        // 当下标为-1的时候
        if (index == -1)
        {
            dialog.SetActive(true);
        }
        else if (index == 5) {
            // 最后进行关闭
            //GetComponent<Transform>().gameObject.GetComponent<TextShowControll>().enabled = false;
            //GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false) ;
            print("index"+dialog.name);
            dialog.SetActive(false);
            index = -2;
        } else {
            print("下标:" + index);
            dialog.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = text1[index];
        }
        index++;
    }

    public void changeText2() {
        // 获取文本框
        GameObject dialog = GetComponent<Transform>().GetChild(0).gameObject;
        print("index,第二段" + index);
        // 当下标为-1的时候
        if (index == -1)
        {

            dialog.SetActive(true);
        }
        else if (index == 4)
        {
            // 最后进行关闭
            GetComponent<Transform>().gameObject.GetComponent<TextShowControll>().enabled = false;
            GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false);
            index = -1;
            isPlay = true;
        }
        else
        {
            dialog.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = text2[index];
        }
        index++;
    }

}

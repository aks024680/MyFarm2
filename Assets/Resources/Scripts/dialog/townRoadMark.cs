using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class townRoadMark : MonoBehaviour
{
    // 判断是否碰撞到路标
    private bool isEnterRoadMark = false;
    // 控制文本框的显示和隐藏
    private bool showDialog = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnterRoadMark == true)
        {
            FishingRoadMark();
        }

        //controllDialogJs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnterRoadMark = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnterRoadMark = false;
    }
    // 钓鱼湖路标指示
    public void FishingRoadMark()
    {
        // 获取实例化后的物体
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        // 获取预制体
        GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
        // 判断是否有实例化文本框
        if (!dialogGameObject)
        {
            Instantiate(dialog);
        }
        // 为防止初始的时候获取不到实例化的物体，重新赋值
        dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        // 判断是否按下E键
        if (Input.GetKeyDown(KeyCode.E))
        {
            //showDialog = !showDialog;
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // 变更文本
            string[] textContent = { "前往小镇" };

            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            //dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "前往小镇";


        }
    }

    // 设置文本框本身的脚本激活和失效
    public void controllDialogJs()
    {
        // 获取实例化后的物体
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (isEnterRoadMark == true)
            {
                // 当触碰到路标的时候，本身已经有显示隐藏的功能,不需要文本框自带隐藏文本框的功能
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = false;

            }
            else
            {
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = true;

            }
        }

    }
    
}

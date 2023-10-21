using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveToTran : MonoBehaviour
{
    // 判断是否碰撞到路标
    private bool isEnterTran = false;
    // 控制文本框的显示和隐藏
    private bool showDialog = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnterTran == true)
        {
            
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnterTran = true;
            FishingRoadMark();
            controllDialogJs();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnterTran = false;
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
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // 变更文本
            dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "现在不需要去火车站";
        
    }

    // 设置文本框本身的脚本激活和失效
    public void controllDialogJs()
    {
        // 获取实例化后的物体
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (isEnterTran == true)
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

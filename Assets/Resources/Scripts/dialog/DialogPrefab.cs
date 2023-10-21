using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPrefab : MonoBehaviour
{
    // 定义读取多文本下标
    public int index = 0;
    // 定义获取文本
    public string[] message = null;
    // public string[] message = new string {"11","222","333"}
    // 控制关闭文本框
    public bool closeDialog = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            DialogControll();


    }

    // 控制对话框的关闭
    public void DialogControll() {
        if (message.Length == 0) {
            return;
        }
        // 获取对话框预制体
        GameObject dialogPrefab = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
        print("长度："+message.Length);
        if (index < message.Length)
        {
            if (Input.GetKeyDown(KeyCode.E) && closeDialog == false) {
                dialogPrefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = message[index];
                index++;
                if (index == message.Length) {
                    closeDialog = true;
                }
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.E) && closeDialog == true) {
                dialogPrefab.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                dialogPrefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "";
                index = 0;
                Array.Clear(message, 0, message.Length);
                message = new string[0];
                closeDialog = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCamera : MonoBehaviour
{
    // 定义变量接收运动镜头的时间
    private float total = 0;
    void Start()
    {
        ChangeStatus();
    }

    // Update is called once per frame
    void Update()
    {
        total += Time.deltaTime;
        if (total >= 10) {
            changeAllStausBack();
        }
    }

    // 限制角色操作和变更状态
    public void ChangeStatus() {
        // 变更状态
        // 获取cineMachine摄像机
        GameObject cm = GameObject.FindGameObjectWithTag("cineMachine").transform.gameObject;
        // 获取运动的空物体
        Transform changeCameraObject = GetComponent<Transform>();
        print(changeCameraObject.name);
        print(cm.name);
        cm.GetComponent<CinemachineVirtualCamera>().Follow = changeCameraObject;

        // 获取玩家
        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        player.GetComponent<PlayerController>().enabled = false;

    }

    // 切换镜头跟踪物体
    public void changeAllStausBack() {
        // 获取cineMachine摄像机
        GameObject cm = GameObject.FindGameObjectWithTag("cineMachine").transform.gameObject;
        // 获取玩家
        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        // 切换镜头跟踪物体为主角
        cm.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        if (total >= 41) {
            player.GetComponent<PlayerController>().enabled = true;
            // 获取fishingMap场景的主音乐进行播放
            GameObject mainAudio = GameObject.Find("audioPlay").transform.gameObject;
            mainAudio.transform.GetChild(0).gameObject.SetActive(true);
        }


    }
}

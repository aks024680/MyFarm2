using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;
using System;
using UnityEngine.UI;

public class initDig : MonoBehaviour
{
    // 当进入矿区的时候进行初始化
    GameObject player;
    // 时间系统
    GameObject timeSystem;
    // ui系统
    GameObject uiSystem;
    // 玩家数据
    playerData pData;
    // 时间系统数据
    timeSystemData tsData;
    // 判断是否需要加载赋值数据
    NeedLoadData needLoadData;
    private void Awake()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo("Assets");
        directoryInfo.Refresh();
        // 赋值需要加载的数据
        //setObjectDataForInitMap();
        pData = PublicMethods.setObjectDataForPlayerData();
        tsData = PublicMethods.setObjectDataForTimeSystemData();
        needLoadData = PublicMethods.setObjectDataForNeedLoadData();
        // 初始化场景的时候进行初始化物体
        PublicMethods.initGameObjectForInitMap();
        // 配置项
        SetObjectValue();
        // 加载数据(根据跳转场景还是读档)
        loadData();
    }

    // 初始化地图的时候，赋值需要加载的数据// 读取并获取数据
    public void setObjectDataForInitMap()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        // 读取通过跳转场景时保存的数据
        TextAsset dataTxt = Resources.Load<TextAsset>("UpdateData/moveScene/moveSceneData");
        if (dataTxt)
        {
            string[] fileContent = dataTxt.text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            for (int i = 0; i < fileContent.Length; i++)
            {
                pData = JsonUtility.FromJson<playerData>(fileContent[0]);
                tsData = JsonUtility.FromJson<timeSystemData>(fileContent[1]);
            }
        }
        // 读取判断是否需要跳转场景赋值或读档赋值
        // 判断是否需要加载存档，判断是否有needLoadData文件
        TextAsset needLoadDataFile = Resources.Load<TextAsset>("UpdateData/saveData/needLoadData");
        if (needLoadDataFile != null)
        {
            // 判断是否需要加载文件
            needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
        }
    }

    // 根据跳转场景或读档进行加载数据
    public void loadData()
    {
        // 判断数据是否为空
        if (needLoadData != null)
        {
            // 判断是否是通过跳转场景进入
            if (needLoadData.isChangeScene == true)
            {
                //刷新文件夹
                DirectoryInfo directoryInfo = new DirectoryInfo(PublicMethods.saveDataPath);
                directoryInfo.Refresh();
                PublicMethods.SetGameObjectDataForChangeScene(pData, tsData);
                //SetGameObjectDataForChangeScene(pData, tsData);
                // 设置玩家坐标
                setPlayerPosition(pData);
            }
            else
            {
                // 判断是否通过读档进入
                if (needLoadData.isNeedLoadData == true)
                {
                    PublicMethods.SetGameObjectDataForLoadData();
                    //SetGameObjectDataForLoadData();
                }
            }
        }
    }
    // 设置默认配置
    public void SetObjectValue()
    {
        // 实例化复用的文本框
        GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (!dialog)
        {
            Instantiate(dialogPrefab);
        }
        // 设置跟随镜头
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject.FindGameObjectWithTag("cineMachine").GetComponent<CinemachineVirtualCamera>().Follow = player;

    }
    // 赋值玩家坐标
    public void setPlayerPosition(playerData plData)
    {
        if (plData != null)
        {
            Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerPosition = new Vector3();
            // 判断是从哪个场景进入进行设置玩家坐标
            switch (plData.beforeSceneName)
            {
                case "fishingMap":
                    playerPosition.x = -16;
                    playerPosition.y = -30;
                    playerPosition.z = 0;
                    break;
                case "town":
                    playerPosition.x = -14;
                    playerPosition.y = (float)3.5;
                    playerPosition.z = 0;
                    break;
                case "field":
                    playerPosition.x = (float)-19.50;
                    playerPosition.y = 31;
                    playerPosition.z = 0;
                    break;
                case "farm":
                    playerPosition.x = 58;
                    playerPosition.y = 70;
                    playerPosition.z = 0;
                    break;
                case "dig":
                    playerPosition.x = 134;
                    playerPosition.y = (float)21.5;
                    playerPosition.z = 0;
                    break;
            }
            Player.position = playerPosition;
        }
    }
}

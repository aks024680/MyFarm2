using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;
using System;
using UnityEngine.UI;

public class initDig : MonoBehaviour
{
    // �����������ʱ����г�ʼ��
    GameObject player;
    // ʱ��ϵͳ
    GameObject timeSystem;
    // uiϵͳ
    GameObject uiSystem;
    // �������
    playerData pData;
    // ʱ��ϵͳ����
    timeSystemData tsData;
    // �ж��Ƿ���Ҫ���ظ�ֵ����
    NeedLoadData needLoadData;
    private void Awake()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo("Assets");
        directoryInfo.Refresh();
        // ��ֵ��Ҫ���ص�����
        //setObjectDataForInitMap();
        pData = PublicMethods.setObjectDataForPlayerData();
        tsData = PublicMethods.setObjectDataForTimeSystemData();
        needLoadData = PublicMethods.setObjectDataForNeedLoadData();
        // ��ʼ��������ʱ����г�ʼ������
        PublicMethods.initGameObjectForInitMap();
        // ������
        SetObjectValue();
        // ��������(������ת�������Ƕ���)
        loadData();
    }

    // ��ʼ����ͼ��ʱ�򣬸�ֵ��Ҫ���ص�����// ��ȡ����ȡ����
    public void setObjectDataForInitMap()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        // ��ȡͨ����ת����ʱ���������
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
        // ��ȡ�ж��Ƿ���Ҫ��ת������ֵ�������ֵ
        // �ж��Ƿ���Ҫ���ش浵���ж��Ƿ���needLoadData�ļ�
        TextAsset needLoadDataFile = Resources.Load<TextAsset>("UpdateData/saveData/needLoadData");
        if (needLoadDataFile != null)
        {
            // �ж��Ƿ���Ҫ�����ļ�
            needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
        }
    }

    // ������ת������������м�������
    public void loadData()
    {
        // �ж������Ƿ�Ϊ��
        if (needLoadData != null)
        {
            // �ж��Ƿ���ͨ����ת��������
            if (needLoadData.isChangeScene == true)
            {
                //ˢ���ļ���
                DirectoryInfo directoryInfo = new DirectoryInfo(PublicMethods.saveDataPath);
                directoryInfo.Refresh();
                PublicMethods.SetGameObjectDataForChangeScene(pData, tsData);
                //SetGameObjectDataForChangeScene(pData, tsData);
                // �����������
                setPlayerPosition(pData);
            }
            else
            {
                // �ж��Ƿ�ͨ����������
                if (needLoadData.isNeedLoadData == true)
                {
                    PublicMethods.SetGameObjectDataForLoadData();
                    //SetGameObjectDataForLoadData();
                }
            }
        }
    }
    // ����Ĭ������
    public void SetObjectValue()
    {
        // ʵ�������õ��ı���
        GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (!dialog)
        {
            Instantiate(dialogPrefab);
        }
        // ���ø��澵ͷ
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject.FindGameObjectWithTag("cineMachine").GetComponent<CinemachineVirtualCamera>().Follow = player;

    }
    // ��ֵ�������
    public void setPlayerPosition(playerData plData)
    {
        if (plData != null)
        {
            Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerPosition = new Vector3();
            // �ж��Ǵ��ĸ�����������������������
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

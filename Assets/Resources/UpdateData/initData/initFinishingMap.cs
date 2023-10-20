using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;
using System;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class initFinishingMap : MonoBehaviour
{
    // ��ʼ���������ͼ
    // ���
    GameObject player;
    // ʱ��ϵͳ
    GameObject timeSystem;
    // uiϵͳ
    GameObject uiSystem;
    // �������
    playerData pData;
    // ʱ��ϵͳ����
    timeSystemData tsData;
    // �ж��Ƿ���Ҫͨ����ת����/�������и�ֵ����
    NeedLoadData needLoadData;
    // ���ƾ��鲥��
    public bool firstTimeLine = true;
    // �ָ�ʵ������ɫ��ʱ��
    public float initPlayerTime = 0;

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
            //PublicMethods.initGameObjectForInitMap();
            initItem();
            // ������
            SetObjectValue();
            // ��������(������ת�������Ƕ���)
            loadData();

    }
    void Start()
    {
        // ������ſ�
        GameObject firstTimeLineObject = GameObject.FindGameObjectWithTag("firstTimeLine");
        firstTimeLineObject.transform.GetChild(0).gameObject.SetActive(true);
        firstTimeLineObject.transform.GetChild(1).gameObject.SetActive(true);
        firstTimeLineObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // ���������ʵ������ɫ
        if (firstTimeLine == true) {
            initPlayerTime += Time.deltaTime;
            if (initPlayerTime >= 45) {
                // ������ر�
                GameObject firstTimeLineObject = GameObject.FindGameObjectWithTag("firstTimeLine");
                firstTimeLineObject.transform.GetChild(0).gameObject.SetActive(false);
                firstTimeLineObject.transform.GetChild(1).gameObject.SetActive(false);
                firstTimeLineObject.transform.GetChild(2).gameObject.SetActive(false);
                // ʵ������ɫ
                initPlayerAndDataAfterTimeLine();
                // ���������ֿ���
                GameObject.Find("audioPlay").transform.GetChild(0).gameObject.SetActive(true);
                firstTimeLine = false;
            }
        }
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
        if (needLoadDataFile !=null)
        {
            // �ж��Ƿ���Ҫ�����ļ�
            needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
        }
    }
    // ������ת������������м�������
    public void loadData() {
        // �ж������Ƿ�Ϊ��
        if (needLoadData !=null) {
            // �ж��Ƿ���ͨ����ת��������
            if (needLoadData.isChangeScene == true)
            {
                //ˢ���ļ���
                DirectoryInfo directoryInfo = new DirectoryInfo(PublicMethods.moveScenePath);
                directoryInfo.Refresh();
                PublicMethods.SetGameObjectDataForChangeScene(pData,tsData);
                //SetGameObjectDataForChangeScene(pData, tsData);
                // ��ֵ�������
                setPlayerPosition(pData);
            }
            else {
                // �ж��Ƿ�ͨ����������
                if (needLoadData.isNeedLoadData == true) {
                    //ˢ���ļ���
                    DirectoryInfo directoryInfo = new DirectoryInfo(PublicMethods.saveDataPath);
                    directoryInfo.Refresh();
                    PublicMethods.SetGameObjectDataForLoadData();
                    //SetGameObjectDataForLoadData();
                }
            }
        }
    }
    // ���ø�ֵ
    public void SetObjectValue() {
        // ����cinemachine�ĵ�ͼ��ײ��
        // ��ȡʱ��ϵͳ
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem");
        // ��ȡcineMachine�µĽű�
        colliderMapController mapController = GameObject.FindGameObjectWithTag("cineMachine").GetComponent<colliderMapController>();
        if (!mapController.timeSys) {
            mapController.timeSys = timeSystem;
        }

    }
    // ��ֵ�������
    public void setPlayerPosition(playerData plData) {
        if (plData!=null) {
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
                    playerPosition.x = 29;
                    playerPosition.y = 2;
                    playerPosition.z = 0;
                    break;
            }
            Player.position = playerPosition;
        }
    }
    // ��ʼ��������ʱ�������������ݸ�ֵ(ͨ����ת����)
    public void SetGameObjectDataForChangeScene(playerData pData, timeSystemData tsData)
    {
        // ��ֵ�������
        if (pData != null)
        {
            Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
            // ��ֵ�������
            PlayerController playerController = Player.GetComponent<PlayerController>();
            playerController.currentDigExp = pData.currentDigExp;
            playerController.currentFieldExp = pData.currentFieldExp;
            playerController.currentFarmExp = pData.currentFarmExp;
            playerController.currentFishExp = pData.currentFishExp;
            playerController.currentPlayerExp = pData.currentPlayerExp;
            playerController.digLevel = pData.digLevel;
            playerController.fieldLevel = pData.fieldLevel;
            playerController.farmingLevel = pData.farmingLevel;
            playerController.fishingLevel = pData.fishingLevel;
            playerController.level = pData.level;
            playerController.currentEndurance = pData.currentEndurance;
            playerController.currentHunger = pData.currentHunger;
            playerController.currentHealth = pData.currentHealth;
            playerController.speed = pData.speed;
        }
        // ��ֵʱ��ϵͳ����
        if (tsData != null)
        {
            // ��ֵʱ��ϵͳ
            TimeSystemContoller timeSystemContoller = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
            timeSystemContoller.seasonTime = tsData.seasonTime;
            timeSystemContoller.showMinute = tsData.showMinute;
            timeSystemContoller.showSeconds = tsData.showSeconds;
        }
        // ����ת���������Ը��ر�
        NeedLoadDataSave(false, "", false, "");
    }
    // ��ʼ��������ʱ�������������ݸ�ֵ(ͨ������)
    public void SetGameObjectDataForLoadData()
    {
        // �ж��Ƿ���Ҫ���ش浵���ж��Ƿ���needLoadData�ļ�
        TextAsset needLoadDataFile = Resources.Load<TextAsset>("UpdateData/saveData" + "/" + "needLoadData");
        if (needLoadDataFile)
        {
            // �ж��Ƿ���Ҫ�����ļ�
            NeedLoadData needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
            // ��ȡ��Ҫ���ش浵���ļ���
            string fileName = needLoadData.needLoadFileName;
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            //����������մ浵����
            TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/" + fileName);
            string[] fileContent = data.text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            DataForPlayer dataForPlayer = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            DataForTimeSystem dataForTimeSystem = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // ��ֵ��Һ�ʱ��ϵͳ����
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.baseSpeed = dataForPlayer.baseSpeed;
            playerController.currentDigExp = dataForPlayer.currentDigExp;
            playerController.currentEndurance = dataForPlayer.currentEndurance;
            playerController.currentFarmExp = dataForPlayer.currentFarmExp;
            playerController.currentFieldExp = dataForPlayer.currentFieldExp;
            playerController.currentFishExp = dataForPlayer.currentFishExp;
            playerController.currentHealth = dataForPlayer.currentHealth;
            playerController.currentHunger = dataForPlayer.currentHunger;
            playerController.currentPlayerExp = dataForPlayer.currentPlayerExp;
            playerController.digLevel = dataForPlayer.digLevel;
            playerController.farmingLevel = dataForPlayer.farmingLevel;
            playerController.fieldLevel = dataForPlayer.fieldLevel;
            playerController.fishingLevel = dataForPlayer.fishingLevel;
            playerController.level = dataForPlayer.level;
            playerController.playerState = dataForPlayer.playerState;
            // �������λ��
            Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerstation = new Vector3();
            playerstation.x = dataForPlayer.playerX;
            playerstation.y = dataForPlayer.playerY;
            playerstation.z = dataForPlayer.playerZ;
            playerPosition.position = playerstation;
            // ����ʱ��ϵͳ����
            TimeSystemContoller timeSystemContoller = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
            timeSystemContoller.seasonTime = dataForTimeSystem.seasonTime;
            timeSystemContoller.showMinute = dataForTimeSystem.showMinute;
            timeSystemContoller.showSeconds = dataForTimeSystem.showSeconds;
            // ����������Ϊ��ֹ
            needLoadData.isNeedLoadData = false;
            string needLoadDataJson = JsonUtility.ToJson(needLoadData);
            // �����ļ�
            createFile("Assets/Resources/UpdateData/saveData", "needLoadData.txt", needLoadDataJson);
            // �����ļ�������
            NeedLoadDataSave(false, "", false, "");
        }
    }
    public static void createFile(string path, string name, string info)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //ֱ������д�룬���Ҫ��ԭ�ļ�����׷�����ݣ�Ӧ��fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
    // ���Ƿ���ת�ͱ��浽�ļ�
    public static void NeedLoadDataSave(bool isChangeScene, string beforeChangeSceneName, bool isNeedLoadData, string needLoadFileName)
    {
        NeedLoadData needLoadData = new NeedLoadData();
        needLoadData.isChangeScene = isChangeScene;
        needLoadData.beforeChangeSceneName = beforeChangeSceneName;
        needLoadData.isNeedLoadData = isNeedLoadData;
        needLoadData.needLoadFileName = needLoadFileName;
        createFile("Assets/Resources/UpdateData/saveData", "needLoadData.txt", JsonUtility.ToJson(needLoadData));
    }

    // �ڲ������������þ�ͷ�������
    public void initPlayerAndDataAfterTimeLine() {
        // ʵ�������
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/player/player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Instantiate(playerPrefab);
        }
        // ����������������
        Transform playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        CinemachineVirtualCamera cinemachineVirtual = GameObject.FindGameObjectWithTag("cineMachine").GetComponent<CinemachineVirtualCamera>();
        if (!cinemachineVirtual.Follow)
        {
            cinemachineVirtual.Follow = playerTran;
        }
    }

    // ʵ������Ʒ
    public void initItem() {

        // ��ʼ��ʱ��ϵͳ
        GameObject timeSystemPrefab = Resources.Load<GameObject>("Prefabs/mainFunc/TimeSystemUI");
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem");
        if (!timeSystem)
        {
            Instantiate(timeSystemPrefab);
        }
        //��ʼ��uiϵͳ
        GameObject uiSystemPrefab = Resources.Load<GameObject>("Prefabs/ui/uiSystem");
        GameObject uiSystem = GameObject.FindGameObjectWithTag("mainUI");
        if (!uiSystem)
        {
            Instantiate(uiSystemPrefab);
        }
        // ��ʼ���Ի���
        GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (!dialog)
        {
            Instantiate(dialogPrefab);
        }
    }
}

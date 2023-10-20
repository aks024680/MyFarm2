using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PublicMethods : MonoBehaviour
{
    // ��¼��Ҫ���õĹ��������͹�������
    // ��ת·��
    public static string UpdateDataPath = Application.dataPath + "/UpdateData";
    public static string moveScenePath = Application.dataPath + "/UpdateData/moveScene";
    public static string saveDataPath = Application.dataPath + "/UpdateData/saveData";
    // ����ת������ʱ����ظ�ֵ����ʱ��·�����ļ�����
    public static string moveSceneFileName = "moveSceneData.txt";
    // �ж��Ƿ���ת�����Ͷ���ʱ���ļ�
    public static string needDataFileName = "needLoadData.txt";
    ///�浵----------------------
    // �浵1�ļ�
    public static string data1FileName = "Data1.txt";
    // �浵2�ļ�
    public static string data2FileName = "Data2.txt";
    // �浵3�ļ�
    public static string data3FileName = "Data3.txt";
    ///--------------------------

    // ���Ƿ���ת�ͱ��浽�ļ�
    public static void NeedLoadDataSave(bool isChangeScene, string beforeChangeSceneName, bool isNeedLoadData, string needLoadFileName)
    {
        NeedLoadData needLoadData = new NeedLoadData();
        needLoadData.isChangeScene = isChangeScene;
        needLoadData.beforeChangeSceneName = beforeChangeSceneName;
        needLoadData.isNeedLoadData = isNeedLoadData;
        needLoadData.needLoadFileName = needLoadFileName;
        createFile(saveDataPath, needDataFileName, JsonUtility.ToJson(needLoadData));
    }
    // �����ļ�
    public static void createFile(string path, string name, string info)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //ֱ������д�룬���Ҫ��ԭ�ļ�����׷�����ݣ�Ӧ��fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
    // ɾ���ļ�
    public static void deleteOneFile(string path)
    {
        File.Delete(path);
    }
    // ��ȡ�ļ� // txt  json 
    public static string ReadFile(string filePath)
    {
        // �����ļ�����
        string finalStr = "";
        string[] strs = File.ReadAllLines(filePath);//��ȡ�ļ��������У��������ݶ�ȡ������õ��ַ�����strs�У�һ�д�һ����Ԫ
        for (int i = 0; i < strs.Length; i++)
        {
            finalStr += strs[i];//��ȡÿһ�У���������
            finalStr += "\n";//ÿһ��ĩβ����
        }
        return finalStr;
    }
    // ��ת������ʱ�򱣴���Ҫ���ص�����,������json���س�ȥ
    public static string ChangeSceneDataSave() {
        ///
        /// �������
        /// ʱ��ϵͳ����
        ///
        string loadJsonData;
        // ��ȡ��ұ�����������д��(�����������λ��)
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerData pData = new playerData();
        pData.speed = player.speed;
        pData.currentHealth = player.currentHealth;
        pData.currentHunger = player.currentHunger;
        pData.currentEndurance = player.currentEndurance;
        pData.playerState = player.playerState;
        pData.level = player.level;
        pData.fishingLevel = player.fishingLevel;
        pData.farmingLevel = player.farmingLevel;
        pData.fieldLevel = player.fieldLevel;
        pData.digLevel = player.digLevel;
        pData.currentPlayerExp = player.currentPlayerExp;
        pData.currentFishExp = player.currentFishExp;
        pData.currentFarmExp = player.currentFarmExp;
        pData.currentDigExp = player.currentDigExp;
        pData.beforeSceneName = SceneManager.GetActiveScene().name;
        loadJsonData = JsonUtility.ToJson(pData) + "\n\n";
        // ��ȡʱ����������д��
        TimeSystemContoller timeSys = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        timeSystemData time = new timeSystemData();
        time.showMinute = timeSys.showMinute;
        time.showSeconds = timeSys.showSeconds;
        time.seasonTime = timeSys.seasonTime;
        loadJsonData += JsonUtility.ToJson(time);
        return loadJsonData;
    }
    // ��ʼ����ͼ��ʱ�򣬸�ֵ��Ҫ���ص�����// ��ȡ����ȡ����
    // pData
    public static playerData setObjectDataForPlayerData() {
        if (!Directory.Exists(saveDataPath)) {
            Directory.CreateDirectory(saveDataPath);
        }
        if (!Directory.Exists(moveScenePath)) {
            Directory.CreateDirectory(moveScenePath);
        }
        DirectoryInfo directory = new DirectoryInfo(moveScenePath);
        directory.Refresh();
        FileInfo[] files = directory.GetFiles("*",SearchOption.AllDirectories);
        playerData pData = null;
        string data = null;
        if (files.Length!=0) {
            for (int i = 0;i < files.Length;i++) {
                if (files[i].Name.EndsWith(".meta")) {
                    continue;
                }
                if (files[i].Name.Contains("moveSceneData")) {
                    data = ReadFile(moveScenePath+"/"+moveSceneFileName);
                }
            }
        }
        if (data!=null) {
            string[] fileContent = data.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            pData = JsonUtility.FromJson<playerData>(fileContent[0]);
        }
        return pData;
    }
    // tsData
    public static timeSystemData setObjectDataForTimeSystemData()
    {
        if (!Directory.Exists(saveDataPath))
        {
            Directory.CreateDirectory(saveDataPath);
        }
        if (!Directory.Exists(moveScenePath))
        {
            Directory.CreateDirectory(moveScenePath);
        }
        DirectoryInfo directory = new DirectoryInfo(moveScenePath);
        directory.Refresh();
        FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
        timeSystemData tsData = null;
        string data = null;
        if (files.Length != 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                if (files[i].Name.Contains("moveSceneData"))
                {
                    data = ReadFile(moveScenePath + "/" + moveSceneFileName);
                }
            }
        }
        if (data != null)
        {
            string[] fileContent = data.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            tsData = JsonUtility.FromJson<timeSystemData>(fileContent[1]);
        }
        return tsData;
    }
    public static NeedLoadData setObjectDataForNeedLoadData()
    {
        if (!Directory.Exists(saveDataPath))
        {
            Directory.CreateDirectory(saveDataPath);
        }
        if (!Directory.Exists(moveScenePath))
        {
            Directory.CreateDirectory(moveScenePath);
        }
        DirectoryInfo directory = new DirectoryInfo(saveDataPath);
        directory.Refresh();
        FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
        NeedLoadData needLoadData = null;
        string data = null;
        if (files.Length != 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                if (files[i].Name.Contains("needLoadData"))
                {
                    data = ReadFile(saveDataPath + "/" + needDataFileName);
                }
            }
        }
        if (data != null)
        {
            //string[] fileContent = data.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            needLoadData = JsonUtility.FromJson<NeedLoadData>(data);
        }
        return needLoadData;
    }
    public static void setObjectDataForInitMap(playerData pData, timeSystemData tsData, NeedLoadData needLoadData)
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        // ��ȡͨ����ת����ʱ���������
        TextAsset dataTxt = Resources.Load<TextAsset>("UpdateData/moveScene/moveSceneData");
        if (dataTxt)
        {
            string[] fileContent = dataTxt.text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
                pData = JsonUtility.FromJson<playerData>(fileContent[0]);
                tsData = JsonUtility.FromJson<timeSystemData>(fileContent[1]);

        }
        // ��ȡ�ж��Ƿ���Ҫ��ת������ֵ�������ֵ
        // �ж��Ƿ���Ҫ���ش浵���ж��Ƿ���needLoadData�ļ�
        TextAsset needLoadDataFile = Resources.Load<TextAsset>("UpdateData/saveData/needLoadData");
        print(needLoadDataFile.text);
        if (needLoadDataFile)
        {
            // �ж��Ƿ���Ҫ�����ļ�
            needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
        }
    }
    // ��ʼ��������ʱ����г�ʼ������ͽ������ݸ�ֵ
    public static void initGameObjectForInitMap() {
        // ��ʼ�����
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/player/player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Instantiate(playerPrefab);
        }
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
    // ��ʼ��������ʱ�������������ݸ�ֵ(ͨ����ת����)
    public static void SetGameObjectDataForChangeScene(playerData pData, timeSystemData tsData) {
        // ��ֵ�������
        if (pData!=null) {
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
        NeedLoadDataSave(false,"",false,"");
    }
    // ��ʼ��������ʱ�������������ݸ�ֵ(ͨ������)
    public static void SetGameObjectDataForLoadData()
    {
        // �ж��Ƿ���Ҫ���ش浵���ж��Ƿ���needLoadData�ļ�
        if (!Directory.Exists(saveDataPath))
        {
            Directory.CreateDirectory(saveDataPath);
        }
        if (!Directory.Exists(moveScenePath))
        {
            Directory.CreateDirectory(moveScenePath);
        }
        DirectoryInfo directory = new DirectoryInfo(saveDataPath);
        directory.Refresh();
        FileInfo[] files = directory.GetFiles("*",SearchOption.AllDirectories);
        bool needLoadFileFlag = false;
        string needDataText = null;
        for (int i = 0; i< files.Length;i++) {
            if (files[i].Name.EndsWith(".meta")) {
                continue;
            }
            if (files[i].Name.Contains("needLoadData")) {
                needLoadFileFlag = true;
                needDataText = ReadFile(saveDataPath+"/"+needDataFileName);
            }
        }
        // TextAsset needLoadDataFile = Resources.Load<TextAsset>(saveDataPath + "/" + needDataFileName);
        if (needLoadFileFlag == true)
        {
            // �ж��Ƿ���Ҫ�����ļ�
            NeedLoadData needLoadData = JsonUtility.FromJson<NeedLoadData>(needDataText);
            // ��ȡ��Ҫ���ش浵���ļ���
            string fileName = needLoadData.needLoadFileName;
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            //����������մ浵����
            //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/" + fileName);
            DirectoryInfo loadSaveData = new DirectoryInfo(Application.dataPath+ "UpdateData/saveData");
            loadSaveData.Refresh();
            string saveDataText = ReadFile(saveDataPath+"/"+needLoadData.needLoadFileName+".txt");
            string[] fileContent = saveDataText.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
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
            createFile(saveDataPath, needDataFileName, needLoadDataJson);
            // �����ļ�������
            NeedLoadDataSave(false,"",false,"");
        }
    }

}

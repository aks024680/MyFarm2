using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class saveData : MonoBehaviour
{
    // �������
    public DataForPlayer dfpData1;
    // ʱ��ϵͳ����
    public DataForTimeSystem dftsData1;
    // �������
    public DataForPlayer dfpData2;
    // ʱ��ϵͳ����
    public DataForTimeSystem dftsData2;
    // �������
    public DataForPlayer dfpData3;
    // ʱ��ϵͳ����
    public DataForTimeSystem dftsData3;
    // �ж��Ƿ���ڴ浵123
    bool data1Flag = false;
    bool data2Flag = false;
    bool data3Flag = false;
    // �浵123��·��
    string data1path;
    string data2path;
    string data3path;
    // ���մ��123������
    string data1Text = "";
    string data2Text = "";
    string data3Text = "";

    // Start is called before the first frame update
    private void Awake()
    {
        data1path = Application.dataPath + "/UpdateData/saveData/Data1.txt";
        data2path = Application.dataPath + "/UpdateData/saveData/Data2.txt";
        data3path = Application.dataPath + "/UpdateData/saveData/Data3.txt";
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ƿ���Esc������֮��رմ浵���棬����ʾ������
        changePanelShow();

    }

    private void OnEnable()
    {
        // ��ȡ�浵�����ļ�
        DirectoryInfo saveDataFiles = new DirectoryInfo(Application.dataPath + "/UpdateData/saveData/");
        saveDataFiles.Refresh();
        FileInfo[] files = saveDataFiles.GetFiles("*", SearchOption.AllDirectories);
        /*        TextAsset data1 = null;
                TextAsset data2 = null;
                TextAsset data3 = null;*/

        //FileInfo data1 = new FileInfo();
        if (files.Length != 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                // �ų�meta��ʽ�ļ�
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                if (files[i].Name.Contains("Data1"))
                {
                    data1Flag = true;
                    data1Text = PublicMethods.ReadFile(data1path);
                }
                if (files[i].Name.Contains("Data2"))
                {
                    data2Flag = true;
                    data2Text = PublicMethods.ReadFile(data2path);
                }
                if (files[i].Name.Contains("Data3"))
                {
                    data3Flag = true;
                    data3Text = PublicMethods.ReadFile(data3path);
                }
            }
        }
        print("flag:"+data1Flag+","+data2Flag+","+data3Flag);
        // ���������ļ�������ʾ������
        if (data1Flag == false)
        {
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        else {
            // ��ȡ����
            string[] fileContent = data1Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData1 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData1 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // ��ֵ
            gameObject.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData1.dataName;
            gameObject.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData1.dataSaveTime;
            string addrName = null;
            switch (dfpData1.currentSceneName)
            {
                case "fishingMap":
                    addrName = "�����";
                    break;
                case "town":
                    addrName = "����";
                    break;
            }
            gameObject.transform.GetChild(0).GetChild(1).GetChild(5).GetComponent<Text>().text = addrName;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        if (data2Flag == false)
        {
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            // ��ȡ����
            string[] fileContent = data2Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData2 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData2 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // ��ֵ
            gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData2.dataName;
            gameObject.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData2.dataSaveTime;
            string addrName = null;
            switch (dfpData2.currentSceneName)
            {
                case "fishingMap":
                    addrName = "�����";
                    break;
                case "town":
                    addrName = "����";
                    break;
            }
            gameObject.transform.GetChild(1).GetChild(1).GetChild(5).GetComponent<Text>().text = addrName;
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
        if (data3Flag == false)
        {
            gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            // ��ȡ����
            string[] fileContent = data3Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData3 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData3 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // ��ֵ
            gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData3.dataName;
            gameObject.transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData3.dataSaveTime;
            string addrName = null;
            switch (dfpData3.currentSceneName)
            {
                case "fishingMap":
                    addrName = "�����";
                    break;
                case "town":
                    addrName = "����";
                    break;
            }
            gameObject.transform.GetChild(2).GetChild(1).GetChild(5).GetComponent<Text>().text = addrName;
            gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }

    }

    // �رա���ʾ����
    public void changePanelShow() {
        if (Input.GetKey(KeyCode.Escape) && GameObject.FindGameObjectWithTag("SaveDataUI").gameObject.activeSelf == true) {
            GameObject saveDataMenu = GameObject.FindGameObjectWithTag("SaveDataUI").gameObject;
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
            saveDataMenu.transform.GetChild(0).gameObject.SetActive(false);
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);

        }
    }

    // ���ش浵1
    public void LoadSaveData1() {

        print("����1");

        if (data1Flag == false)
        {
            return;
        }

        // ���ж��費��Ҫ�л�����
        print(dfpData1.currentSceneName);
        if (SceneManager.GetActiveScene().name == dfpData1.currentSceneName)
        {
            // 
            // �رն�������
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
           // loadGameData("Data1");
        }
        else
        {
            // �����
            // �糡����ʱ����б���һ���ļ������ڼ��ص�ʱ���ȡ
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data1";
            string path = Application.dataPath + "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // �����ļ�
            CreateOrOPenFile(path, fileName, saveLoadData);
            // �رն�������
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // ��ת����
            
            SceneManager.LoadScene(dfpData1.currentSceneName);
        }
    }

    // ���ش浵2
    public void LoadSaveData2()
    {

        // �ж��Ƿ��ļ�Ϊ��
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data2");
        if (data2Flag == false)
        {
            return;
        }
        print(dfpData2.currentSceneName);
        // ���ж��費��Ҫ�л�����
        if (SceneManager.GetActiveScene().name == dfpData2.currentSceneName)
        {
            // 
            // �رն�������
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data2");
        }
        else
        {
            // �����
            // �糡����ʱ����б���һ���ļ������ڼ��ص�ʱ���ȡ
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data2";
            string path = Application.dataPath + "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // ɾ���ļ�
            //deleteFile(path + fileName);
            // �����ļ�
            CreateOrOPenFile(path, fileName, saveLoadData);

            // �رն�������
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // ��ת����
            
            SceneManager.LoadScene(dfpData2.currentSceneName);
        }
    }

    // ���ش浵3
    public void LoadSaveData3()
    {

        // �ж��Ƿ��ļ�Ϊ��
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data3");
        if (data3Flag == false)
        {
            return;
        }
        print(dfpData3.currentSceneName);
        // ���ж��費��Ҫ�л�����
        if (SceneManager.GetActiveScene().name == dfpData3.currentSceneName)
        {
            // 
            // �رն�������
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            //loadGameData("Data3");
        }
        else
        {
            // �����
            // �糡����ʱ����б���һ���ļ������ڼ��ص�ʱ���ȡ
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data3";
            string path = Application.dataPath + "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // ɾ���ļ�
            //deleteFile(path + fileName);
            // �����ļ�
            CreateOrOPenFile(path, fileName, saveLoadData);

            // �رն�������
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // ��ת����
            
            SceneManager.LoadScene(dfpData3.currentSceneName);
        }
    }
    // �����ļ�
    public void CreateOrOPenFile(string path, string name, string info)
    {          //·�����ļ�����д������
        // ���ļ���
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //ֱ������д�룬���Ҫ��ԭ�ļ�����׷�����ݣ�Ӧ��fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
    // ����������Ϸ����
    public void loadGameData(string name)
    {
        // ��ȡ��Ҫ���ش浵���ļ���
        //string path = "UpdateData/saveData/";
        //string fileName = name;
        // ˢ���ļ�
        // �ж����ĸ��浵
        string data = "";
        if (name == "Data1")
        {
            data = data1Text;
        }
        else if (name == "Data2")
        {
            data = data2Text;
        }
        else if (name == "Data3")
        {
            data = data3Text;
        }
        //����������մ浵����
        //TextAsset data = Resources.Load<TextAsset>(path + fileName);
        string[] fileContent = data.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
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
    }
}

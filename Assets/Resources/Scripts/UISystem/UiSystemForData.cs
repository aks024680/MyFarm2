using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UiSystemForData : MonoBehaviour
{
    // �ж��Ƕ������Ǵ浵;1�浵��2����
    public int dataType = 1;
    // ��ʼ����ʱ���ж��Ƿ��ǴӶ����м���
    public bool needLoadData = false;
    // �жϼ����ĸ��浵
    public string needLoadDataFileName;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeUI();
    }
    // �رմ�������桢��ϵͳ����
    public void ChangeUI()
    {
        // ����Esc,�浵����򿪵�
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.activeSelf == true)
        {
            //��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(true);
            // �رմ��������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        if (dataType == 1)
        {
            // ������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "�浵";
            // ��ȡ��data�����а�ť
            Button data1Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>();
            Button data2Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Button>();
            Button data3Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Button>();
            // ��������а󶨷���
            data1Button.onClick.RemoveAllListeners();
            data2Button.onClick.RemoveAllListeners();
            data3Button.onClick.RemoveAllListeners();
            // �󶨷���
            data1Button.onClick.AddListener(delegate { saveData1(); });
            data2Button.onClick.AddListener(delegate { saveData2(); });
            data3Button.onClick.AddListener(delegate { saveData3(); });

        }
        else
        {
            // ������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "����";
            // ��ȡ��data�����а�ť
            Button data1Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>();
            Button data2Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Button>();
            Button data3Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Button>();
            // ��������а󶨷���
            data1Button.onClick.RemoveAllListeners();
            data2Button.onClick.RemoveAllListeners();
            data3Button.onClick.RemoveAllListeners();
            // �󶨷���
            data1Button.onClick.AddListener(delegate { loadData1(); });
            data2Button.onClick.AddListener(delegate { loadData2(); });
            data3Button.onClick.AddListener(delegate { loadData3(); });
        }
        // ��ȡ�浵�����ļ�
        DirectoryInfo saveDataFiles = new DirectoryInfo(Application.dataPath + "/UpdateData/saveData/");
        saveDataFiles.Refresh();
        FileInfo[] files = saveDataFiles.GetFiles("*",SearchOption.AllDirectories);
        /*        TextAsset data1 = null;
                TextAsset data2 = null;
                TextAsset data3 = null;*/

        //FileInfo data1 = new FileInfo();
        if (files.Length!=0) {
            for (int i = 0; i< files.Length;i++) {
                // �ų�meta��ʽ�ļ�
                if (files[i].Name.EndsWith(".meta")) {
                    continue;
                }
                if (files[i].Name.Contains("Data1")) {
                    data1Flag = true;
                    data1Text = PublicMethods.ReadFile(data1path);
                }
                if (files[i].Name.Contains("Data2")) {
                    data2Flag = true;
                    data2Text = PublicMethods.ReadFile(data2path);
                }
                if (files[i].Name.Contains("Data3")) {
                    data3Flag = true;
                    data3Text = PublicMethods.ReadFile(data3path);
                }
            }
        }
        //TextAsset data1 = Resources.Load<TextAsset>(Application.streamingAssetsPath+"/UpdateData/saveData/Data1");
        //TextAsset data2 = Resources.Load<TextAsset>(Application.streamingAssetsPath + "/UpdateData/saveData/Data2");
        //TextAsset data3 = Resources.Load<TextAsset>(Application.streamingAssetsPath + "/UpdateData/saveData/Data3");
        // ˢ���ļ���
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        // �ж��Ƿ��ж�Ӧ�ļ�����ʾ��ͬ�Ļ���
        if (data1Flag == false)
        {
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            // ��ȡ����
            string[] fileContent = data1Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData1 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData1 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // ��ֵ
            gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData1.dataName;
            gameObject.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData1.dataSaveTime;
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
            gameObject.transform.GetChild(1).GetChild(1).GetChild(5).GetComponent<Text>().text = addrName;
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
        if (data2Flag == false)
        {
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            // ��ȡ���ݽ��и�ֵ
            string[] fileContent = data2Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData2 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData2 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData2.dataName;
            gameObject.transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData2.dataSaveTime;
            string addr = null;
            switch (dfpData2.currentSceneName)
            {
                case "fishingMap":
                    addr = "�����";
                    break;
                case "town":
                    addr = "����";
                    break;
            }
            gameObject.transform.GetChild(2).GetChild(1).GetChild(5).GetComponent<Text>().text = addr;
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        if (data3Flag == false)
        {
            print("�޴浵3");
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            print("�д浵3");
            // ��ȡ���ݽ��и�ֵ
            string[] fileContent = data3Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData3 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData3 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            gameObject.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData3.dataName;
            gameObject.transform.GetChild(3).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData3.dataSaveTime;
            string addr = null;
            switch (dfpData3.currentSceneName)
            {
                case "fishingMap":
                    addr = "�����";
                    break;
                case "town":
                    addr = "����";
                    break;
            }
            gameObject.transform.GetChild(3).GetChild(1).GetChild(5).GetComponent<Text>().text = addr;
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
        }
    }

    // �����ͬ�����ݽ��в�ͬ����
    public void saveData1()
    {
        
        string path = Application.dataPath+"/UpdateData/saveData/";
        string fileName = "Data1.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        // ɾ���ļ�
        deleteFile(path + fileName);
        // ��ֵ����
        string saveTime = DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");
        string dataName = "�浵1";
        string data = null;
        data = DataJson(saveTime, dataName);
        // �����ļ�
        CreateOrOPenFile(path, fileName, data);
        // ˢ����ʾ����
        // �浵����
        string saveDataName = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "fishingMap":
                saveDataName = "�����";
                break;
            case "town":
                saveDataName = "����";
                break;
        }
        // �����ı���ʾ
        gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = dataName;
        gameObject.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = saveTime;
        gameObject.transform.GetChild(1).GetChild(1).GetChild(5).GetComponent<Text>().text = saveDataName;
        // �ж�null�Ƿ���ʾ��
        if (gameObject.transform.GetChild(1).GetChild(0).gameObject.activeSelf == true)
        {
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
    }
    public void loadData1()
    {
        print("����1");
        // �ж��Ƿ��ļ�Ϊ��
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data1");
        
        if (data1Flag == false) {
            return;
        }

        // ���ж��費��Ҫ�л�����
        if (SceneManager.GetActiveScene().name == dfpData1.currentSceneName)
        {
            // 
            // �رն�������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data1");
        }
        else {
            // �����
            // �糡����ʱ����б���һ���ļ������ڼ��ص�ʱ���ȡ
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data1";
            string path = Application.dataPath+"/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // �����ļ�
            CreateOrOPenFile(path, fileName, saveLoadData);
            // �رն�������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // ��ת����
            SceneManager.LoadScene(dfpData1.currentSceneName);
        }
    }
    public void saveData2()
    {
        // ɾ��ԭ�����ļ�
        string path = Application.dataPath+"/UpdateData/saveData";
        string name = "Data2.txt";
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        // TextAsset txt json yml ...
        deleteFile(path + name);
        // ��ֵ�������������
        string data = null;
        string saveTime = DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");
        string dataName = "�浵2";
        data = DataJson(saveTime, dataName);
        // �����ļ���������
        CreateOrOPenFile(path, name, data);
        // ��ʾ�浵�������
        // ������Ҫ����ı�������
        gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = dataName;
        gameObject.transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>().text = saveTime;
        string addr = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "fishingMap":
                addr = "�����";
                break;
            case "town":
                addr = "����";
                break;
        }
        gameObject.transform.GetChild(2).GetChild(1).GetChild(5).GetComponent<Text>().text = addr;
        if (gameObject.transform.GetChild(2).GetChild(0).gameObject.activeSelf == true)
        {
            gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }

    }
    public void loadData2()
    {
        // �ж��Ƿ��ļ�Ϊ��
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data2");
        if (data2Flag==false)
        {
            return;
        }
        // ���ж��費��Ҫ�л�����
        if (SceneManager.GetActiveScene().name == dfpData2.currentSceneName)
        {
            // 
            // �رն�������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data2");
        }
        else
        {
            // �����
            // �糡����ʱ����б���һ���ļ������ڼ��ص�ʱ���ȡ
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data2";
            string path = Application.dataPath+"/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // ɾ���ļ�
            deleteFile(path + fileName);
            // �����ļ�
            CreateOrOPenFile(path, fileName, saveLoadData);

            // �رն�������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // ��ת����
            SceneManager.LoadScene(dfpData2.currentSceneName);
        }

    }
    public void saveData3()
    {
        // ɾ��ԭ�����ļ�
        string path = Application.dataPath + "/UpdateData/saveData";
        string name = "Data3.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        // TextAsset txt json yml ...
        deleteFile(path + name);
        // ��ֵ�������������
        string data = null;
        string saveTime = DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");
        string dataName = "�浵3";
        data = DataJson(saveTime, dataName);
        // �����ļ���������
        CreateOrOPenFile(path, name, data);
        // ��ʾ�浵�������
        // ������Ҫ����ı�������
        gameObject.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>().text = dataName;
        gameObject.transform.GetChild(3).GetChild(1).GetChild(3).GetComponent<Text>().text = saveTime;
        string addr = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "fishingMap":
                addr = "�����";
                break;
            case "town":
                addr = "����";
                break;
        }
        gameObject.transform.GetChild(3).GetChild(1).GetChild(5).GetComponent<Text>().text = addr;
        if (gameObject.transform.GetChild(3).GetChild(0).gameObject.activeSelf == true)
        {
            gameObject.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
        }

    }
    public void loadData3()
    {
        // �ж��Ƿ��ļ�Ϊ��
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data3");
        if (data3Flag == false)
        {
            return;
        }
        // ���ж��費��Ҫ�л�����
        if (SceneManager.GetActiveScene().name == dfpData3.currentSceneName)
        {
            // 
            // �رն�������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data3");
        }
        else
        {
            // �����
            // �糡����ʱ����б���һ���ļ������ڼ��ص�ʱ���ȡ
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data3";
            string path =Application.dataPath+ "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // ɾ���ļ�
            deleteFile(path + fileName);
            // �����ļ�
            CreateOrOPenFile(path, fileName, saveLoadData);

            // �رն�������
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // �ر�uiϵͳ
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // ��ת����
            SceneManager.LoadScene(dfpData3.currentSceneName);
        }
    }

    // ɾ���ļ�
    public void deleteFile(string path)
    {
        // path ����·��+���嵽�ļ���ʽ���ļ�����:   test.txt
        File.Delete(path);
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

    //

    // ��ȡ��Ҫ�浵������
    public string DataJson(string saveTime, string dataName)
    {
        string data;
        DataForPlayer dataForPlayer = new DataForPlayer();
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dataForPlayer.dataSaveTime = saveTime;
        dataForPlayer.dataName = dataName;
        dataForPlayer.currentSceneName = SceneManager.GetActiveScene().name;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        dataForPlayer.baseSpeed = playerController.baseSpeed;
        dataForPlayer.speed = playerController.speed;
        dataForPlayer.playerX = player.position.x;
        dataForPlayer.playerY = player.position.y;
        dataForPlayer.playerZ = player.position.z;
        dataForPlayer.playerState = playerController.playerState;
        dataForPlayer.level = playerController.level;
        dataForPlayer.fishingLevel = playerController.fishingLevel;
        dataForPlayer.farmingLevel = playerController.farmingLevel;
        dataForPlayer.fieldLevel = playerController.fieldLevel;
        dataForPlayer.digLevel = playerController.digLevel;
        dataForPlayer.currentPlayerExp = playerController.currentPlayerExp;
        dataForPlayer.currentHunger = playerController.currentHunger;
        dataForPlayer.currentHealth = playerController.currentHealth;
        dataForPlayer.currentFishExp = playerController.currentFishExp;
        dataForPlayer.currentFieldExp = playerController.currentFieldExp;
        dataForPlayer.currentFarmExp = playerController.currentFarmExp;
        dataForPlayer.currentEndurance = playerController.currentEndurance;
        dataForPlayer.currentDigExp = playerController.currentDigExp;
        data = JsonUtility.ToJson(dataForPlayer) + "\n\n";
        TimeSystemContoller timeSystemContoller = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        DataForTimeSystem dataForTimeSystem = new DataForTimeSystem();
        dataForTimeSystem.showMinute = timeSystemContoller.showMinute;
        dataForTimeSystem.showSeconds = timeSystemContoller.showSeconds;
        dataForTimeSystem.seasonTime = timeSystemContoller.seasonTime;
        data += JsonUtility.ToJson(dataForTimeSystem);
        return data;
    }

    // ����������Ϸ����
    public void loadGameData(string name)
    {
            // ��ȡ��Ҫ���ش浵���ļ���
            //string path = "UpdateData/saveData/";
            //string fileName = name;
        // ˢ���ļ�
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        // �ж����ĸ��浵
        string data = "";
        if (name == "Data1") {
            data = data1Text;
        } else if (name == "Data2") {
            data = data2Text;
        } else if (name == "Data3") {
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

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
    // 判断是读档还是存档;1存档、2读档
    public int dataType = 1;
    // 初始化的时候，判断是否是从读档中加载
    public bool needLoadData = false;
    // 判断加载哪个存档
    public string needLoadDataFileName;
    // 玩家数据
    public DataForPlayer dfpData1;
    // 时间系统数据
    public DataForTimeSystem dftsData1;
    // 玩家数据
    public DataForPlayer dfpData2;
    // 时间系统数据
    public DataForTimeSystem dftsData2;
    // 玩家数据
    public DataForPlayer dfpData3;
    // 时间系统数据
    public DataForTimeSystem dftsData3;
    // 判断是否存在存档123
    bool data1Flag = false;
    bool data2Flag = false;
    bool data3Flag = false;
    // 存档123的路径
    string data1path;
    string data2path;
    string data3path;
    // 接收存读123的数据
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
    // 关闭存读档界面、打开系统界面
    public void ChangeUI()
    {
        // 按下Esc,存档界面打开的
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.activeSelf == true)
        {
            //打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(true);
            // 关闭存读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        if (dataType == 1)
        {
            // 换标题
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "存档";
            // 获取到data的所有按钮
            Button data1Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>();
            Button data2Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Button>();
            Button data3Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Button>();
            // 先清除所有绑定方法
            data1Button.onClick.RemoveAllListeners();
            data2Button.onClick.RemoveAllListeners();
            data3Button.onClick.RemoveAllListeners();
            // 绑定方法
            data1Button.onClick.AddListener(delegate { saveData1(); });
            data2Button.onClick.AddListener(delegate { saveData2(); });
            data3Button.onClick.AddListener(delegate { saveData3(); });

        }
        else
        {
            // 换标题
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "读档";
            // 获取到data的所有按钮
            Button data1Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>();
            Button data2Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Button>();
            Button data3Button = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Button>();
            // 先清除所有绑定方法
            data1Button.onClick.RemoveAllListeners();
            data2Button.onClick.RemoveAllListeners();
            data3Button.onClick.RemoveAllListeners();
            // 绑定方法
            data1Button.onClick.AddListener(delegate { loadData1(); });
            data2Button.onClick.AddListener(delegate { loadData2(); });
            data3Button.onClick.AddListener(delegate { loadData3(); });
        }
        // 获取存档数据文件
        DirectoryInfo saveDataFiles = new DirectoryInfo(Application.dataPath + "/UpdateData/saveData/");
        saveDataFiles.Refresh();
        FileInfo[] files = saveDataFiles.GetFiles("*",SearchOption.AllDirectories);
        /*        TextAsset data1 = null;
                TextAsset data2 = null;
                TextAsset data3 = null;*/

        //FileInfo data1 = new FileInfo();
        if (files.Length!=0) {
            for (int i = 0; i< files.Length;i++) {
                // 排除meta格式文件
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
        // 刷新文件夹
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        // 判断是否有对应文件，显示不同的画面
        if (data1Flag == false)
        {
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            // 获取数据
            string[] fileContent = data1Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData1 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData1 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // 赋值
            gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData1.dataName;
            gameObject.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData1.dataSaveTime;
            string addrName = null;
            switch (dfpData1.currentSceneName)
            {
                case "fishingMap":
                    addrName = "钓鱼湖";
                    break;
                case "town":
                    addrName = "城镇";
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
            // 获取数据进行赋值
            string[] fileContent = data2Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData2 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData2 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData2.dataName;
            gameObject.transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData2.dataSaveTime;
            string addr = null;
            switch (dfpData2.currentSceneName)
            {
                case "fishingMap":
                    addr = "钓鱼湖";
                    break;
                case "town":
                    addr = "城镇";
                    break;
            }
            gameObject.transform.GetChild(2).GetChild(1).GetChild(5).GetComponent<Text>().text = addr;
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        if (data3Flag == false)
        {
            print("无存档3");
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            print("有存档3");
            // 获取数据进行赋值
            string[] fileContent = data3Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData3 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData3 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            gameObject.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData3.dataName;
            gameObject.transform.GetChild(3).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData3.dataSaveTime;
            string addr = null;
            switch (dfpData3.currentSceneName)
            {
                case "fishingMap":
                    addr = "钓鱼湖";
                    break;
                case "town":
                    addr = "城镇";
                    break;
            }
            gameObject.transform.GetChild(3).GetChild(1).GetChild(5).GetComponent<Text>().text = addr;
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
        }
    }

    // 点击不同的数据进行不同操作
    public void saveData1()
    {
        
        string path = Application.dataPath+"/UpdateData/saveData/";
        string fileName = "Data1.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        // 删除文件
        deleteFile(path + fileName);
        // 赋值数据
        string saveTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        string dataName = "存档1";
        string data = null;
        data = DataJson(saveTime, dataName);
        // 创建文件
        CreateOrOPenFile(path, fileName, data);
        // 刷新显示内容
        // 存档名称
        string saveDataName = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "fishingMap":
                saveDataName = "钓鱼湖";
                break;
            case "town":
                saveDataName = "城镇";
                break;
        }
        // 设置文本显示
        gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = dataName;
        gameObject.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = saveTime;
        gameObject.transform.GetChild(1).GetChild(1).GetChild(5).GetComponent<Text>().text = saveDataName;
        // 判断null是否显示打开
        if (gameObject.transform.GetChild(1).GetChild(0).gameObject.activeSelf == true)
        {
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
    }
    public void loadData1()
    {
        print("读档1");
        // 判断是否文件为空
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data1");
        
        if (data1Flag == false) {
            return;
        }

        // 先判断需不需要切换场景
        if (SceneManager.GetActiveScene().name == dfpData1.currentSceneName)
        {
            // 
            // 关闭读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data1");
        }
        else {
            // 不相等
            // 跨场景的时候进行保存一个文件，用于加载的时候读取
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data1";
            string path = Application.dataPath+"/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // 创建文件
            CreateOrOPenFile(path, fileName, saveLoadData);
            // 关闭读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // 跳转场景
            SceneManager.LoadScene(dfpData1.currentSceneName);
        }
    }
    public void saveData2()
    {
        // 删除原来的文件
        string path = Application.dataPath+"/UpdateData/saveData";
        string name = "Data2.txt";
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        // TextAsset txt json yml ...
        deleteFile(path + name);
        // 赋值给到保存的数据
        string data = null;
        string saveTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        string dataName = "存档2";
        data = DataJson(saveTime, dataName);
        // 数据文件创建下来
        CreateOrOPenFile(path, name, data);
        // 显示存档后的内容
        // 查找需要变更文本的物体
        gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = dataName;
        gameObject.transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>().text = saveTime;
        string addr = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "fishingMap":
                addr = "钓鱼湖";
                break;
            case "town":
                addr = "城镇";
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
        // 判断是否文件为空
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data2");
        if (data2Flag==false)
        {
            return;
        }
        // 先判断需不需要切换场景
        if (SceneManager.GetActiveScene().name == dfpData2.currentSceneName)
        {
            // 
            // 关闭读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data2");
        }
        else
        {
            // 不相等
            // 跨场景的时候进行保存一个文件，用于加载的时候读取
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data2";
            string path = Application.dataPath+"/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // 删除文件
            deleteFile(path + fileName);
            // 创建文件
            CreateOrOPenFile(path, fileName, saveLoadData);

            // 关闭读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // 跳转场景
            SceneManager.LoadScene(dfpData2.currentSceneName);
        }

    }
    public void saveData3()
    {
        // 删除原来的文件
        string path = Application.dataPath + "/UpdateData/saveData";
        string name = "Data3.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        // TextAsset txt json yml ...
        deleteFile(path + name);
        // 赋值给到保存的数据
        string data = null;
        string saveTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        string dataName = "存档3";
        data = DataJson(saveTime, dataName);
        // 数据文件创建下来
        CreateOrOPenFile(path, name, data);
        // 显示存档后的内容
        // 查找需要变更文本的物体
        gameObject.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>().text = dataName;
        gameObject.transform.GetChild(3).GetChild(1).GetChild(3).GetComponent<Text>().text = saveTime;
        string addr = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "fishingMap":
                addr = "钓鱼湖";
                break;
            case "town":
                addr = "城镇";
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
        // 判断是否文件为空
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data3");
        if (data3Flag == false)
        {
            return;
        }
        // 先判断需不需要切换场景
        if (SceneManager.GetActiveScene().name == dfpData3.currentSceneName)
        {
            // 
            // 关闭读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data3");
        }
        else
        {
            // 不相等
            // 跨场景的时候进行保存一个文件，用于加载的时候读取
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data3";
            string path =Application.dataPath+ "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // 删除文件
            deleteFile(path + fileName);
            // 创建文件
            CreateOrOPenFile(path, fileName, saveLoadData);

            // 关闭读档界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // 跳转场景
            SceneManager.LoadScene(dfpData3.currentSceneName);
        }
    }

    // 删除文件
    public void deleteFile(string path)
    {
        // path 基础路径+具体到文件格式的文件名称:   test.txt
        File.Delete(path);
    }
    // 创建文件
    public void CreateOrOPenFile(string path, string name, string info)
    {          //路径、文件名、写入内容
        // 打开文件流
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //直接重新写入，如果要在原文件后面追加内容，应用fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }

    //

    // 获取需要存档的数据
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

    // 读档加载游戏数据
    public void loadGameData(string name)
    {
            // 获取需要加载存档的文件名
            //string path = "UpdateData/saveData/";
            //string fileName = name;
        // 刷新文件
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
        // 判断是哪个存档
        string data = "";
        if (name == "Data1") {
            data = data1Text;
        } else if (name == "Data2") {
            data = data2Text;
        } else if (name == "Data3") {
            data = data3Text;
        }
        //定义变量接收存档数据
        //TextAsset data = Resources.Load<TextAsset>(path + fileName);
            string[] fileContent = data.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            DataForPlayer dataForPlayer = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            DataForTimeSystem dataForTimeSystem = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // 赋值玩家和时间系统属性
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
            // 设置玩家位置
            Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerstation = new Vector3();
            playerstation.x = dataForPlayer.playerX;
            playerstation.y = dataForPlayer.playerY;
            playerstation.z = dataForPlayer.playerZ;
            playerPosition.position = playerstation;
            // 设置时间系统数据
            TimeSystemContoller timeSystemContoller = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
            timeSystemContoller.seasonTime = dataForTimeSystem.seasonTime;
            timeSystemContoller.showMinute = dataForTimeSystem.showMinute;
            timeSystemContoller.showSeconds = dataForTimeSystem.showSeconds;
    }
}

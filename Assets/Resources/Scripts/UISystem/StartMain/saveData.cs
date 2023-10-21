using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class saveData : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        // 检测是否按了Esc，按了之后关闭存档界面，并显示主界面
        changePanelShow();

    }

    private void OnEnable()
    {
        // 获取存档数据文件
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
                // 排除meta格式文件
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
        // 根据有无文件进行显示和隐藏
        if (data1Flag == false)
        {
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        else {
            // 获取数据
            string[] fileContent = data1Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData1 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData1 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // 赋值
            gameObject.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData1.dataName;
            gameObject.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData1.dataSaveTime;
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
            // 获取数据
            string[] fileContent = data2Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData2 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData2 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // 赋值
            gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData2.dataName;
            gameObject.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData2.dataSaveTime;
            string addrName = null;
            switch (dfpData2.currentSceneName)
            {
                case "fishingMap":
                    addrName = "钓鱼湖";
                    break;
                case "town":
                    addrName = "城镇";
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
            // 获取数据
            string[] fileContent = data3Text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
            dfpData3 = JsonUtility.FromJson<DataForPlayer>(fileContent[0]);
            dftsData3 = JsonUtility.FromJson<DataForTimeSystem>(fileContent[1]);
            // 赋值
            gameObject.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = dfpData3.dataName;
            gameObject.transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<Text>().text = dfpData3.dataSaveTime;
            string addrName = null;
            switch (dfpData3.currentSceneName)
            {
                case "fishingMap":
                    addrName = "钓鱼湖";
                    break;
                case "town":
                    addrName = "城镇";
                    break;
            }
            gameObject.transform.GetChild(2).GetChild(1).GetChild(5).GetComponent<Text>().text = addrName;
            gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }

    }

    // 关闭、显示界面
    public void changePanelShow() {
        if (Input.GetKey(KeyCode.Escape) && GameObject.FindGameObjectWithTag("SaveDataUI").gameObject.activeSelf == true) {
            GameObject saveDataMenu = GameObject.FindGameObjectWithTag("SaveDataUI").gameObject;
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
            saveDataMenu.transform.GetChild(0).gameObject.SetActive(false);
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);

        }
    }

    // 加载存档1
    public void LoadSaveData1() {

        print("读档1");

        if (data1Flag == false)
        {
            return;
        }

        // 先判断需不需要切换场景
        print(dfpData1.currentSceneName);
        if (SceneManager.GetActiveScene().name == dfpData1.currentSceneName)
        {
            // 
            // 关闭读档界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
           // loadGameData("Data1");
        }
        else
        {
            // 不相等
            // 跨场景的时候进行保存一个文件，用于加载的时候读取
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data1";
            string path = Application.dataPath + "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // 创建文件
            CreateOrOPenFile(path, fileName, saveLoadData);
            // 关闭读档界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // 跳转场景
            
            SceneManager.LoadScene(dfpData1.currentSceneName);
        }
    }

    // 加载存档2
    public void LoadSaveData2()
    {

        // 判断是否文件为空
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data2");
        if (data2Flag == false)
        {
            return;
        }
        print(dfpData2.currentSceneName);
        // 先判断需不需要切换场景
        if (SceneManager.GetActiveScene().name == dfpData2.currentSceneName)
        {
            // 
            // 关闭读档界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            loadGameData("Data2");
        }
        else
        {
            // 不相等
            // 跨场景的时候进行保存一个文件，用于加载的时候读取
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data2";
            string path = Application.dataPath + "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // 删除文件
            //deleteFile(path + fileName);
            // 创建文件
            CreateOrOPenFile(path, fileName, saveLoadData);

            // 关闭读档界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // 跳转场景
            
            SceneManager.LoadScene(dfpData2.currentSceneName);
        }
    }

    // 加载存档3
    public void LoadSaveData3()
    {

        // 判断是否文件为空
        //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/Data3");
        if (data3Flag == false)
        {
            return;
        }
        print(dfpData3.currentSceneName);
        // 先判断需不需要切换场景
        if (SceneManager.GetActiveScene().name == dfpData3.currentSceneName)
        {
            // 
            // 关闭读档界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            //loadGameData("Data3");
        }
        else
        {
            // 不相等
            // 跨场景的时候进行保存一个文件，用于加载的时候读取
            NeedLoadData needLoadData = new NeedLoadData();
            needLoadData.isNeedLoadData = true;
            needLoadData.needLoadFileName = "Data3";
            string path = Application.dataPath + "/UpdateData/saveData";
            string fileName = "needLoadData.txt";
            string saveLoadData = JsonUtility.ToJson(needLoadData);
            // 删除文件
            //deleteFile(path + fileName);
            // 创建文件
            CreateOrOPenFile(path, fileName, saveLoadData);

            // 关闭读档界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            // 关闭ui系统
            //GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
            // 跳转场景
            
            SceneManager.LoadScene(dfpData3.currentSceneName);
        }
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
    // 读档加载游戏数据
    public void loadGameData(string name)
    {
        // 获取需要加载存档的文件名
        //string path = "UpdateData/saveData/";
        //string fileName = name;
        // 刷新文件
        // 判断是哪个存档
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

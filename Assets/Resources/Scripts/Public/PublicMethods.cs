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
    // 记录需要调用的公共方法和公共属性
    // 跳转路径
    public static string UpdateDataPath = Application.dataPath + "/UpdateData";
    public static string moveScenePath = Application.dataPath + "/UpdateData/moveScene";
    public static string saveDataPath = Application.dataPath + "/UpdateData/saveData";
    // 当跳转场景的时候加载赋值数据时的路径和文件名称
    public static string moveSceneFileName = "moveSceneData.txt";
    // 判断是否跳转场景和读档时的文件
    public static string needDataFileName = "needLoadData.txt";
    ///存档----------------------
    // 存档1文件
    public static string data1FileName = "Data1.txt";
    // 存档2文件
    public static string data2FileName = "Data2.txt";
    // 存档3文件
    public static string data3FileName = "Data3.txt";
    ///--------------------------

    // 将是否跳转和保存到文件
    public static void NeedLoadDataSave(bool isChangeScene, string beforeChangeSceneName, bool isNeedLoadData, string needLoadFileName)
    {
        NeedLoadData needLoadData = new NeedLoadData();
        needLoadData.isChangeScene = isChangeScene;
        needLoadData.beforeChangeSceneName = beforeChangeSceneName;
        needLoadData.isNeedLoadData = isNeedLoadData;
        needLoadData.needLoadFileName = needLoadFileName;
        createFile(saveDataPath, needDataFileName, JsonUtility.ToJson(needLoadData));
    }
    // 创建文件
    public static void createFile(string path, string name, string info)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //直接重新写入，如果要在原文件后面追加内容，应用fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
    // 删除文件
    public static void deleteOneFile(string path)
    {
        File.Delete(path);
    }
    // 读取文件 // txt  json 
    public static string ReadFile(string filePath)
    {
        // 接收文件数据
        string finalStr = "";
        string[] strs = File.ReadAllLines(filePath);//读取文件的所有行，并将数据读取到定义好的字符数组strs中，一行存一个单元
        for (int i = 0; i < strs.Length; i++)
        {
            finalStr += strs[i];//读取每一行，并连起来
            finalStr += "\n";//每一行末尾换行
        }
        return finalStr;
    }
    // 跳转场景的时候保存需要加载的数据,将最后的json返回出去
    public static string ChangeSceneDataSave() {
        ///
        /// 玩家数据
        /// 时间系统数据
        ///
        string loadJsonData;
        // 获取玩家本身属性数据写入(包括玩家所在位置)
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
        // 获取时间属性数据写入
        TimeSystemContoller timeSys = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        timeSystemData time = new timeSystemData();
        time.showMinute = timeSys.showMinute;
        time.showSeconds = timeSys.showSeconds;
        time.seasonTime = timeSys.seasonTime;
        loadJsonData += JsonUtility.ToJson(time);
        return loadJsonData;
    }
    // 初始化地图的时候，赋值需要加载的数据// 读取并获取数据
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
        // 读取通过跳转场景时保存的数据
        TextAsset dataTxt = Resources.Load<TextAsset>("UpdateData/moveScene/moveSceneData");
        if (dataTxt)
        {
            string[] fileContent = dataTxt.text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
                pData = JsonUtility.FromJson<playerData>(fileContent[0]);
                tsData = JsonUtility.FromJson<timeSystemData>(fileContent[1]);

        }
        // 读取判断是否需要跳转场景赋值或读档赋值
        // 判断是否需要加载存档，判断是否有needLoadData文件
        TextAsset needLoadDataFile = Resources.Load<TextAsset>("UpdateData/saveData/needLoadData");
        print(needLoadDataFile.text);
        if (needLoadDataFile)
        {
            // 判断是否需要加载文件
            needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
        }
    }
    // 初始化场景的时候进行初始化物体和进行数据赋值
    public static void initGameObjectForInitMap() {
        // 初始化玩家
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/player/player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Instantiate(playerPrefab);
        }
        // 初始化时间系统
        GameObject timeSystemPrefab = Resources.Load<GameObject>("Prefabs/mainFunc/TimeSystemUI");
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem");
        if (!timeSystem)
        {
            Instantiate(timeSystemPrefab);
        }
        //初始化ui系统
        GameObject uiSystemPrefab = Resources.Load<GameObject>("Prefabs/ui/uiSystem");
        GameObject uiSystem = GameObject.FindGameObjectWithTag("mainUI");
        if (!uiSystem)
        {
            Instantiate(uiSystemPrefab);
        }
        // 初始化对话框
        GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (!dialog)
        {
            Instantiate(dialogPrefab);
        }

    }
    // 初始化场景的时候进行物体的数据赋值(通过跳转场景)
    public static void SetGameObjectDataForChangeScene(playerData pData, timeSystemData tsData) {
        // 赋值玩家属性
        if (pData!=null) {
            Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
            // 赋值玩家属性
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
        // 赋值时间系统属性
        if (tsData != null)
        {
            // 赋值时间系统
            TimeSystemContoller timeSystemContoller = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
            timeSystemContoller.seasonTime = tsData.seasonTime;
            timeSystemContoller.showMinute = tsData.showMinute;
            timeSystemContoller.showSeconds = tsData.showSeconds;
        }
        // 将跳转场景的属性给关闭
        NeedLoadDataSave(false,"",false,"");
    }
    // 初始化场景的时候进行物体的数据赋值(通过读档)
    public static void SetGameObjectDataForLoadData()
    {
        // 判断是否需要加载存档，判断是否有needLoadData文件
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
            // 判断是否需要加载文件
            NeedLoadData needLoadData = JsonUtility.FromJson<NeedLoadData>(needDataText);
            // 获取需要加载存档的文件名
            string fileName = needLoadData.needLoadFileName;
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            //定义变量接收存档数据
            //TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/" + fileName);
            DirectoryInfo loadSaveData = new DirectoryInfo(Application.dataPath+ "UpdateData/saveData");
            loadSaveData.Refresh();
            string saveDataText = ReadFile(saveDataPath+"/"+needLoadData.needLoadFileName+".txt");
            string[] fileContent = saveDataText.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
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
            // 将读档设置为禁止
            needLoadData.isNeedLoadData = false;
            string needLoadDataJson = JsonUtility.ToJson(needLoadData);
            // 创建文件
            createFile(saveDataPath, needDataFileName, needLoadDataJson);
            // 更新文件不读档
            NeedLoadDataSave(false,"",false,"");
        }
    }

}

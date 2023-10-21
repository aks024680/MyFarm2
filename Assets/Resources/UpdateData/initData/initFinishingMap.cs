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
    // 初始化钓鱼湖地图
    // 玩家
    GameObject player;
    // 时间系统
    GameObject timeSystem;
    // ui系统
    GameObject uiSystem;
    // 玩家数据
    playerData pData;
    // 时间系统数据
    timeSystemData tsData;
    // 判断是否需要通过跳转场景/读档进行赋值对象
    NeedLoadData needLoadData;
    // 控制剧情播放
    public bool firstTimeLine = true;
    // 恢复实例化角色的时间
    public float initPlayerTime = 0;

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
            //PublicMethods.initGameObjectForInitMap();
            initItem();
            // 配置项
            SetObjectValue();
            // 加载数据(根据跳转场景还是读档)
            loadData();

    }
    void Start()
    {
        // 将剧情放开
        GameObject firstTimeLineObject = GameObject.FindGameObjectWithTag("firstTimeLine");
        firstTimeLineObject.transform.GetChild(0).gameObject.SetActive(true);
        firstTimeLineObject.transform.GetChild(1).gameObject.SetActive(true);
        firstTimeLineObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // 播放完剧情实例化角色
        if (firstTimeLine == true) {
            initPlayerTime += Time.deltaTime;
            if (initPlayerTime >= 45) {
                // 将剧情关闭
                GameObject firstTimeLineObject = GameObject.FindGameObjectWithTag("firstTimeLine");
                firstTimeLineObject.transform.GetChild(0).gameObject.SetActive(false);
                firstTimeLineObject.transform.GetChild(1).gameObject.SetActive(false);
                firstTimeLineObject.transform.GetChild(2).gameObject.SetActive(false);
                // 实例化角色
                initPlayerAndDataAfterTimeLine();
                // 查找主音乐开启
                GameObject.Find("audioPlay").transform.GetChild(0).gameObject.SetActive(true);
                firstTimeLine = false;
            }
        }
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
        if (needLoadDataFile !=null)
        {
            // 判断是否需要加载文件
            needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
        }
    }
    // 根据跳转场景或读档进行加载数据
    public void loadData() {
        // 判断数据是否为空
        if (needLoadData !=null) {
            // 判断是否是通过跳转场景进入
            if (needLoadData.isChangeScene == true)
            {
                //刷新文件夹
                DirectoryInfo directoryInfo = new DirectoryInfo(PublicMethods.moveScenePath);
                directoryInfo.Refresh();
                PublicMethods.SetGameObjectDataForChangeScene(pData,tsData);
                //SetGameObjectDataForChangeScene(pData, tsData);
                // 赋值玩家坐标
                setPlayerPosition(pData);
            }
            else {
                // 判断是否通过读档进入
                if (needLoadData.isNeedLoadData == true) {
                    //刷新文件夹
                    DirectoryInfo directoryInfo = new DirectoryInfo(PublicMethods.saveDataPath);
                    directoryInfo.Refresh();
                    PublicMethods.SetGameObjectDataForLoadData();
                    //SetGameObjectDataForLoadData();
                }
            }
        }
    }
    // 配置赋值
    public void SetObjectValue() {
        // 设置cinemachine的地图碰撞器
        // 获取时间系统
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem");
        // 获取cineMachine下的脚本
        colliderMapController mapController = GameObject.FindGameObjectWithTag("cineMachine").GetComponent<colliderMapController>();
        if (!mapController.timeSys) {
            mapController.timeSys = timeSystem;
        }

    }
    // 赋值玩家坐标
    public void setPlayerPosition(playerData plData) {
        if (plData!=null) {
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
                    playerPosition.x = 29;
                    playerPosition.y = 2;
                    playerPosition.z = 0;
                    break;
            }
            Player.position = playerPosition;
        }
    }
    // 初始化场景的时候进行物体的数据赋值(通过跳转场景)
    public void SetGameObjectDataForChangeScene(playerData pData, timeSystemData tsData)
    {
        // 赋值玩家属性
        if (pData != null)
        {
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
        NeedLoadDataSave(false, "", false, "");
    }
    // 初始化场景的时候进行物体的数据赋值(通过读档)
    public void SetGameObjectDataForLoadData()
    {
        // 判断是否需要加载存档，判断是否有needLoadData文件
        TextAsset needLoadDataFile = Resources.Load<TextAsset>("UpdateData/saveData" + "/" + "needLoadData");
        if (needLoadDataFile)
        {
            // 判断是否需要加载文件
            NeedLoadData needLoadData = JsonUtility.FromJson<NeedLoadData>(needLoadDataFile.text);
            // 获取需要加载存档的文件名
            string fileName = needLoadData.needLoadFileName;
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
            //定义变量接收存档数据
            TextAsset data = Resources.Load<TextAsset>("UpdateData/saveData/" + fileName);
            string[] fileContent = data.text.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
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
            createFile("Assets/Resources/UpdateData/saveData", "needLoadData.txt", needLoadDataJson);
            // 更新文件不读档
            NeedLoadDataSave(false, "", false, "");
        }
    }
    public static void createFile(string path, string name, string info)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //直接重新写入，如果要在原文件后面追加内容，应用fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
    // 将是否跳转和保存到文件
    public static void NeedLoadDataSave(bool isChangeScene, string beforeChangeSceneName, bool isNeedLoadData, string needLoadFileName)
    {
        NeedLoadData needLoadData = new NeedLoadData();
        needLoadData.isChangeScene = isChangeScene;
        needLoadData.beforeChangeSceneName = beforeChangeSceneName;
        needLoadData.isNeedLoadData = isNeedLoadData;
        needLoadData.needLoadFileName = needLoadFileName;
        createFile("Assets/Resources/UpdateData/saveData", "needLoadData.txt", JsonUtility.ToJson(needLoadData));
    }

    // 在播放完剧情后设置镜头跟随玩家
    public void initPlayerAndDataAfterTimeLine() {
        // 实例化玩家
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/player/player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Instantiate(playerPrefab);
        }
        // 设置摄像机跟随玩家
        Transform playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        CinemachineVirtualCamera cinemachineVirtual = GameObject.FindGameObjectWithTag("cineMachine").GetComponent<CinemachineVirtualCamera>();
        if (!cinemachineVirtual.Follow)
        {
            cinemachineVirtual.Follow = playerTran;
        }
    }

    // 实例化物品
    public void initItem() {

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
}

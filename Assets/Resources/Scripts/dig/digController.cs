using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class digController : MonoBehaviour
{
    // 当前已经挖矿的次数
    public int currentDigCount = 0;
    // 每天可挖矿的次数
    public int maxDigCount = 5;
    // 判断是否在挖矿范围
    public bool isDigArea = false;
    // 控制对话框显示
    public bool showDialog = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDigArea = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 获取对话框
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true)
        {
            isDigArea = false;
        }
        else {
            isDigArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDigArea = false;
    }



    // Update is called once per frame
    void Update()
    {
        // 重置挖矿次数
        reSetDigCount();
        // 挖矿
        diging();
        // 控制对话框显隐
        // 获取对话框
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true)
        {
            showDialog = false;
        }
        else
        {
            showDialog = true;
        }
    }

    // 00:00重置挖矿次数
    public void reSetDigCount() {
        // 获取时间系统
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        if (timeSystem.GetComponent<TimeSystemContoller>().showMinute == 0 && timeSystem.GetComponent<TimeSystemContoller>().showSeconds == 0) {
            // 00:00重置挖矿次数
            currentDigCount = 0;
        }
    }

    // 挖矿
    public void diging() {
        // 判断是否在挖矿区域内
        if (isDigArea == false) {
            return;
        }
        // 判断是否有装备镐子
        // 获取装备栏
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        if (equip.GetComponent<Image>().sprite != null)
        {
            // 判断是否为镐子
            if (equip.GetComponent<Image>().sprite.name == "镐头")
            {
                if (Input.GetKeyDown(KeyCode.E) && isDigArea == true && showDialog == true) {
                    // 判断次数是否用完
                    if (currentDigCount == maxDigCount)
                    {
                        // 获取实例化后的物体
                        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                        // 获取预制体
                        GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
                        // 判断是否有实例化文本框
                        if (!dialogGameObject)
                        {
                            Instantiate(dialog);
                        }
                        // 为防止初始的时候获取不到实例化的物体，重新赋值
                        dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                        dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        // 变更文本
                        string[] textContent;
                        textContent = new string[] { "今日挖矿次数已经用完" };
                        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                    }
                    else {
                        // 挖矿次数加1
                        currentDigCount++;
                        // 获取挖矿掉落物品
                        List<itemsCreations> digList = new List<itemsCreations>();
                        itemsCreations 石头 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/石头");
                        itemsCreations 红宝石 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/红宝石");
                        itemsCreations 绿宝石 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/绿宝石");
                        itemsCreations 蓝宝石 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/蓝宝石");
                        itemsCreations 黄宝石 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/黄宝石");
                        itemsCreations 钻石 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/钻石");
                        digList.Add(石头);
                        digList.Add(红宝石);
                        digList.Add(绿宝石);
                        digList.Add(蓝宝石);
                        digList.Add(黄宝石);
                        digList.Add(钻石);
                        // 随机获取物品
                        System.Random random = new System.Random();
                        // 设定出货概率
                        int rate = random.Next(1,101);
                        // 设定出货物品
                        string itemName = null;
                        if (rate <= 80)
                        {
                            // 石头
                            itemName = "石头";
                        }
                        else if (rate > 80 && rate <= 85)
                        {
                            // 红宝石
                            itemName = "红宝石";
                        }
                        else if (rate > 85 && rate <= 90)
                        {
                            // 绿宝石
                            itemName = "绿宝石";
                        }
                        else if (rate > 90 && rate <= 95)
                        {
                            // 黄宝石
                            itemName = "黄宝石";
                        }
                        else if (rate > 95 && rate <= 99)
                        {
                            // 蓝宝石
                            itemName = "蓝宝石";
                        }
                        else {
                            // 钻石
                            itemName = "钻石";
                        }
                        // 
                        // 将对应的物品实例化
                        // 设置随机生成的位置

                        float x = random.Next(15, 20);
                        float y = random.Next(8,11);
                        GameObject item = Resources.Load<GameObject>("Prefabs/bag/items/dig/item/" + itemName);
                        Instantiate(item, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0));
                    }
                }
            }
            else {
                if (Input.GetKeyDown(KeyCode.E) && isDigArea == true && showDialog == true) {
                    // 获取实例化后的物体
                    GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                    // 获取预制体
                    GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
                    // 判断是否有实例化文本框
                    if (!dialogGameObject)
                    {
                        Instantiate(dialog);
                    }
                    // 为防止初始的时候获取不到实例化的物体，重新赋值
                    dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    // 变更文本
                    string[] textContent;
                    textContent = new string[] { "你未装备镐子" };
                    dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                }
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.E) && isDigArea == true && showDialog == true) {
                // 获取实例化后的物体
                GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                // 获取预制体
                GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
                // 判断是否有实例化文本框
                if (!dialogGameObject)
                {
                    Instantiate(dialog);
                }
                // 为防止初始的时候获取不到实例化的物体，重新赋值
                dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                // 变更文本
                string[] textContent;
                textContent = new string[] { "你未装备镐子" };
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            }
        }

    }
}

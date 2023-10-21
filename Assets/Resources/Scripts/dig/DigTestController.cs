using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigTestController : MonoBehaviour
{
    // Start is called before the first frame update
    // 当天已经挖矿的次数
    public int currentCount = 0;
    // 每天可以挖矿多少
    public int maxCount = 5;
    // 判断是否在挖矿区
    public bool isInDig = false;
    // 控制对话框显隐
    public bool showDialog = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 更新挖矿次数
        reSetDigCount();
        // 挖矿
        digging();
        // 控制对话框显隐
        DialogControll();
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInDig = true;
    }
    // 
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInDig = false;
    }
    // 每天00:00恢复挖矿次数
    public void reSetDigCount() {
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        if (timeSystem.GetComponent<TimeSystemContoller>().showMinute == 0 && timeSystem.GetComponent<TimeSystemContoller>().showSeconds == 0) {
            currentCount = 0;
        }
    }
    // 
    public void DialogControll() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).GetChild(0).gameObject;
        if (dialog.activeSelf == true)
        {
            showDialog = false;
        }
        else {
            showDialog = true;
        }

    }
    // 挖矿的方法
    public void digging() {
        if (Input.GetKeyDown(KeyCode.E)&& isInDig == true && showDialog == true) {
            // 是否有装备物品
            GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
            // 判断是否可以挖矿
            if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "镐头")
            {
                print(111);
                // 获取对话框
                GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).gameObject;
                dialog.GetComponent<DialogPrefab>().message = new string[] {"你未装备镐头!" };
                dialog.transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                // 挖矿
                if (currentCount == maxCount)
                {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).gameObject;
                    dialog.GetComponent<DialogPrefab>().message = new string[] { "你当日挖矿次数已用尽!" };
                    dialog.transform.GetChild(0).gameObject.SetActive(true);
                }
                else {
                    currentCount++;
                    // 物品数据
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
                    // 随机抽取物品
                    System.Random random = new System.Random();
                    // 设定出货概率
                    int rate = random.Next(1, 101);
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
                    else
                    {
                        // 钻石
                        itemName = "钻石";
                    }
                    // 物品实例化

                    float x = random.Next(15, 20);
                    float y = random.Next(8, 11);
                    //Prefabs/bag/items/dig/item/绿宝石
                    GameObject item = Resources.Load<GameObject>("Prefabs/bag/items/dig/item/" + itemName);
                    Instantiate(item, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0));

                }
            }
        }
    }
}

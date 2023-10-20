using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fieldTestController : MonoBehaviour
{
    // 判断是否在种植区
    public bool isInField = false;
    // 判断是否对话框有显示
    public bool showDialog = true;
    // 判断是否已经种植
    public bool isFielding = false;
    // 种植物
    public string name;
    // 生长周期
    public int circleDay;
    // 剩余成熟时间
    public int leaveTime;
    public float totalTime = 0;
    // 种植分钟
    public int minute = 0;
    // 种植的秒钟
    public int second = 0;
    // 生长状态（切换图片）种子期、幼苗期、成熟期、已采摘===>0,1,2,3（不浇水不松土停止生长）
    public int circleStatus = -1;
    // 每天浇水次数
    public int waterCount = 0;
    // 每天松土次数
    public int landCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 对话框显隐
        DialogShow();
        // 种植
        field();
        // 更新种植数据
        updateFiledData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInField = true;
        // 根据是否种植显示面板
        if (isFielding == false)
        {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInField = true;
        // 根据是否种植显示面板
        if (isFielding == false)
        {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInField = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    // 控制对话框显隐
    public void DialogShow() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).GetChild(0).gameObject;
        if (dialog.activeSelf == false)
        {
            showDialog = true;
        }
        else {
            showDialog = false;
        }
    }

    // 种植
    public void field() {
        // 按下按键，在种植区、对话框未显示
        if (Input.GetKeyDown(KeyCode.E)&& isInField == true && showDialog == true) {
            // 是否种植
            if (isFielding == false)
            {
                // 有装备种子
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "种子")
                {
                    // 获取对话框预制体
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你未装备种子" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    // 处理背包种子
                    // 根据背包种子数量去减少还是销毁
                    // 背包ui的种子处理
                    // 状态ui装备的图片处理
                    // 装备状态设置为未装备
                    // 将种子数量减一// 
                    bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                    for (int i = 0; i < playerBag.bag.Count; i++)
                    {
                        if (playerBag.bag[i].name == "种子")
                        {
                            // 判断数量是否只有一个了
                            if (playerBag.bag[i].itemNumber <= 1)
                            {
                                // 只有一个销毁种子物品
                                playerBag.bag.Remove(playerBag.bag[i]);
                                // 销毁ui界面种子物品
                                // 获取背包ui
                                GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
                                // 获取背包UI父级物体下的网格子物体
                                GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
                                // 遍历
                                for (int j = 0; j < bagGrid.transform.childCount; j++)
                                {
                                    if (equip.GetComponent<Image>().sprite == bagGrid.transform.GetChild(j).GetComponent<Image>().sprite)
                                    {
                                        //销毁ui物品
                                        Destroy(bagGrid.transform.GetChild(j));
                                    }
                                }
                                // 设置装备图片为空
                                equip.GetComponent<Image>().sprite = null;
                                //装备标识未未装备状态
                                GridPrefab.clickItemIndex = -1;
                                // 刷新背包
                                // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
                                addToBag.RefreshItem();

                            }
                            else
                            {
                                // 背包种子数据数量减一
                                playerBag.bag[i].itemNumber -= 1;
                            }
                        }
                    }

                    // 获取物品进行种植
                    // 进行种植
                    System.Random random = new System.Random();
                    // 随机进行种植
                    string itemName = "";
                    // 获取随机数
                    int index = random.Next(0, 3);
                    // 
                    List<itemsCreations> seeds = new List<itemsCreations>();
                    itemsCreations 向日葵 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/成熟向日葵");
                    itemsCreations 胡萝卜 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/成熟胡萝卜");
                    itemsCreations 草莓 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/成熟草莓");
                    seeds.Add(向日葵);
                    seeds.Add(胡萝卜);
                    seeds.Add(草莓);
                    switch (index)
                    {
                        case 0:
                            itemName = "成熟向日葵";
                            break;
                        case 1:
                            itemName = "成熟胡萝卜";
                            break;
                        case 2:
                            itemName = "成熟草莓";
                            break;
                    }
                    // 把数据进行更新
                    // 将种植状态变更为已种植
                    isFielding = true;
                    // 提示信息
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你种植了" + itemName.Replace("成熟", "") };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    // 更新种子种植后的信息
                    name = itemName.Replace("成熟", "");
                    // 生长周期/天
                    circleDay = 7;
                    // 剩余时间
                    leaveTime = 7;
                    // 种植时间
                    // 获取时间系统的分和秒
                    TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
                    minute = timeSystem.showMinute;
                    second = timeSystem.showSeconds;
                    // 更新图片
                    string updateImageName = "";
                    switch (itemName)
                    {
                        case "成熟向日葵":
                            updateImageName = "向日葵";
                            break;
                        case "成熟胡萝卜":
                            updateImageName = "胡萝卜";
                            break;
                        case "成熟草莓":
                            updateImageName = "草莓";
                            break;
                    }
                    Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/" + updateImageName);
                    // 变更生长状态
                    circleStatus = 0;
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[circleStatus];
                }
            }
            else {
                // 浇水松土
                waterAndLand();
            }
        }
    }

    // 浇水松土
    public void waterAndLand() {
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        if (equip.GetComponent<Image>().sprite == null && equip.GetComponent<Image>().sprite.name != "锄头" && equip.GetComponent<Image>().sprite.name.Contains("水壶"))
        {
            // 获取对话框预制体
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
            dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "没有装备水壶或锄头" };
            dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else {
            // 
            if (equip.GetComponent<Image>().sprite.name == "锄头") {
                if (landCount == 1)
                {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "今日已松土" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    landCount++;
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "松土成功!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }

            if (equip.GetComponent<Image>().sprite.name.Contains("水壶"))
            {
                if (waterCount == 1)
                {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "今日已浇水" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    waterCount++;
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "浇水成功!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    // 更新数据
    public void updateFiledData() {
        if (isFielding == false) {
            return;
        }
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        // 剩余时间和生长状态
        if (timeSystem.showMinute == 0 && timeSystem.showSeconds == 0)
        {
            if (leaveTime == 0)
            {
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // 采摘期
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "已成熟";
                    circleStatus = 3;
                }
                else {
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = System.Math.Abs(timeSystem.showMinute - minute) + "小时";
                }
            }
            else {
                totalTime += Time.deltaTime;
                if (totalTime >=0.7) {
                    leaveTime -= 1;
                    totalTime = 0;
                }
                // 更新剩余时间
                gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = leaveTime + "天";
                // 更新生长状态
                switch (leaveTime)
                {
                    case 4:
                        // 幼苗期
                        circleStatus = 1;
                        break;
                    case 2:
                        // 成熟期
                        circleStatus = 2;
                        break;
                }

            }
        }
        else {
            if (leaveTime == 0)
            {
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // 采摘期
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "已成熟";
                    circleStatus = 3;
                }
                else
                {
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = System.Math.Abs(timeSystem.showMinute - minute) + "小时";
                }
            }
            else
            {
                // 更新剩余时间
                gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = leaveTime + "天";
                // 更新生长状态
                switch (leaveTime)
                {
                    case 4:
                        // 幼苗期
                        circleStatus = 1;
                        break;
                    case 2:
                        // 成熟期
                        circleStatus = 2;
                        break;
                }

            }
        }

        // 
        // 更新名称
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = name;
        // 更新生长周期
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(7).GetComponent<Text>().text = circleDay + "天";
        // 更新浇水和松土次数
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = waterCount + "/1";
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(5).GetComponent<Text>().text = landCount + "/1";
        Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/" + name);
        // 更新图片
        switch (circleStatus)
        {
            case 0:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[0];
                break;
            case 1:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[1];
                break;
            case 2:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[2];
                break;
            case 3:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[3];
                break;
        }
    }
    
}

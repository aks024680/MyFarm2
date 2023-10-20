using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fieldController : MonoBehaviour
{
    // Start is called before the first frame update
    // 判断是否在种植区
    public bool inFieldArea = false;
    // 判断是否正在种植
    public bool isField = false;
    // 文本框显示控制
    public bool showDialog = true;
    // 种植作物名称
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
    public int circleStatus=-1;
    // 每天浇水次数
    public int waterCount = 0;
    // 每天松土次数
    public int landCount = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inFieldArea == true) {
            // 种植
            Fielding();
        }
        // 获取对话框预制体
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == false)
        {
            showDialog = true;
        }
        else {
            showDialog = false;
        }
        // 更新种植状态
        updateFieldData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isField == false)
        {
            // 未种植状态，显示空物体界面
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            // 种植状态显示已种植界面
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        inFieldArea = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isField == false)
        {
            // 未种植状态，显示空物体界面
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            // 种植状态显示已种植界面
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        inFieldArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        inFieldArea = false;
    }

    // 编写方法进行种植和操作
    public void Fielding() {
        // 获取对话框预制体
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        // 判断是否未种植状态
        if (isField == false)
        {
            // 未种植状态，判断是否按E并且对话框是关闭的
            if (Input.GetKeyDown(KeyCode.E)&& showDialog==true) {
                // 判断是否装备种子
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                if (equip.GetComponent<Image>().sprite == null)
                {
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你未装备种子" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    // 判断是否装备的是种子
                    if (equip.GetComponent<Image>().sprite.name == "种子")
                    {
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
                        switch (index) {
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

                        // 将种子数量减一
                        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                        for (int i = 0;i<playerBag.bag.Count;i++) {
                            if (playerBag.bag[i].name == "种子") {
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
                                else {
                                    // 背包种子数据数量减一
                                    playerBag.bag[i].itemNumber -= 1;
                                }
                            }
                        }

                        // 将种植状态变更为已种植
                        isField = true;
                        // 提示信息
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你种植了"+itemName.Replace("成熟","") };
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
                        switch (itemName) {
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
                        Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/"+updateImageName);
                        // 变更生长状态
                        circleStatus = 0;
                        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[circleStatus];
                    }
                    else {
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你装备的物品不是种子" };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }
        else {
            // 浇水施肥
            TakeCareOfSeed();
        }
    }

    // 浇水施肥
    public void TakeCareOfSeed() {
        //判断按下按键并且是种植状态并且是对话框为关闭状态
        if (Input.GetKeyDown(KeyCode.E) && isField == true && showDialog == true) {
            // 判断是否成熟
            if (circleStatus == 3)
            {
                // 进行获取物品
                bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                // 判断背包是否已经有该物品
                int itemCount = 0;
                for (int i = 0; i<playerBag.bag.Count;i++) {
                    if (playerBag.bag[i].itemName == "成熟"+name) {
                        itemCount++;
                    }
                }
                // 获取需要加入玩家背包的物品
                itemsCreations item = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/"+"成熟"+name);
                if (itemCount == 0)
                {
                    playerBag.bag.Add(item);
                }
                else {
                    for (int i = 0; i < playerBag.bag.Count; i++)
                    {
                        if (playerBag.bag[i].itemName == "成熟" + name)
                        {
                            if (playerBag.bag[i].itemNumber == 10)
                            {
                                playerBag.bag.Add(item);
                            }
                            else {
                                playerBag.bag[i].itemNumber++;
                            }
                        }
                    }
                }
                // 刷新背包
                addToBag.RefreshItem();
                // 清空图片
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                // 变更为未种植状态
                isField = false;
                circleStatus = -1;
                // 提示文本
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
                textContent = new string[] { "你收获了成熟"+name };
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;

            }
            else {
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                // 判断是否装备了水壶或者锄头
                if (equip.GetComponent<Image>().sprite.name == null && equip.GetComponent<Image>().sprite.name != "水壶" && equip.GetComponent<Image>().sprite.name != "锄头")
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
                    textContent = new string[] { "你未装备水壶或锄头" };
                    dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                }
                else {
                    if (equip.GetComponent<Image>().sprite.name.Contains("水壶")) {
                        // 判断当日浇水次数是否用完
                        if (waterCount == 1)
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
                            textContent = new string[] { "今日已浇水" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                        else {
                            // 浇水次数加1
                            waterCount += 1;
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
                            textContent = new string[] { "浇水成功!" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                    }
                    if (equip.GetComponent<Image>().sprite.name == "锄头")
                    {
                        // 判断当日松土次数是否用完
                        if (landCount == 1)
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
                            textContent = new string[] { "今日已松土" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                        else
                        {
                            // 松土次数加1
                            landCount += 1;
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
                            textContent = new string[] { "松土成功!" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                    }
                }
            }
        }
    }
    // 编写方法进行更新种植后的数据
    public void updateFieldData() {
        // 是种植状态才更新
        if (isField == false) {
            return;
        }
        // 更新剩余时间
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        if (timeSystem.showMinute == 0 && timeSystem.showSeconds == 0)
        {
            // 更新浇水和松土次数
            waterCount = 0;
            landCount = 0;
            // 判断剩余时间是否为0
            if (leaveTime == 0)
            {
                // 判断生长状态是否不为3，为3不更新
                if (circleStatus == 3)
                {
                    return;
                }
                // 判断分和秒是否和种植的分和秒相等
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // 更新剩余时间
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "已成熟";
                    // 更新生长状态为采摘期
                    circleStatus = 3;
                }
                else
                {
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = System.Math.Abs(timeSystem.showMinute - minute) + "小时";
                }
            }
            else
            {
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
            // 不是零点
            // 判断剩余时间是否为0
            if (leaveTime == 0)
            {
                // 判断生长状态是否不为3，为3不更新
                if (circleStatus == 3)
                {
                    return;
                }
                // 判断分和秒是否和种植的分和秒相等
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // 更新剩余时间
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "已成熟";
                    // 更新生长状态为采摘期
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
        // 更新名称
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = name;
        // 更新生长周期
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(7).GetComponent<Text>().text = circleDay + "天";
        // 更新浇水和松土次数
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = waterCount + "/1";
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(5).GetComponent<Text>().text = landCount + "/1";
        Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/" + name);
        // 更新图片
        switch (circleStatus) {
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

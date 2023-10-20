using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoFishing : MonoBehaviour
{
    // 钓鱼功能
    public bool OpenFishing = false;
    // 控制文本框显示
    private bool showDialog = false;
    // 判断是否在钓鱼中
    private bool isFishing = false;
    // 判断上次钓鱼是否结束
    private bool beforeFish = false;
    // 等待钓鱼时间
    private float fishTime = 0;
    //用于控制是否可以开启钓鱼
    public bool canFish = false;
    // 判断是否不满足的条件时开启文本框
    public bool notFish = false;
    // 控制防止一次性把不满足的条件的文本框代码执行
    public bool afterNotFish = false;
    // 控制钓鱼中文本框显示
    public bool closeFishingAlert = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fishTime += Time.deltaTime;
        // 控制钓鱼图标的显示
        fishMessageControll();

        //钓鱼功能
        if (OpenFishing == true)
        {
            print("openFish:" + OpenFishing);
            // 当上次钓鱼结束才进行下一次钓鱼
            if (beforeFish == false)
            {
                print("before:" + beforeFish);
                // 检查并开启钓鱼状态
                fishing();
                fishTime = 0;
            }
            else
            {

                if (fishTime >= 3)
                {
                    // 获取物品
                    GetFish();
                    fishTime = 0;
                }

            }

        }
/*        // 控制文本框本身脚本激活
        if (notFish == true)
        {
            controllDialogJs();
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            OpenFishing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            OpenFishing = false;
        }
    }
    public void fishMessageControll()
    {
        // 获取主角物体
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (OpenFishing == true)
        {
            // 当我们能够钓鱼，开启动画，关闭钓鱼提示
            if (isFishing == false)
            {
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else {
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    // 点击E钓鱼
    public void fishing() {
        // 需要关闭文本框才能够开始钓鱼
        GameObject dialogObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true) {
            return;
        }
        if (canFish == false) {
            canFish = true;
            return;
        }
        // 判断是否按下E
        if (Input.GetKeyDown(KeyCode.E)) {
            //判断是否装备鱼竿，有鱼饵
            // 获取ui系统
            GameObject UiSystem = GameObject.FindGameObjectWithTag("mainUI");
            // 判断是否为空
            if (UiSystem.transform.GetChild(0).GetChild(0).GetChild(19).gameObject.activeSelf == false)
            {
                //**---------------------------------------------------------------------------------------------*/
                // 弹出文本框提示未装备钓鱼竿
                // 获取实例化后的物体
                GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                DialogMessage(dialogGameObject);
                // 变更文本
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message =new string[] { "你未装备钓鱼竿" };
                //**---------------------------------------------------------------------------------------------*/
                notFish = true;
            }
            else {
                // 装备的物品是否为钓鱼竿
                List<itemsCreations> fishRodList = new List<itemsCreations>();
                // 获取三个鱼竿
                itemsCreations fishRod1 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/fishRod1");
                itemsCreations fishRod2 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/fishRod2");
                itemsCreations fishRod3 = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/fishRod3");
                fishRodList.Add(fishRod1);
                fishRodList.Add(fishRod2);
                fishRodList.Add(fishRod3);
                // 比较装备的物品图片和钓鱼竿的物品图片名称是否一致
                Image equipImage = UiSystem.transform.GetChild(0).GetChild(0).GetChild(19).gameObject.GetComponent<Image>();
                int sameEquipImageCount = 0;
                for (int i = 0; i < fishRodList.Count; i++) {
                    print(equipImage.sprite);
                    print(fishRodList[i].name);
                    if (equipImage.sprite == fishRodList[i].itemImage) {
                        sameEquipImageCount++;
                    }
                }
                if (sameEquipImageCount == 0)
                {
                    // 弹出文本框提示未装备钓鱼竿
                    // 获取实例化后的物体
                    GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                    DialogMessage(dialogGameObject);
                    // 变更文本
                    dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你装备的物品不是钓鱼竿" };
                    notFish = true;
                }
                else {
                    // 是否有鱼饵
                    bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                    // 遍历玩家背包
                    if (playerBag.bag.Count == 0)
                    {
                        // 获取实例化后的物体
                        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                        DialogMessage(dialogGameObject);
                        // 变更文本
                        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你没有鱼饵" };
                        // 
                        notFish = true;
                    }
                    else {
                        int baitCount = 0;
                        for (int i = 0; i < playerBag.bag.Count; i++) {
                            if (playerBag.bag[i].itemName.Contains("鱼饵")) {
                                baitCount++;
                            }
                        }
                        if (baitCount == 0)
                        {
                            // 获取实例化后的物体
                            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                            DialogMessage(dialogGameObject);
                            // 变更文本
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你没有鱼饵" };
                            notFish = true;
                        }
                        else {
                            // 正式进入到垂钓状态------------------------
                            // 变更动画显示
                            // 获取主角物体
                            GameObject Player = GameObject.FindGameObjectWithTag("Player");
                            // 关闭钓鱼提示
                            isFishing = true;
                            Player.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                            // 关闭玩家脚本
                            Player.GetComponent<PlayerController>().enabled = false;
                            // 开启钓鱼进度
                            beforeFish = true;

                        }
                    }
                }
            }
        }
    }
    // 获取物品
    public void GetFish()
    {   // 获取主角物体
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        // 获取玩家背包
        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // 获取需要遍历的物品加入到钓鱼的获取里
        List<itemsCreations> addFishList = new List<itemsCreations>();
        // 获取所有的物品
        itemsCreations[] allItemList = Resources.LoadAll<itemsCreations>("Prefabs/bag/itemData/itemsCreation");
        for (int i = 0; i < allItemList.Length; i++)
        {
            if (allItemList[i].isAddFishing == true)
            {
                addFishList.Add(allItemList[i]);
            }
        }
        // 编写随机数
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, addFishList.Count - 1);
        //调用添加物品的方法
        getItem addItem = new getItem();
        int isFull = addItem.AddNewFishItem(addFishList[randomIndex], playerBag);
        // 提示文本
        // 如果物品满了
        if (isFull == 1)
        {
            // 获取实例化后的物体
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            DialogMessage(dialogGameObject);
            // 变更文本
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "背包或" + addFishList[randomIndex].itemName + "累加数量已满" };
        }
        else
        {
            // 获取实例化后的物体
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            DialogMessage(dialogGameObject);
            // 变更文本
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message =new string[] { "你钓到了:" + addFishList[randomIndex].itemName };
            // 在钓鱼成功获取物品的时候才进行增加经验
            // 增加人物经验
            PlayerController playerController = Player.GetComponent<PlayerController>();
            playerController.currentPlayerExp += addFishList[randomIndex].Exp;
            // 增加钓鱼经验
            playerController.currentFishExp += addFishList[randomIndex].Exp;

        }
        // 减少鱼饵
        GridPrefab gridPrefab = new GridPrefab();
        int primaryBaitIndex = -1;
        int middleBaitIndex = -1;
        int highBaitIndex = -1;
        for (int i = 0;i<playerBag.bag.Count;i++) {
            if (playerBag.bag[i].itemName == "初级鱼饵") {
                primaryBaitIndex = i;
            } else if (playerBag.bag[i].itemName == "中级鱼饵") {
                middleBaitIndex = i;
            } else if (playerBag.bag[i].itemName == "高级鱼饵") {
                highBaitIndex = i;
            }
        }
        if (primaryBaitIndex != -1) {
            gridPrefab.ItemDiscardRedurce(playerBag,playerBag.bag[primaryBaitIndex]);
        } else if (middleBaitIndex != -1) {
            gridPrefab.ItemDiscardRedurce(playerBag, playerBag.bag[middleBaitIndex]);
        } else if (highBaitIndex != -1) {
            gridPrefab.ItemDiscardRedurce(playerBag, playerBag.bag[highBaitIndex]);
        }


        //开启玩家脚本
        Player.GetComponent<PlayerController>().enabled = true;
        // 开启钓鱼提示
        isFishing = false;
        // 关闭钓鱼动画
        Player.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        // 关闭钓鱼进度
        beforeFish = false;
        // 控制可以开启钓鱼关闭
        canFish = false;
        // 正式进入到垂钓状态------------------------
    }

    // 文本信息显示
    public void DialogMessage(GameObject dialogGameObject) {
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
    }

    // 设置文本框脚本的失活
    public void controllDialogJs()
    {
        // 获取实例化后的物体
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (showDialog == true)
            {
                // 当触碰到路标的时候，本身已经有显示隐藏的功能,不需要文本框自带隐藏文本框的功能
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = false;
            }
            else
            {
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = true;
            }
        }
        // 防止一下直接执行到后面的代码，卡控一下
        if (afterNotFish == false)
        {
            afterNotFish = true;
            return;
        }
        if (dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            notFish = false;
            afterNotFish = false;
        }
    }
}

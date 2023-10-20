using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc2Controller : MonoBehaviour
{
    // 速度
    public float speed = 120;
    // 获取刚体
    private Rigidbody2D rigidbody;
    // 动画器
    private Animator animator;
    // 向量
    private Vector2 vector2;
    // 状态 0 正常行走 1 发生了碰撞 2 碰撞到主角 3 对话 4 送礼 5组队
    public int npcState = 0;
    // 多长时间变换npc的移动方向
    public float changeDirectionTime = 0;
    // 随机时间
    public float randomTime = 1;
    // 方向 x -1 左 0 不动 1 右 Input.GetAxisRaw("Horizontal");
    public float[] xDirection = {-1,0,1 };
    // y y为-1是往下 0 不动 y为1是往上 Input.GetAxisRaw("Vertical")
    public float[] yDirection = { -1, 0, 1 };
    // 随机xy
    public int xIndex = 0;
    public int yIndex = 0;
    // 最终的坐标
    public float x = 0;
    public float y = 0;
    // 前一个位移坐标
    public float beforeX = 0;
    public float beforeY = 0;

    //-----------碰撞-----------
    // 持续碰撞时间
    public float stayingTime = 0;
    // 正常碰撞变更方向执行时间
    public float touchNormalTime = 0;
    // 保持碰撞变更方向执行时间
    public float touchStayTime = 0;
    // 发生持续碰撞随机方向
    public int changeDirectionIndex = 0;
    //-----------碰撞------------

    // --------碰撞玩家
    // 碰撞玩家flag
    public bool touchPlayer = false;
    // 碰撞玩家的时间
    public float touchPlayerTime = 0;
    //---------碰撞玩家

    // --------- 对话---------------
    public bool firstTalk = true;
    public string npcName = "xxxx";
    // 当前好感度
    public int npcFavorLevel = 0;
    // 最大好感度等级
    public int maxNpcFavorLevel = 5;
    // 当前好感度
    public int currentFavor = 0;
    // 升级好感度需要的数值
    public int maxFavor = 0;
    // --------- 对话---------------

    // ----------组队---------------
    // 判断组队情况
    public bool isParty = false;
    // npc和玩家的距离
    public float partyX = 2;
    public float partyY = 2;
    // ----------组队---------------

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (npcState != 2)
        {
            // 变更动画
            ChangeAnim();
        }
        else {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);
        }
        // 好感度升级体系
        FavorUpdateLevel();
        // 控制ui显隐
        ControllUI();
    }

    private void FixedUpdate()
    {
        // 控制器
        NpcController();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (npcState != 5)
        {
            if (collision.gameObject.tag == "Player")
            {
                npcState = 2;
                rigidbody.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                npcState = 1;
            }
        }
        else {
            if (collision.gameObject.tag == "Player") {
                touchPlayer = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (npcState !=5) {
            if (collision.gameObject.tag == "Player")
            {
                npcState = 2;
                rigidbody.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                npcState = 1;
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isParty == true)
        {
            npcState = 5;
        }
        else {
            npcState = 0;
        }
        
        stayingTime = 0;
        touchStayTime = 0;
        touchNormalTime = 0;

        // 碰撞玩家
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        touchPlayerTime = 0;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        touchPlayer = false;
    }


    // npc控制器
    public void NpcController() {
        switch (npcState) {
            // 正常行走
            case 0:
                NormalMove();
                break;
            // 发生碰撞
            case 1:
                TouchMove();
                break;
            // 与玩家碰撞交互
            case 2:
                touchWithPlayer();
                break;
            // 对话
            case 3:
                // 不需要任何操作
                // 判断对话框有无关闭,关闭之后变更状态为正常行走
                talkWithPlayer();
                break;
            // 送礼
            case 4:
                break;
            // 组队
            case 5:
                // 组队移动
                patryMove();
                break;
        }
    }

    // 正常行走的时候，会需要执行什么
    public void NormalMove() {
        changeDirectionTime += Time.fixedDeltaTime;
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (changeDirectionTime >= randomTime) {
            beforeX = x;
            beforeY = y;
            System.Random random = new System.Random();
            randomTime = random.Next(1,5);
            xIndex = random.Next(0,3);
            yIndex = random.Next(0,3);
            // 我们的xy同时移动，重新随机
            while (xIndex !=1&&yIndex!=1) {
                xIndex = random.Next(0, 3);
                yIndex = random.Next(0, 3);
            }
            x = xDirection[xIndex];
            y = yDirection[yIndex];
            changeDirectionTime = 0;
        }
    }

    // 变更动画
    public void ChangeAnim() {
        // x为-1是往左，x为1是往右
        // y为-1是往下，y为1是往上
        // 左走
        if (x == -1 && y == 0)
        {
            animator.SetBool("lefting", true);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            // 右走
        }
        else if (x == 1 && y == 0)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", true);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }
        // 上走
        else if (x == 0 && y == 1)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", true);
            animator.SetBool("downing", false);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            // 下走
        }
        else if (x == 0 && y == -1)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", true);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }
        else if (x==0 && y==0) {
            // 停止
            if (beforeX == 0 && beforeY == 0) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
                // 面左
            } else if (beforeX == -1 && beforeY == 0) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", true);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                // 面右
            } else if (beforeX == 1 && beforeY == 0) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", true);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                // 面上
            } else if (beforeX == 0 && beforeY == 1) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", true);
                animator.SetBool("down", false);
                // 面下
            } else if (beforeX == 0 && beforeY == -1) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
            }
        }
    }

    // 碰撞
    public void TouchMove() {
        stayingTime += Time.fixedDeltaTime;
        touchNormalTime += Time.fixedDeltaTime;
        touchStayTime += Time.fixedDeltaTime;
        // 反方向
        if (stayingTime <= 2.5)
        {
            if (touchNormalTime >= 1) {
                beforeX = x;
                beforeY = y;
                switch (x)
                {
                    case -1:
                        x = 1;
                        break;
                    case 1:
                        x = -1;
                        break;
                }
                switch (y)
                {
                    case -1:
                        y = 1;
                        break;
                    case 1:
                        y = -1;
                        break;
                }
                if (x == 0 && y == 0)
                {
                    while (xIndex != 1 && yIndex != 1)
                    {
                        // 编写随机数
                        System.Random random = new System.Random();
                        xIndex = random.Next(0, 3);
                        yIndex = random.Next(0, 3);
                    }
                    x = xDirection[xIndex];
                    y = yDirection[yIndex];
                }
                touchNormalTime = 0;
            }
        }
        else {
            // 随机方向
            if (touchStayTime >= 1) {
                beforeX = x;
                beforeY = y;
                switch (changeDirectionIndex) {
                    case 0:
                        x = -1;
                        y = 0;
                        changeDirectionIndex = 1;
                        break;
                    case 1:
                        x = 1;
                        y = 0;
                        changeDirectionIndex = 2;
                        break;
                    case 2:
                        x = 0;
                        y = -1;
                        changeDirectionIndex = 3;
                        break;
                    case 3:
                        x = 0;
                        y = 1;
                        changeDirectionIndex = 0;
                        break;
                }
                touchStayTime = 0;
            }   
            
        }
        npcState = 0;
    }

    // 触碰玩家
    public void touchWithPlayer() {
        touchPlayerTime += Time.fixedDeltaTime;

        if (touchPlayerTime >= 5)
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            npcState = 0;
            touchPlayerTime = 0;
        }

    }

    public void talkWithPlayer() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == false) {
            npcState = 0;
        }
    }

    // 对话
    public void Talk() {
        if (firstTalk == true)
        {
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
            if (!dialog)
            {
                Instantiate(dialogPrefab);
            }
            // 赋值
            dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你好", "没见过的人呢", "说起来听村长说有一个人会来我们这", "看起来就是你了", "我叫" + npcName, "祝你在这玩的愉快!" };
            dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            currentFavor++;
        }
        else {
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
            if (!dialog)
            {
                Instantiate(dialogPrefab);
            }
            // 赋值
            dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            string[] textContent = new string[] { };
            // 接收文本
            textContent = talkMessage();
            dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        if (isParty == true)
        {
            npcState = 5;
        }
        else {
            npcState = 3;
        }
        
    }

    // 对话文本
    public string[] talkMessage() {
        string[] textContenxt = new string[] { };
        switch (npcFavorLevel)
        {
            case 0:
                textContenxt = new string[] { "有什么事吗?", "如果有需要不妨去找村长", "我现在有些忙" };
                break;
            case 1:
                textContenxt = new string[] { "啊,是你啊", "看来你也渐渐融入了这里", "这里很不错吧" };
                break;
            case 2:
                textContenxt = new string[] { "最近常听大家提起你", "看起来挺活跃呢", "有时间的话", "可以约我一起玩" };
                break;
            case 3:
                textContenxt = new string[] { "你最近在大家的印象中热情", "太好了", "没想到你能很好的融入这里", "以后的日子多多关照" };
                break;
            case 4:
                textContenxt = new string[] { "不知道为什么", "感觉对你总莫名的在意", "....", "啊...没什么", "我什么都没说" };
                break;

        }
        return textContenxt;
    }

    // 再见
    public void SayBye()
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
        textContent = new string[] { "再见!" };
        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
        // 变更状态为与玩家对话
        npcState = 3;
    }
    // 好感度升级体系
    public void FavorUpdateLevel() {
        // 最大好感度数值变动
        maxFavor = 10 + npcFavorLevel * (npcFavorLevel + 1) * 5;
        // 升级
        // 进行人物好感度升级
        if (currentFavor >= maxFavor)
        {
            // 判断当前经验是否大于最大经验
            if (currentFavor > maxFavor)
            {
                // 当前经验等于当前经验减去最大经验
                if (npcFavorLevel < maxNpcFavorLevel)
                {
                    currentFavor = currentFavor - maxFavor;
                    npcFavorLevel++;
                }
                else
                {
                    currentFavor = maxFavor;
                }
            }
            else
            {
                // 判断是否到最大等级
                if (npcFavorLevel < maxNpcFavorLevel)
                {
                    currentFavor = 0;
                    npcFavorLevel++;
                }
                else
                {
                    currentFavor = maxFavor;
                }
            }
        }
    }

    // 送礼
    public void sendGifts()
    {
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        if (state.transform.GetChild(19).GetComponent<Image>().sprite == null)
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
            textContent = new string[] { "未装备物品" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // 变更状态为与玩家对话
            npcState = 3;
        }
        else
        {
            // 消减物体
            useItem();
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
            textContent = new string[] { "谢谢" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // 变更状态为与玩家对话
            npcState = 3;
        }
    }

    public void useItem()
    {
        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        for (int i = 0; i < playerBag.bag.Count; i++)
        {
            // 判断是否为同一个物体
            if (playerBag.bag[i].itemImage == equip.GetComponent<Image>().sprite)
            {
                if (playerBag.bag[i].itemNumber == 1)
                {
                    // 只有一个物品进行移除
                    // 背包物品
                    playerBag.bag.Remove(playerBag.bag[i]);
                    // ui的物品进行销毁
                    // 获取背包ui
                    GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
                    // 获取背包UI父级物体下的网格子物体
                    GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
                    // 遍历
                    for (int j = 0; j < bagGrid.transform.childCount; j++)
                    {
                        if (equip.GetComponent<Image>().sprite == bagGrid.transform.GetChild(j).GetComponent<Image>().sprite)
                        {
                            Destroy(bagGrid.transform.GetChild(j));
                        }
                    }
                    // 设置装备为关闭状态并且状态的图片设置空
                    equip.GetComponent<Image>().sprite = null;
                    GridPrefab.clickItemIndex = -1;
                    // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
                    addToBag.RefreshItem();
                }
                else
                {
                    // 多个数量进行数量减一
                    playerBag.bag[i].itemNumber -= 1;
                }
                // 进行增加好感度
                currentFavor += playerBag.bag[i].npcFavorNumber;
            }
        }
        //
    }

    // 组队
    public void party() {
        // 是否未组队状态
        if (isParty == false)
        {
            // 好感度0，1，2，3不能组队
            if (npcFavorLevel < 4)
            {
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
                string[] textContent = new string[] { };
                System.Random random = new System.Random();
                int chooseText = random.Next(1, 4);
                switch (chooseText)
                {
                    case 1:
                        textContent = new string[] { "抱歉，我们还不熟吧" };
                        break;
                    case 2:
                        textContent = new string[] { "我现在很忙，以后再约吧" };
                        break;
                    case 3:
                        textContent = new string[] { "抱歉，一会儿我还有事" };
                        break;
                }
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                npcState = 3;
            }
            else {
                // 4，5能组队
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
                string[] textContent = new string[] {"乐意至极"};
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                npcState = 5;
                isParty = true;
            }
        }
        else {
            // 离队
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
            textContent = new string[] { "已经要走了吗", "那好吧", "下次再找我玩吧" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            isParty = false;
            npcState = 3;
        }
    }

    // 组队是否移动
    public void patryMove() {
        // 玩家位置
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        // npc位置
        Transform npc = gameObject.transform;
        float xDistance = System.Math.Abs(player.position.x - npc.position.x);
        float yDistance = System.Math.Abs(player.position.y - npc.position.y);
        float checkX = player.position.x - npc.position.x;
        float checkY = player.position.y - npc.position.y;

        // 停止移动
        if (xDistance <= partyX && yDistance <= partyY)
        {
            x = 0;
            y = 0;
            vector2.x = x;
            vector2.y = y;
            vector2.Normalize();
            rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        }
        else {
            // 跟随玩家
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
            {
                // npc跟随玩家
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }
            else {
                // 判断距离是否在限定范围
                if (xDistance > partyX)
                {
                    beforeX = x;
                    beforeY = y;
                    if (checkX < 0)
                    {
                        x = -1;
                    }
                    else
                    {
                        x = 1;
                    }
                    y = 0;
                    vector2.x = x;
                    vector2.y = y;
                    vector2.Normalize();
                    rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
                }

                if (yDistance > partyY) {
                    beforeX = x;
                    beforeY = y;
                    if (checkY < 0)
                    {
                        y = -1;
                    }
                    else
                    {
                        y = 1;
                    }
                    x = 0;
                    vector2.x = x;
                    vector2.y = y;
                    vector2.Normalize();
                    rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
                }
            }
        }
    }

    // 控制面板显隐
    public void ControllUI() {
        // 期间有没有按下E键
        if (touchPlayer == true && Input.GetKeyDown(KeyCode.E))
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            var playerPos = Camera.main.WorldToScreenPoint(player.position);
            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).position = new Vector3(playerPos.x, playerPos.y, 0);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc1Controller : MonoBehaviour
{
    // 获取刚体
    private Rigidbody2D rigidbody;
    // 向量
    private Vector2 vector2;
    // 动画控制器
    private Animator animator;
    // npc状态 // 0正常行动 1碰撞到物体  2碰撞到玩家 3与玩家交互 4与玩家组队
    public int npcState = 0;
    // npc移动速度
    public float speed;
    // x坐标数组
    private float[] xMove = { -1, 0, 1 };
    // y坐标数组
    private float[] yMove = { -1, 0, 1 };
    // 随机选择坐标的下标
    public int xIndex = 0;
    public int yIndex = 0;
    // 行动坐标
    public float x = 0;
    public float y = 0;
    // 上一次变换坐标
    public float beforeX = 0;
    public float beforeY = 0;
    // 判断行动的随机时间
    public float randomTime = 1;
    // 加总变换时间
    public float totalTime = 0;
    // 碰到玩家的时候，加总时间
    public float touchPlayerTime = 0;
    // 设置对话信息
    public string[] talkMessage;
    // 判断是否触碰到玩家
    public bool touchPlayer = false;

    // npc名字
    public string name;

    //-------------碰撞---------------
    // 发生碰撞用于反应远离碰撞的时间
    public float escapTime = 0;
    //------------碰撞----------------

    //------------持续碰撞时进行操作-------
    public float stayingTime = 0;
    public float escapStayingTime = 0;
    // 变更方向下标
    public float changeDirectionIndex = 0;
    // 远离持续碰撞的时间
    public float escapContinueStayingTime = 0;
    //--------------持续碰撞-----------

    //--------------对话--------------
    // 判断是否为第一次对话
    public bool isFistTalk = true;
    // 每天可增加好感度的对话次数/5次每次对话加1好感
    public int npcFavorCount = 0;
    // 当前好感度
    public int currentNpcFavor = 0;
    // 升级好感度等级需要的好感度
    public int maxNpcFavor = 5;
    // 好感度等级
    public int npcFavorLevel = 0;
    public int MaxNpcFavorLevel = 5;

    //--------------对话--------------

    //--------------组队---------------
    // 判断是否在组队状态
    public bool isParty = false;
    // 判断与玩家的距离
    // x轴不移动范围
    public float partyX = 2;
    // y轴不移动范围
    public float partyY = 2;
    //--------------组队---------------

    //-------------送礼---------------


    //-------------送礼---------------


    // Start is called before the first frame update
    void Start()
    {
        // 初始化赋值
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 120;
    }

    // Update is called once per frame
    void Update()
    {
        // 动画变换
        if (npcState == 0 || npcState == 1 || npcState == 5)
        {
            ChangeAnim();
        }
        else
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
        // 好感度升级系数
        NpcFavorUpdate();
    }
    private void FixedUpdate()
    {
        // npc控制器
        NpcController();

    }


    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果不为组队状态
        if (npcState != 5 && npcState != 3)
        {
            if (collision.gameObject.tag == "Player")
            {
                print("碰到玩家");
                // 触碰玩家开关打开
                touchPlayer = true;
                // 将状态改为碰撞到玩家
                npcState = 2;
            }
            else
            {
                // 将状态改为碰撞到物体
                npcState = 1;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 如果不是组队状态
        if (npcState != 5 && npcState != 3)
        {
            // 如果不是触碰到玩家
            if (collision.gameObject.tag != "Player")
            {
                npcState = 1;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 如果不为组队状态
        if (npcState != 5)
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            // 将状态变更为正常行动
            npcState = 0;
        }

        touchPlayer = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        stayingTime = 0;
        touchPlayerTime = 0;
        escapContinueStayingTime = 0;
    }

    // npc逻辑
    public void NpcController()
    {
        switch (npcState)
        {
            case 0:
                // 正常移动
                normalMove();
                break;
            case 1:
                // 碰到障碍物,反方向
                //negativeMove();
                // 碰到障碍物，随机方
                //RandomDirection();
                escapTime += Time.fixedDeltaTime;
                stayingTime += Time.fixedDeltaTime;
                escapContinueStayingTime += Time.fixedDeltaTime;
                if (escapTime >= 0.6)
                {
                    //negativeMove();
                    RandomDirection();
                    escapTime = 0;
                }
                break;
            case 2:
                // 碰到玩家,停止操作
                TouchPlayer();
                talkWithNpc();
                break;
            case 3:
                // 与玩家交互对话,什么都不操作，由点击后的按钮进行变更
                break;
            case 4:
                // 被玩家送礼
                break;
            case 5:
                // 与玩家组队
                PartyMove();
                break;
            case 6:
                // 当npc长时间接触碰撞离不开
                break;
        }
    }

    // 正常移动
    public void normalMove()
    {
        // 获取x，y轴的随机数
        totalTime += Time.fixedDeltaTime;
        // 设置向量
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // 让物体移动
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (totalTime >= randomTime)
        {
            // 记录上一个坐标
            beforeX = x;
            beforeY = y;
            // 编写随机数
            System.Random random = new System.Random();
            // 随机时间
            randomTime = random.Next(1, 5);
            // 随机坐标
            // x和y的index，不能同时移动
            xIndex = random.Next(0, 3);
            yIndex = random.Next(0, 3);
            while (xIndex != 1 && yIndex != 1)
            {

                xIndex = random.Next(0, 3);
                yIndex = random.Next(0, 3);
            }
            x = xMove[xIndex];
            y = yMove[yIndex];
            // 加总时间归0
            totalTime = 0;
        }
        npcState = 0;
    }

    // 碰到物品，反方向
    public void negativeMove()
    {

        // 记录上一个坐标
        beforeX = x;
        beforeY = y;
        // 变换坐标
        switch (x)
        {
            case 1:
                x = -1;
                break;
            case -1:
                x = 1;
                break;
        }
        switch (y)
        {
            case 1:
                y = -1;
                break;
            case -1:
                y = 1;
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
            x = xMove[xIndex];
            y = yMove[yIndex];
        }
        npcState = 0;
    }
    // 碰到物体，切换方向
    public void RandomDirection()
    {
        // 记录上一个坐标
        if (stayingTime <= 3)
        {
            beforeX = x;
            beforeY = y;
            // 变换坐标
            switch (x)
            {
                case 1:
                    x = -1;
                    break;
                case -1:
                    x = 1;
                    break;
            }
            switch (y)
            {
                case 1:
                    y = -1;
                    break;
                case -1:
                    y = 1;
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
                x = xMove[xIndex];
                y = yMove[yIndex];
            }
        }
        else
        {

            if (escapContinueStayingTime >= 1)
            {
                switch (changeDirectionIndex)
                {
                    case 0:
                        beforeX = x;
                        beforeY = y;
                        x = -1;
                        y = 0;
                        changeDirectionIndex = 1;
                        break;
                    case 1:
                        beforeX = x;
                        beforeY = y;
                        x = 1;
                        y = 0;
                        changeDirectionIndex = 2;
                        break;
                    case 2:
                        beforeX = x;
                        beforeY = y;
                        x = 0;
                        y = -1;
                        changeDirectionIndex = 3;
                        break;
                    case 3:
                        beforeX = x;
                        beforeY = y;
                        x = 0;
                        y = 1;
                        changeDirectionIndex = 0;
                        break;
                }
                escapContinueStayingTime = 0;
            }


        }

        npcState = 0;
    }
    // 动画变换
    public void ChangeAnim()
    {
        // x-1 左 x1右
        // y-1下  y1上
        // 向左
        if (x == -1 && y == 0)
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("lefting", true);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
        // 向右
        if (x == 1 && y == 0)
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("lefting", false);
            animator.SetBool("righting", true);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
        // 向下
        if (x == 0 && y == -1)
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", true);
        }
        // 向上
        if (x == 0 && y == 1)
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", true);
            animator.SetBool("downing", false);
        }
        // 当停止行动的时候
        if (x == 0 && y == 0)
        {
            // 判断前一次变换坐标
            // 前一次为00向下
            if (beforeX == 0 && beforeY == 0)
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
            }
            // 面左
            if (beforeX == -1 && beforeY == 0)
            {
                animator.SetBool("left", true);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
            }
            // 面右
            if (beforeX == 1 && beforeY == 0)
            {
                animator.SetBool("left", false);
                animator.SetBool("right", true);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
            }
            // 面下
            if (beforeX == 0 && beforeY == -1)
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
            }
            // 面上
            if (beforeX == 0 && beforeY == 1)
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", true);
                animator.SetBool("down", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
            }
        }

    }    
    // 与npc交互
    public void talkWithNpc()
    {
        if (touchPlayer == true && Input.GetKeyDown(KeyCode.E))
        {
            // 设置ui和角色距离始终一致
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;

            var playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y, 0);
            //npc.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y, 0);
            // 打开交互面板
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    // 当碰到玩家的时候
    public void TouchPlayer()
    {
        // 设置碰撞模式为动态
        rigidbody.bodyType = RigidbodyType2D.Static;
        touchPlayerTime += Time.fixedDeltaTime;
        if (touchPlayerTime >= 5)
        {
            if (npcState != 3 || npcState != 4 || npcState != 5)
            {
                // 变更npc状态
                npcState = 0;
                touchPlayerTime = 0;
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }

        }
    }

    // 当与玩家对话
    public void talkWithPlayer()
    {
        // 获取对话框预制体
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf == false)
        {
            if (isParty == true)
            {
                npcState = 5;
            }
            else
            {
                npcState = 0;
            }

        }
    }

    // 对话
    public void Talk()
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
        //showDialog = !showDialog;
        dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        // 变更文本
        string[] textContent;
        textContent = talkInfo();
        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
        //dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "药店\n营业时间:10:00-20:00";
        // 每次对话，好感度加1
        currentNpcFavor += 1;
        // 变更状态为与玩家对话
        if (isParty == true)
        {
            npcState = 5;
        }
        else
        {
            npcState = 3;
        }

    }
    // 组队
    public void Party()
    {
        // 判断是否为组队状态
        if (isParty == false)
        {
            // 好感度0-3拒绝组队
            if (npcFavorLevel < 4)
            {
                // 变更状态为对话
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
                // 变更状态为与玩家对话
                npcState = 3;

            }
            else
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
                textContent = new string[] { "乐意至极" };
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                // 变更为组队状态
                npcState = 5;
                // 设置bodyType
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                isParty = true;
            }
        }
        else
        {
            // 变更为对话状态
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
            // 变更状态为与玩家对话
            npcState = 3;
            // 变更为非组队状态
            isParty = false;
        }


    }

    // 组队行走逻辑
    public void PartyMove()
    {
        // 判断与玩家的距离
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float xInstance = System.Math.Abs(player.position.x - gameObject.transform.position.x);
        float yInstance = System.Math.Abs(player.position.y - gameObject.transform.position.y);
        float xDirection = player.position.x - gameObject.transform.position.x;
        float yDirection = player.position.y - gameObject.transform.position.y;
        if (xInstance <= partyX && yInstance <= partyY)
        {

            // 保持不动
            x = 0;
            y = 0;
            // 设置向量
            vector2.x = x;
            vector2.y = y;
            vector2.Normalize();
            // 让物体移动
            rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        }
        else
        {

            if (xInstance > partyX)
            {
                beforeX = x;
                beforeY = y;
                if (xDirection < 0)
                {
                    x = -1;
                }
                else
                {
                    x = 1;
                }

                y = 0;
                // 设置向量
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                // 让物体移动

                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }

            if (yInstance > partyY)
            {
                beforeX = x;
                beforeY = y;
                // x轴未超出y超出

                if (yDirection < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
                x = 0;
                // 设置向量
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                // 让物体移动
                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }
        }
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

    // 设置对话
    public string[] talkInfo()
    {
        string[] textContent = new string[0];
        // 是否第一次对话
        if (isFistTalk == true)
        {
            textContent = new string[] { "你好", "没见过的人呢", "说起来听村长说有一个人会来我们这", "看起来就是你了", "我叫" + name, "祝你在这玩的愉快!" };
            isFistTalk = false;
        }
        else
        {
            // 不是第一次对话，根据好感度设置文本
            switch (npcFavorLevel)
            {
                case 0:
                    textContent = new string[] { "有什么事吗?", "如果有需要不妨去找村长", "我现在有些忙" };
                    break;
                case 1:
                    textContent = new string[] { "啊,是你啊", "看来你也渐渐融入了这里", "这里很不错吧" };
                    break;
                case 2:
                    textContent = new string[] { "最近常听大家提起你", "看起来挺活跃呢", "有时间的话", "可以约我一起玩" };
                    break;
                case 3:
                    textContent = new string[] { "你最近在大家的印象中热情", "太好了", "没想到你能很好的融入这里", "以后的日子多多关照" };
                    break;
                case 4:
                    textContent = new string[] { "不知道为什么", "感觉对你总莫名的在意", "....", "啊...没什么", "我什么都没说" };
                    break;

            }
        }
        return textContent;
    }
    // 送礼
    public void sendGifts() {
        // 判断是否装备物品
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        if (equip.GetComponent<Image>().sprite == null)
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
            textContent = new string[] {"您未装备物品" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // 变更状态为与玩家对话
            npcState = 3;
        }
        else {
            // 执行减少物品的方法
            useItem();
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

    // 使用物品
    public void useItem() {
        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        for (int i =0; i<playerBag.bag.Count;i++) {
            // 判断是否为同一个物体
            if (playerBag.bag[i].itemImage == equip.GetComponent<Image>().sprite) {
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
                    for (int j = 0; j<bagGrid.transform.childCount;j++) {
                        if (equip.GetComponent<Image>().sprite == bagGrid.transform.GetChild(j).GetComponent<Image>().sprite) {
                            Destroy(bagGrid.transform.GetChild(j));
                        }
                    }
                    // 设置装备为关闭状态并且状态的图片设置空
                    equip.GetComponent<Image>().sprite = null;
                    GridPrefab.clickItemIndex = -1;
                    // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
                    addToBag.RefreshItem();
                }
                else {
                    // 多个数量进行数量减一
                    playerBag.bag[i].itemNumber -= 1;
                }
                // 进行增加好感度
                currentNpcFavor += playerBag.bag[i].npcFavorNumber;
            }
        }
        //
    }

    // npc好感度升级系数和升级好感度
    public void NpcFavorUpdate() { 
        maxNpcFavor = 10 + npcFavorLevel * (npcFavorLevel + 1) * npcFavorLevel * 5;
        if (currentNpcFavor >= maxNpcFavor)
        {
            // 判断当前经验是否大于最大经验
            if (currentNpcFavor > maxNpcFavor)
            {
                // 当前经验等于当前经验减去最大经验
                if (npcFavorLevel < MaxNpcFavorLevel)
                {
                    currentNpcFavor = currentNpcFavor - maxNpcFavor;
                    npcFavorLevel++;
                }
                else
                {
                    currentNpcFavor = maxNpcFavor;
                }
            }
            else
            {
                // 判断是否到最大等级
                if (npcFavorLevel < MaxNpcFavorLevel)
                {
                    currentNpcFavor = 0;
                    npcFavorLevel++;
                }
                else
                {
                    currentNpcFavor = maxNpcFavor;
                }
            }
        }
    }

}

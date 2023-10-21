using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowControler : MonoBehaviour
{
    // 引入组件
    private Animator animator;
    private Rigidbody2D rigidbody;
    private Vector2 vector;
    // 速度
    private float speed = 100;
    // 状态 0 正常行走 1 进食 
    public int cowState = 0;
    // -------------正常行走--------------
    public float x = 0;
    public float y = 0;
    // 前一个坐标
    public float beforeX;
    public float beforeY;
    // x y移动的数组
    private float[] xMove = { -1, 0, 1 };
    private float[] yMove = { -1, 0, 1 };
    // 变换方向，多长时间进行变换
    public float randomTime = 2;
    // 加总变换方向的时间
    public float totalRandomTime = 0;

    //-------------------------------------
    // --------------产奶和进食------------
    public int currentHunger = 100;
    public int maxHunger = 100;
    // 减少饥饿度的时间
    public float hungerTime = 0;
    // 产奶次数
    public int currentMilkCount = 0;
    public int maxMilkCount = 5;
    public float milkTime = 15;
    public float totalMilkTime = 0;
    // 进食区
    public float eatX = 1;
    public float eatY = 47;
    // ------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 变换动画
        ChangeAnim();
        // 恢复产奶时间
        resetMilkCount();

    }
    private void FixedUpdate()
    {
        // 控制器
        cowControll();
        // 减少饥饿度
        redurceHunger();
        // 产奶
        initMilk();
    }

    // 奶牛控制器
    public void cowControll() {
        switch (cowState) {
            case 0:
                // 正常行走
                normalMove();
                break;
            case 1:
                // 进食
                eating();
                break;
        }
    }

    // 正常行走
    public void normalMove() {
        // x-1 左 x1右 x0
        // y-1下  y1上 y0 
        totalRandomTime += Time.fixedDeltaTime;
        // 赋值移动
        vector.x = x;
        vector.y = y;
        vector.Normalize();
        rigidbody.velocity = speed * vector * Time.fixedDeltaTime;
        if (totalRandomTime >= randomTime) {
            // 前一个坐标
            beforeX = x;
            beforeY = y;
            // 变换方向
            System.Random random = new System.Random();
            // 随机变换时间
            randomTime = random.Next(3, 8);
            int xIndex = random.Next(0, 3);
            int yIndex = random.Next(0, 3);
            // 防止x和y同时移动
            while (xIndex != 1 && yIndex != 1)
            {
                xIndex = random.Next(0, 3);
                yIndex = random.Next(0, 3);
            }
            print(xIndex + "," + yIndex);
            x = xMove[xIndex];
            y = yMove[yIndex];
            totalRandomTime = 0;
        }
    }

    // 变换动画
    public void ChangeAnim() {
        // x-1 左 x1右 x0
        // y-1下  y1上 y0 
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
    // 奶牛产奶
    public void initMilk() {
        // 饥饿度不为0
        if (currentHunger != 0) {
            totalMilkTime += Time.fixedDeltaTime;
            if (totalMilkTime >= milkTime) {
                System.Random random = new System.Random();
                milkTime = random.Next(10, 20);
                // 是否今日还能产奶
                if (currentMilkCount != maxMilkCount) {
                    currentMilkCount++;
                    // 实例化牛奶
                    GameObject milk = Resources.Load<GameObject>("Prefabs/bag/items/animals/item/牛奶");
                    Instantiate(milk, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                }
                totalMilkTime = 0;
            }
        }
    }
    // 减少饥饿度时间
    public void redurceHunger() {
        // 判断是否饥饿度为0
        if (currentHunger != 0)
        {
            hungerTime += Time.fixedDeltaTime;
            if (hungerTime >= 2)
            {
                currentHunger--;
                cowState = 0;
                hungerTime = 0;
            }
        }
        else {
            cowState = 1;
        }
    }
    // 恢复产奶
    public void resetMilkCount() {
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        if (timeSystem.showMinute==0&& timeSystem.showSeconds == 0) {
            currentMilkCount = 0;
        }
    }

    // 进食
    public void eating() {
        float xDistance = System.Math.Abs(eatX - gameObject.transform.position.x);
        float yDistance = System.Math.Abs(eatY - gameObject.transform.position.y);
        float xDirection = eatX - gameObject.transform.position.x;
        float yDirection = eatY - gameObject.transform.position.y;
        if (xDistance <= 0.1 && yDistance <= 1)
        {
            beforeX = x;
            beforeY = y;
            x = 0;
            y = 0;
            vector.x = x;
            vector.y = y;
            vector.Normalize();
            rigidbody.velocity = speed * vector * Time.fixedDeltaTime;

        }
        else {
            if (xDistance >0.1) {
                beforeX = x;
                beforeY = y;
                y = 0;
                if (xDirection < 0)
                {
                    x = -1;
                }
                else {
                    x = 1;
                }
                vector.x = x;
                vector.y = y;
                vector.Normalize();
                rigidbody.velocity = speed * vector * Time.fixedDeltaTime;
            }

            if (yDistance >1) {
                x = 0;
                if (yDirection < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
                vector.x = x;
                vector.y = y;
                vector.Normalize();
                rigidbody.velocity = speed * vector * Time.fixedDeltaTime;
            }
        }
    }
}

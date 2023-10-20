using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenController : MonoBehaviour
{
    // 获取刚体
    private Rigidbody2D rigidbody;
    // 向量
    private Vector2 vector2;
    // 动画控制器
    private Animator animator;
    // 动物状态 0正常移动  1 进食
    public int animalState = 0;
    //-------------------------正常行动------------------
    // 移动速度
    public float speed = 100;
    // x坐标数组
    private float[] xMove = { -1, 0, 1 };
    // y坐标数组
    private float[] yMove = { -1, 0, 1 };
    // 动物运动方向 
    public float x = 0;
    public float y = 0;
    // 前一个运动方向
    public float beforeX = 0;
    public float beforeY = 0;
    // 判断行动的随机时间
    public float randomTime = 1;
    // 加总时间
    public float totalTime = 0;
    //----------------------饥饿度--------------------------------
    // 饥饿度
    public int hunger = 100;
    // 最大饥饿度
    public int maxHunger = 100;
    // 降低饥饿度的时间
    public float hungerTime = 0;
    // ---------------------碰到门禁-----------------------------
    // 碰到门禁时间
    public float touchDoorTime = 0;
    // 判断是否碰到门禁
    public bool isTouchDoor = false;
    //------------------------下蛋-------------------------------
    // 当日已下蛋次数
    public int eggCount = 0;
    // 最大可下蛋次数
    public int maxEggCount = 5;
    // 隔多长时间下蛋
    public float eggTime = 10;
    // 下蛋循环时间
    public float eggTotalTime = 0;
    //------------------------ 进食-----------------------------
    // 进食区坐标
    public float eatX = (float)-29;
    public float eatY = 48;




    // Start is called before the first frame update
    void Start()
    {
        // 初始化赋值
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 动画
        ChangeAnim();
        // 恢复下蛋次数
        reSetEgg();

    }

    private void FixedUpdate()
    {
        // 动物状态
        AnimalControll();
        // 状态变更
        changeState();
        //碰到门禁
        touchDoorAbandon();
        // 下蛋
        addEgg();



    }
    // 碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.otherCollider.tag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchDoorTime = 0;
    }

    // 动物状态控制器
    public void AnimalControll() {
        switch (animalState) {
            case 0:
                // 正常行动
                NormalMove();
                break;
            case 1:
                // 进食
                eating();
                break;
        }
    }

    // 变更动画
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
    // 正常行动
    public void NormalMove() {
        if (isTouchDoor == true) {
            return;
        }
        // 获取x，y轴的随机数
        totalTime += Time.fixedDeltaTime;
        // 设置向量
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // 让物体移动
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        // 变更方向时间
        if (totalTime >= randomTime) {
            // 记录上一个坐标
            beforeX = x;
            beforeY = y;
            // 编写随机数
            System.Random random = new System.Random();
            // 随机时间
            randomTime = random.Next(3, 8);
            // 随机坐标
            // x和y的index，不能同时移动
            int xIndex = random.Next(0, 3);
            int yIndex = random.Next(0, 3);
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
    }

    // 进食
    public void eating() {
        // 前往坐标x-29 y47
        // 判断与进食区的距离
        float xInstance = System.Math.Abs(System.Math.Abs(eatX) - System.Math.Abs(gameObject.transform.position.x));
        float yInstance = System.Math.Abs(System.Math.Abs(eatY) - System.Math.Abs(gameObject.transform.position.y));
        float xDirection = eatX - gameObject.transform.position.x;
        float yDirection = eatY - gameObject.transform.position.y;
        if (xInstance <= 9 && yInstance <= 3)
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
            if (xInstance > 9)
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
            if (yInstance > 3)
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
    // 状态变更
    public void changeState() {
        hungerTime += Time.fixedDeltaTime;
        if (hungerTime >= 1) {
            if (hunger != 0)
            {
                // 减少饥饿度
                hunger--;
                // 状态为正常行动
                animalState = 0;
            }
            else {
                // 进食
                animalState = 1;
            }
            hungerTime = 0;
        }
    }
    // 碰到门禁
    public void touchDoorAbandon() {
        if (isTouchDoor == false) {
            return;
        }
        touchDoorTime += Time.fixedDeltaTime;
        // 设置向量
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // 让物体移动
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (touchDoorTime >=1) {
            // 记录前一个坐标
            beforeX = x;
            beforeY = y;
            // 变更方向
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
            isTouchDoor = false;
        }
        

    }

    // 恢复下蛋次数 
    public void reSetEgg() {
        // 获取时间系统
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        if (timeSystem.showMinute == 0 && timeSystem.showSeconds == 0) {
            eggCount = 0;
        }
    }

    // 下蛋
    public void addEgg() {
        // Assets/Resources/Prefabs/bag/items/animals/item/鸡蛋.prefab
        eggTotalTime += Time.fixedDeltaTime;
        if (eggTotalTime >=eggTime) {
            // 随机下蛋时间
            System.Random random = new System.Random();
            eggTime = random.Next(10,30);
            // 实例化鸡蛋
            GameObject eggPrefab = Resources.Load<GameObject>("Prefabs/bag/items/animals/item/鸡蛋");
            // 不挨饿才下蛋
            if (hunger!=0) {
                // 判断是否当天已经下完了鸡蛋
                if (eggCount!=maxEggCount) {
                    // 下蛋次数加1
                    eggCount++;
                    // 实例化鸡蛋
                    Instantiate(eggPrefab,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0),Quaternion.Euler(0,0,0));
                }
            }
            eggTotalTime = 0;
        }
    }
}

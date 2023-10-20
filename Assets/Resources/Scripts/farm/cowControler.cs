using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowControler : MonoBehaviour
{
    // �������
    private Animator animator;
    private Rigidbody2D rigidbody;
    private Vector2 vector;
    // �ٶ�
    private float speed = 100;
    // ״̬ 0 �������� 1 ��ʳ 
    public int cowState = 0;
    // -------------��������--------------
    public float x = 0;
    public float y = 0;
    // ǰһ������
    public float beforeX;
    public float beforeY;
    // x y�ƶ�������
    private float[] xMove = { -1, 0, 1 };
    private float[] yMove = { -1, 0, 1 };
    // �任���򣬶೤ʱ����б任
    public float randomTime = 2;
    // ���ܱ任�����ʱ��
    public float totalRandomTime = 0;

    //-------------------------------------
    // --------------���̺ͽ�ʳ------------
    public int currentHunger = 100;
    public int maxHunger = 100;
    // ���ټ����ȵ�ʱ��
    public float hungerTime = 0;
    // ���̴���
    public int currentMilkCount = 0;
    public int maxMilkCount = 5;
    public float milkTime = 15;
    public float totalMilkTime = 0;
    // ��ʳ��
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
        // �任����
        ChangeAnim();
        // �ָ�����ʱ��
        resetMilkCount();

    }
    private void FixedUpdate()
    {
        // ������
        cowControll();
        // ���ټ�����
        redurceHunger();
        // ����
        initMilk();
    }

    // ��ţ������
    public void cowControll() {
        switch (cowState) {
            case 0:
                // ��������
                normalMove();
                break;
            case 1:
                // ��ʳ
                eating();
                break;
        }
    }

    // ��������
    public void normalMove() {
        // x-1 �� x1�� x0
        // y-1��  y1�� y0 
        totalRandomTime += Time.fixedDeltaTime;
        // ��ֵ�ƶ�
        vector.x = x;
        vector.y = y;
        vector.Normalize();
        rigidbody.velocity = speed * vector * Time.fixedDeltaTime;
        if (totalRandomTime >= randomTime) {
            // ǰһ������
            beforeX = x;
            beforeY = y;
            // �任����
            System.Random random = new System.Random();
            // ����任ʱ��
            randomTime = random.Next(3, 8);
            int xIndex = random.Next(0, 3);
            int yIndex = random.Next(0, 3);
            // ��ֹx��yͬʱ�ƶ�
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

    // �任����
    public void ChangeAnim() {
        // x-1 �� x1�� x0
        // y-1��  y1�� y0 
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
        // ����
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
        // ����
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
        // ����
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
        // ��ֹͣ�ж���ʱ��
        if (x == 0 && y == 0)
        {
            // �ж�ǰһ�α任����
            // ǰһ��Ϊ00����
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
            // ����
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
            // ����
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
            // ����
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
            // ����
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
    // ��ţ����
    public void initMilk() {
        // �����Ȳ�Ϊ0
        if (currentHunger != 0) {
            totalMilkTime += Time.fixedDeltaTime;
            if (totalMilkTime >= milkTime) {
                System.Random random = new System.Random();
                milkTime = random.Next(10, 20);
                // �Ƿ���ջ��ܲ���
                if (currentMilkCount != maxMilkCount) {
                    currentMilkCount++;
                    // ʵ����ţ��
                    GameObject milk = Resources.Load<GameObject>("Prefabs/bag/items/animals/item/ţ��");
                    Instantiate(milk, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                }
                totalMilkTime = 0;
            }
        }
    }
    // ���ټ�����ʱ��
    public void redurceHunger() {
        // �ж��Ƿ񼢶���Ϊ0
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
    // �ָ�����
    public void resetMilkCount() {
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        if (timeSystem.showMinute==0&& timeSystem.showSeconds == 0) {
            currentMilkCount = 0;
        }
    }

    // ��ʳ
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

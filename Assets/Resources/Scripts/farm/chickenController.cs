using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenController : MonoBehaviour
{
    // ��ȡ����
    private Rigidbody2D rigidbody;
    // ����
    private Vector2 vector2;
    // ����������
    private Animator animator;
    // ����״̬ 0�����ƶ�  1 ��ʳ
    public int animalState = 0;
    //-------------------------�����ж�------------------
    // �ƶ��ٶ�
    public float speed = 100;
    // x��������
    private float[] xMove = { -1, 0, 1 };
    // y��������
    private float[] yMove = { -1, 0, 1 };
    // �����˶����� 
    public float x = 0;
    public float y = 0;
    // ǰһ���˶�����
    public float beforeX = 0;
    public float beforeY = 0;
    // �ж��ж������ʱ��
    public float randomTime = 1;
    // ����ʱ��
    public float totalTime = 0;
    //----------------------������--------------------------------
    // ������
    public int hunger = 100;
    // ��󼢶���
    public int maxHunger = 100;
    // ���ͼ����ȵ�ʱ��
    public float hungerTime = 0;
    // ---------------------�����Ž�-----------------------------
    // �����Ž�ʱ��
    public float touchDoorTime = 0;
    // �ж��Ƿ������Ž�
    public bool isTouchDoor = false;
    //------------------------�µ�-------------------------------
    // �������µ�����
    public int eggCount = 0;
    // �����µ�����
    public int maxEggCount = 5;
    // ���೤ʱ���µ�
    public float eggTime = 10;
    // �µ�ѭ��ʱ��
    public float eggTotalTime = 0;
    //------------------------ ��ʳ-----------------------------
    // ��ʳ������
    public float eatX = (float)-29;
    public float eatY = 48;




    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ����ֵ
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ����
        ChangeAnim();
        // �ָ��µ�����
        reSetEgg();

    }

    private void FixedUpdate()
    {
        // ����״̬
        AnimalControll();
        // ״̬���
        changeState();
        //�����Ž�
        touchDoorAbandon();
        // �µ�
        addEgg();



    }
    // ��ײ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.otherCollider.tag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchDoorTime = 0;
    }

    // ����״̬������
    public void AnimalControll() {
        switch (animalState) {
            case 0:
                // �����ж�
                NormalMove();
                break;
            case 1:
                // ��ʳ
                eating();
                break;
        }
    }

    // �������
    public void ChangeAnim()
    {
        // x-1 �� x1��
        // y-1��  y1��
        // ����
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
    // �����ж�
    public void NormalMove() {
        if (isTouchDoor == true) {
            return;
        }
        // ��ȡx��y��������
        totalTime += Time.fixedDeltaTime;
        // ��������
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // �������ƶ�
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        // �������ʱ��
        if (totalTime >= randomTime) {
            // ��¼��һ������
            beforeX = x;
            beforeY = y;
            // ��д�����
            System.Random random = new System.Random();
            // ���ʱ��
            randomTime = random.Next(3, 8);
            // �������
            // x��y��index������ͬʱ�ƶ�
            int xIndex = random.Next(0, 3);
            int yIndex = random.Next(0, 3);
            while (xIndex != 1 && yIndex != 1)
            {

                xIndex = random.Next(0, 3);
                yIndex = random.Next(0, 3);
            }
            x = xMove[xIndex];
            y = yMove[yIndex];
            // ����ʱ���0
            totalTime = 0;
        }
    }

    // ��ʳ
    public void eating() {
        // ǰ������x-29 y47
        // �ж����ʳ���ľ���
        float xInstance = System.Math.Abs(System.Math.Abs(eatX) - System.Math.Abs(gameObject.transform.position.x));
        float yInstance = System.Math.Abs(System.Math.Abs(eatY) - System.Math.Abs(gameObject.transform.position.y));
        float xDirection = eatX - gameObject.transform.position.x;
        float yDirection = eatY - gameObject.transform.position.y;
        if (xInstance <= 9 && yInstance <= 3)
        {

            // ���ֲ���
            x = 0;
            y = 0;
            // ��������
            vector2.x = x;
            vector2.y = y;
            vector2.Normalize();
            // �������ƶ�
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
                // ��������
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                // �������ƶ�

                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }
            if (yInstance > 3)
            {
                beforeX = x;
                beforeY = y;
                // x��δ����y����

                if (yDirection < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
                x = 0;
                // ��������
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                // �������ƶ�
                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }

        }

    }
    // ״̬���
    public void changeState() {
        hungerTime += Time.fixedDeltaTime;
        if (hungerTime >= 1) {
            if (hunger != 0)
            {
                // ���ټ�����
                hunger--;
                // ״̬Ϊ�����ж�
                animalState = 0;
            }
            else {
                // ��ʳ
                animalState = 1;
            }
            hungerTime = 0;
        }
    }
    // �����Ž�
    public void touchDoorAbandon() {
        if (isTouchDoor == false) {
            return;
        }
        touchDoorTime += Time.fixedDeltaTime;
        // ��������
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // �������ƶ�
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (touchDoorTime >=1) {
            // ��¼ǰһ������
            beforeX = x;
            beforeY = y;
            // �������
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

    // �ָ��µ����� 
    public void reSetEgg() {
        // ��ȡʱ��ϵͳ
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        if (timeSystem.showMinute == 0 && timeSystem.showSeconds == 0) {
            eggCount = 0;
        }
    }

    // �µ�
    public void addEgg() {
        // Assets/Resources/Prefabs/bag/items/animals/item/����.prefab
        eggTotalTime += Time.fixedDeltaTime;
        if (eggTotalTime >=eggTime) {
            // ����µ�ʱ��
            System.Random random = new System.Random();
            eggTime = random.Next(10,30);
            // ʵ��������
            GameObject eggPrefab = Resources.Load<GameObject>("Prefabs/bag/items/animals/item/����");
            // ���������µ�
            if (hunger!=0) {
                // �ж��Ƿ����Ѿ������˼���
                if (eggCount!=maxEggCount) {
                    // �µ�������1
                    eggCount++;
                    // ʵ��������
                    Instantiate(eggPrefab,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0),Quaternion.Euler(0,0,0));
                }
            }
            eggTotalTime = 0;
        }
    }
}

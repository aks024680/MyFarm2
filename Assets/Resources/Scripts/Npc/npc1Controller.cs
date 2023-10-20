using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc1Controller : MonoBehaviour
{
    // ��ȡ����
    private Rigidbody2D rigidbody;
    // ����
    private Vector2 vector2;
    // ����������
    private Animator animator;
    // npc״̬ // 0�����ж� 1��ײ������  2��ײ����� 3����ҽ��� 4��������
    public int npcState = 0;
    // npc�ƶ��ٶ�
    public float speed;
    // x��������
    private float[] xMove = { -1, 0, 1 };
    // y��������
    private float[] yMove = { -1, 0, 1 };
    // ���ѡ��������±�
    public int xIndex = 0;
    public int yIndex = 0;
    // �ж�����
    public float x = 0;
    public float y = 0;
    // ��һ�α任����
    public float beforeX = 0;
    public float beforeY = 0;
    // �ж��ж������ʱ��
    public float randomTime = 1;
    // ���ܱ任ʱ��
    public float totalTime = 0;
    // ������ҵ�ʱ�򣬼���ʱ��
    public float touchPlayerTime = 0;
    // ���öԻ���Ϣ
    public string[] talkMessage;
    // �ж��Ƿ��������
    public bool touchPlayer = false;

    // npc����
    public string name;

    //-------------��ײ---------------
    // ������ײ���ڷ�ӦԶ����ײ��ʱ��
    public float escapTime = 0;
    //------------��ײ----------------

    //------------������ײʱ���в���-------
    public float stayingTime = 0;
    public float escapStayingTime = 0;
    // ��������±�
    public float changeDirectionIndex = 0;
    // Զ�������ײ��ʱ��
    public float escapContinueStayingTime = 0;
    //--------------������ײ-----------

    //--------------�Ի�--------------
    // �ж��Ƿ�Ϊ��һ�ζԻ�
    public bool isFistTalk = true;
    // ÿ������ӺøжȵĶԻ�����/5��ÿ�ζԻ���1�ø�
    public int npcFavorCount = 0;
    // ��ǰ�øж�
    public int currentNpcFavor = 0;
    // �����øжȵȼ���Ҫ�ĺøж�
    public int maxNpcFavor = 5;
    // �øжȵȼ�
    public int npcFavorLevel = 0;
    public int MaxNpcFavorLevel = 5;

    //--------------�Ի�--------------

    //--------------���---------------
    // �ж��Ƿ������״̬
    public bool isParty = false;
    // �ж�����ҵľ���
    // x�᲻�ƶ���Χ
    public float partyX = 2;
    // y�᲻�ƶ���Χ
    public float partyY = 2;
    //--------------���---------------

    //-------------����---------------


    //-------------����---------------


    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ����ֵ
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 120;
    }

    // Update is called once per frame
    void Update()
    {
        // �����任
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
        // �øж�����ϵ��
        NpcFavorUpdate();
    }
    private void FixedUpdate()
    {
        // npc������
        NpcController();

    }


    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����Ϊ���״̬
        if (npcState != 5 && npcState != 3)
        {
            if (collision.gameObject.tag == "Player")
            {
                print("�������");
                // ������ҿ��ش�
                touchPlayer = true;
                // ��״̬��Ϊ��ײ�����
                npcState = 2;
            }
            else
            {
                // ��״̬��Ϊ��ײ������
                npcState = 1;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ����������״̬
        if (npcState != 5 && npcState != 3)
        {
            // ������Ǵ��������
            if (collision.gameObject.tag != "Player")
            {
                npcState = 1;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �����Ϊ���״̬
        if (npcState != 5)
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            // ��״̬���Ϊ�����ж�
            npcState = 0;
        }

        touchPlayer = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        stayingTime = 0;
        touchPlayerTime = 0;
        escapContinueStayingTime = 0;
    }

    // npc�߼�
    public void NpcController()
    {
        switch (npcState)
        {
            case 0:
                // �����ƶ�
                normalMove();
                break;
            case 1:
                // �����ϰ���,������
                //negativeMove();
                // �����ϰ�������
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
                // �������,ֹͣ����
                TouchPlayer();
                talkWithNpc();
                break;
            case 3:
                // ����ҽ����Ի�,ʲô�����������ɵ����İ�ť���б��
                break;
            case 4:
                // ���������
                break;
            case 5:
                // ��������
                PartyMove();
                break;
            case 6:
                // ��npc��ʱ��Ӵ���ײ�벻��
                break;
        }
    }

    // �����ƶ�
    public void normalMove()
    {
        // ��ȡx��y��������
        totalTime += Time.fixedDeltaTime;
        // ��������
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // �������ƶ�
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (totalTime >= randomTime)
        {
            // ��¼��һ������
            beforeX = x;
            beforeY = y;
            // ��д�����
            System.Random random = new System.Random();
            // ���ʱ��
            randomTime = random.Next(1, 5);
            // �������
            // x��y��index������ͬʱ�ƶ�
            xIndex = random.Next(0, 3);
            yIndex = random.Next(0, 3);
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
        npcState = 0;
    }

    // ������Ʒ��������
    public void negativeMove()
    {

        // ��¼��һ������
        beforeX = x;
        beforeY = y;
        // �任����
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
                // ��д�����
                System.Random random = new System.Random();
                xIndex = random.Next(0, 3);
                yIndex = random.Next(0, 3);
            }
            x = xMove[xIndex];
            y = yMove[yIndex];
        }
        npcState = 0;
    }
    // �������壬�л�����
    public void RandomDirection()
    {
        // ��¼��һ������
        if (stayingTime <= 3)
        {
            beforeX = x;
            beforeY = y;
            // �任����
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
                    // ��д�����
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
    // �����任
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
    // ��npc����
    public void talkWithNpc()
    {
        if (touchPlayer == true && Input.GetKeyDown(KeyCode.E))
        {
            // ����ui�ͽ�ɫ����ʼ��һ��
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;

            var playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y, 0);
            //npc.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y, 0);
            // �򿪽������
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    // ��������ҵ�ʱ��
    public void TouchPlayer()
    {
        // ������ײģʽΪ��̬
        rigidbody.bodyType = RigidbodyType2D.Static;
        touchPlayerTime += Time.fixedDeltaTime;
        if (touchPlayerTime >= 5)
        {
            if (npcState != 3 || npcState != 4 || npcState != 5)
            {
                // ���npc״̬
                npcState = 0;
                touchPlayerTime = 0;
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }

        }
    }

    // ������ҶԻ�
    public void talkWithPlayer()
    {
        // ��ȡ�Ի���Ԥ����
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

    // �Ի�
    public void Talk()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        // ��ȡԤ����
        GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
        // �ж��Ƿ���ʵ�����ı���
        if (!dialogGameObject)
        {
            Instantiate(dialog);
        }
        // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
        dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        //showDialog = !showDialog;
        dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        // ����ı�
        string[] textContent;
        textContent = talkInfo();
        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
        //dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "ҩ��\nӪҵʱ��:10:00-20:00";
        // ÿ�ζԻ����øжȼ�1
        currentNpcFavor += 1;
        // ���״̬Ϊ����ҶԻ�
        if (isParty == true)
        {
            npcState = 5;
        }
        else
        {
            npcState = 3;
        }

    }
    // ���
    public void Party()
    {
        // �ж��Ƿ�Ϊ���״̬
        if (isParty == false)
        {
            // �øж�0-3�ܾ����
            if (npcFavorLevel < 4)
            {
                // ���״̬Ϊ�Ի�
                // ��ȡʵ�����������
                GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                // ��ȡԤ����
                GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
                // �ж��Ƿ���ʵ�����ı���
                if (!dialogGameObject)
                {
                    Instantiate(dialog);
                }
                // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
                dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                // ����ı�
                string[] textContent = new string[] { };
                System.Random random = new System.Random();
                int chooseText = random.Next(1, 4);
                switch (chooseText)
                {
                    case 1:
                        textContent = new string[] { "��Ǹ�����ǻ������" };
                        break;
                    case 2:
                        textContent = new string[] { "�����ں�æ���Ժ���Լ��" };
                        break;
                    case 3:
                        textContent = new string[] { "��Ǹ��һ����һ�����" };
                        break;
                }

                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                // ���״̬Ϊ����ҶԻ�
                npcState = 3;

            }
            else
            {
                // ��ȡʵ�����������
                GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                // ��ȡԤ����
                GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
                // �ж��Ƿ���ʵ�����ı���
                if (!dialogGameObject)
                {
                    Instantiate(dialog);
                }
                // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
                dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
                dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                // ����ı�
                string[] textContent;
                textContent = new string[] { "��������" };
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                // ���Ϊ���״̬
                npcState = 5;
                // ����bodyType
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                isParty = true;
            }
        }
        else
        {
            // ���Ϊ�Ի�״̬
            // ��ȡʵ�����������
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            // ��ȡԤ����
            GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
            // �ж��Ƿ���ʵ�����ı���
            if (!dialogGameObject)
            {
                Instantiate(dialog);
            }
            // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
            dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // ����ı�
            string[] textContent;
            textContent = new string[] { "�Ѿ�Ҫ������", "�Ǻð�", "�´����������" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // ���״̬Ϊ����ҶԻ�
            npcState = 3;
            // ���Ϊ�����״̬
            isParty = false;
        }


    }

    // ��������߼�
    public void PartyMove()
    {
        // �ж�����ҵľ���
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float xInstance = System.Math.Abs(player.position.x - gameObject.transform.position.x);
        float yInstance = System.Math.Abs(player.position.y - gameObject.transform.position.y);
        float xDirection = player.position.x - gameObject.transform.position.x;
        float yDirection = player.position.y - gameObject.transform.position.y;
        if (xInstance <= partyX && yInstance <= partyY)
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
                // ��������
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                // �������ƶ�

                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }

            if (yInstance > partyY)
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


    // �ټ�
    public void SayBye()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        // ��ȡԤ����
        GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
        // �ж��Ƿ���ʵ�����ı���
        if (!dialogGameObject)
        {
            Instantiate(dialog);
        }
        // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
        dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        // ����ı�
        string[] textContent;
        textContent = new string[] { "�ټ�!" };
        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
        // ���״̬Ϊ����ҶԻ�
        npcState = 3;
    }

    // ���öԻ�
    public string[] talkInfo()
    {
        string[] textContent = new string[0];
        // �Ƿ��һ�ζԻ�
        if (isFistTalk == true)
        {
            textContent = new string[] { "���", "û����������", "˵�������峤˵��һ���˻���������", "��������������", "�ҽ�" + name, "ף������������!" };
            isFistTalk = false;
        }
        else
        {
            // ���ǵ�һ�ζԻ������ݺøж������ı�
            switch (npcFavorLevel)
            {
                case 0:
                    textContent = new string[] { "��ʲô����?", "�������Ҫ����ȥ�Ҵ峤", "��������Щæ" };
                    break;
                case 1:
                    textContent = new string[] { "��,���㰡", "������Ҳ��������������", "����ܲ����" };
                    break;
                case 2:
                    textContent = new string[] { "����������������", "������ͦ��Ծ��", "��ʱ��Ļ�", "����Լ��һ����" };
                    break;
                case 3:
                    textContent = new string[] { "������ڴ�ҵ�ӡ��������", "̫����", "û�뵽���ܺܺõ���������", "�Ժ�����Ӷ�����" };
                    break;
                case 4:
                    textContent = new string[] { "��֪��Ϊʲô", "�о�������Ī��������", "....", "��...ûʲô", "��ʲô��û˵" };
                    break;

            }
        }
        return textContent;
    }
    // ����
    public void sendGifts() {
        // �ж��Ƿ�װ����Ʒ
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        if (equip.GetComponent<Image>().sprite == null)
        {
            // ��ȡʵ�����������
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            // ��ȡԤ����
            GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
            // �ж��Ƿ���ʵ�����ı���
            if (!dialogGameObject)
            {
                Instantiate(dialog);
            }
            // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
            dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // ����ı�
            string[] textContent;
            textContent = new string[] {"��δװ����Ʒ" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // ���״̬Ϊ����ҶԻ�
            npcState = 3;
        }
        else {
            // ִ�м�����Ʒ�ķ���
            useItem();
            // ��ȡʵ�����������
            GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            // ��ȡԤ����
            GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
            // �ж��Ƿ���ʵ�����ı���
            if (!dialogGameObject)
            {
                Instantiate(dialog);
            }
            // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
            dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // ����ı�
            string[] textContent;
            textContent = new string[] { "лл" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // ���״̬Ϊ����ҶԻ�
            npcState = 3;
        }
    }

    // ʹ����Ʒ
    public void useItem() {
        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        for (int i =0; i<playerBag.bag.Count;i++) {
            // �ж��Ƿ�Ϊͬһ������
            if (playerBag.bag[i].itemImage == equip.GetComponent<Image>().sprite) {
                if (playerBag.bag[i].itemNumber == 1)
                {
                    // ֻ��һ����Ʒ�����Ƴ�
                    // ������Ʒ
                    playerBag.bag.Remove(playerBag.bag[i]);
                    // ui����Ʒ��������
                    // ��ȡ����ui
                    GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
                    // ��ȡ����UI���������µ�����������
                    GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
                    // ����
                    for (int j = 0; j<bagGrid.transform.childCount;j++) {
                        if (equip.GetComponent<Image>().sprite == bagGrid.transform.GetChild(j).GetComponent<Image>().sprite) {
                            Destroy(bagGrid.transform.GetChild(j));
                        }
                    }
                    // ����װ��Ϊ�ر�״̬����״̬��ͼƬ���ÿ�
                    equip.GetComponent<Image>().sprite = null;
                    GridPrefab.clickItemIndex = -1;
                    // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
                    addToBag.RefreshItem();
                }
                else {
                    // �����������������һ
                    playerBag.bag[i].itemNumber -= 1;
                }
                // �������Ӻøж�
                currentNpcFavor += playerBag.bag[i].npcFavorNumber;
            }
        }
        //
    }

    // npc�øж�����ϵ���������øж�
    public void NpcFavorUpdate() { 
        maxNpcFavor = 10 + npcFavorLevel * (npcFavorLevel + 1) * npcFavorLevel * 5;
        if (currentNpcFavor >= maxNpcFavor)
        {
            // �жϵ�ǰ�����Ƿ���������
            if (currentNpcFavor > maxNpcFavor)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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

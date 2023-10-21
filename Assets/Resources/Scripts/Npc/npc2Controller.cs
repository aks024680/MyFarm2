using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc2Controller : MonoBehaviour
{
    // �ٶ�
    public float speed = 120;
    // ��ȡ����
    private Rigidbody2D rigidbody;
    // ������
    private Animator animator;
    // ����
    private Vector2 vector2;
    // ״̬ 0 �������� 1 ��������ײ 2 ��ײ������ 3 �Ի� 4 ���� 5���
    public int npcState = 0;
    // �೤ʱ��任npc���ƶ�����
    public float changeDirectionTime = 0;
    // ���ʱ��
    public float randomTime = 1;
    // ���� x -1 �� 0 ���� 1 �� Input.GetAxisRaw("Horizontal");
    public float[] xDirection = {-1,0,1 };
    // y yΪ-1������ 0 ���� yΪ1������ Input.GetAxisRaw("Vertical")
    public float[] yDirection = { -1, 0, 1 };
    // ���xy
    public int xIndex = 0;
    public int yIndex = 0;
    // ���յ�����
    public float x = 0;
    public float y = 0;
    // ǰһ��λ������
    public float beforeX = 0;
    public float beforeY = 0;

    //-----------��ײ-----------
    // ������ײʱ��
    public float stayingTime = 0;
    // ������ײ�������ִ��ʱ��
    public float touchNormalTime = 0;
    // ������ײ�������ִ��ʱ��
    public float touchStayTime = 0;
    // ����������ײ�������
    public int changeDirectionIndex = 0;
    //-----------��ײ------------

    // --------��ײ���
    // ��ײ���flag
    public bool touchPlayer = false;
    // ��ײ��ҵ�ʱ��
    public float touchPlayerTime = 0;
    //---------��ײ���

    // --------- �Ի�---------------
    public bool firstTalk = true;
    public string npcName = "xxxx";
    // ��ǰ�øж�
    public int npcFavorLevel = 0;
    // ���øжȵȼ�
    public int maxNpcFavorLevel = 5;
    // ��ǰ�øж�
    public int currentFavor = 0;
    // �����øж���Ҫ����ֵ
    public int maxFavor = 0;
    // --------- �Ի�---------------

    // ----------���---------------
    // �ж�������
    public bool isParty = false;
    // npc����ҵľ���
    public float partyX = 2;
    public float partyY = 2;
    // ----------���---------------

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
            // �������
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
        // �øж�������ϵ
        FavorUpdateLevel();
        // ����ui����
        ControllUI();
    }

    private void FixedUpdate()
    {
        // ������
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

        // ��ײ���
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        touchPlayerTime = 0;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        touchPlayer = false;
    }


    // npc������
    public void NpcController() {
        switch (npcState) {
            // ��������
            case 0:
                NormalMove();
                break;
            // ������ײ
            case 1:
                TouchMove();
                break;
            // �������ײ����
            case 2:
                touchWithPlayer();
                break;
            // �Ի�
            case 3:
                // ����Ҫ�κβ���
                // �ж϶Ի������޹ر�,�ر�֮����״̬Ϊ��������
                talkWithPlayer();
                break;
            // ����
            case 4:
                break;
            // ���
            case 5:
                // ����ƶ�
                patryMove();
                break;
        }
    }

    // �������ߵ�ʱ�򣬻���Ҫִ��ʲô
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
            // ���ǵ�xyͬʱ�ƶ����������
            while (xIndex !=1&&yIndex!=1) {
                xIndex = random.Next(0, 3);
                yIndex = random.Next(0, 3);
            }
            x = xDirection[xIndex];
            y = yDirection[yIndex];
            changeDirectionTime = 0;
        }
    }

    // �������
    public void ChangeAnim() {
        // xΪ-1������xΪ1������
        // yΪ-1�����£�yΪ1������
        // ����
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
            // ����
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
        // ����
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
            // ����
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
            // ֹͣ
            if (beforeX == 0 && beforeY == 0) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
                // ����
            } else if (beforeX == -1 && beforeY == 0) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", true);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                // ����
            } else if (beforeX == 1 && beforeY == 0) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", true);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                // ����
            } else if (beforeX == 0 && beforeY == 1) {
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", true);
                animator.SetBool("down", false);
                // ����
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

    // ��ײ
    public void TouchMove() {
        stayingTime += Time.fixedDeltaTime;
        touchNormalTime += Time.fixedDeltaTime;
        touchStayTime += Time.fixedDeltaTime;
        // ������
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
                        // ��д�����
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
            // �������
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

    // �������
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

    // �Ի�
    public void Talk() {
        if (firstTalk == true)
        {
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            GameObject dialogPrefab = Resources.Load<GameObject>("Dialog/DialogPrefab");
            if (!dialog)
            {
                Instantiate(dialogPrefab);
            }
            // ��ֵ
            dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "���", "û����������", "˵�������峤˵��һ���˻���������", "��������������", "�ҽ�" + npcName, "ף������������!" };
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
            // ��ֵ
            dialog = GameObject.FindGameObjectWithTag("DialogPrefab").gameObject;
            string[] textContent = new string[] { };
            // �����ı�
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

    // �Ի��ı�
    public string[] talkMessage() {
        string[] textContenxt = new string[] { };
        switch (npcFavorLevel)
        {
            case 0:
                textContenxt = new string[] { "��ʲô����?", "�������Ҫ����ȥ�Ҵ峤", "��������Щæ" };
                break;
            case 1:
                textContenxt = new string[] { "��,���㰡", "������Ҳ��������������", "����ܲ����" };
                break;
            case 2:
                textContenxt = new string[] { "����������������", "������ͦ��Ծ��", "��ʱ��Ļ�", "����Լ��һ����" };
                break;
            case 3:
                textContenxt = new string[] { "������ڴ�ҵ�ӡ��������", "̫����", "û�뵽���ܺܺõ���������", "�Ժ�����Ӷ�����" };
                break;
            case 4:
                textContenxt = new string[] { "��֪��Ϊʲô", "�о�������Ī��������", "....", "��...ûʲô", "��ʲô��û˵" };
                break;

        }
        return textContenxt;
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
    // �øж�������ϵ
    public void FavorUpdateLevel() {
        // ���øж���ֵ�䶯
        maxFavor = 10 + npcFavorLevel * (npcFavorLevel + 1) * 5;
        // ����
        // ��������øж�����
        if (currentFavor >= maxFavor)
        {
            // �жϵ�ǰ�����Ƿ���������
            if (currentFavor > maxFavor)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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

    // ����
    public void sendGifts()
    {
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        if (state.transform.GetChild(19).GetComponent<Image>().sprite == null)
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
            textContent = new string[] { "δװ����Ʒ" };
            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            // ���״̬Ϊ����ҶԻ�
            npcState = 3;
        }
        else
        {
            // ��������
            useItem();
            // ��ʾ�ı�
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

    public void useItem()
    {
        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        for (int i = 0; i < playerBag.bag.Count; i++)
        {
            // �ж��Ƿ�Ϊͬһ������
            if (playerBag.bag[i].itemImage == equip.GetComponent<Image>().sprite)
            {
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
                    for (int j = 0; j < bagGrid.transform.childCount; j++)
                    {
                        if (equip.GetComponent<Image>().sprite == bagGrid.transform.GetChild(j).GetComponent<Image>().sprite)
                        {
                            Destroy(bagGrid.transform.GetChild(j));
                        }
                    }
                    // ����װ��Ϊ�ر�״̬����״̬��ͼƬ���ÿ�
                    equip.GetComponent<Image>().sprite = null;
                    GridPrefab.clickItemIndex = -1;
                    // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
                    addToBag.RefreshItem();
                }
                else
                {
                    // �����������������һ
                    playerBag.bag[i].itemNumber -= 1;
                }
                // �������Ӻøж�
                currentFavor += playerBag.bag[i].npcFavorNumber;
            }
        }
        //
    }

    // ���
    public void party() {
        // �Ƿ�δ���״̬
        if (isParty == false)
        {
            // �øж�0��1��2��3�������
            if (npcFavorLevel < 4)
            {
                // ��ʾ�ı�
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
                npcState = 3;
            }
            else {
                // 4��5�����
                // ��ʾ�ı�
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
                string[] textContent = new string[] {"��������"};
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                npcState = 5;
                isParty = true;
            }
        }
        else {
            // ���
            // ��ʾ�ı�
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
            isParty = false;
            npcState = 3;
        }
    }

    // ����Ƿ��ƶ�
    public void patryMove() {
        // ���λ��
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        // npcλ��
        Transform npc = gameObject.transform;
        float xDistance = System.Math.Abs(player.position.x - npc.position.x);
        float yDistance = System.Math.Abs(player.position.y - npc.position.y);
        float checkX = player.position.x - npc.position.x;
        float checkY = player.position.y - npc.position.y;

        // ֹͣ�ƶ�
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
            // �������
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
            {
                // npc�������
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
                vector2.x = x;
                vector2.y = y;
                vector2.Normalize();
                rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
            }
            else {
                // �жϾ����Ƿ����޶���Χ
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

    // �����������
    public void ControllUI() {
        // �ڼ���û�а���E��
        if (touchPlayer == true && Input.GetKeyDown(KeyCode.E))
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            var playerPos = Camera.main.WorldToScreenPoint(player.position);
            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).position = new Vector3(playerPos.x, playerPos.y, 0);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }






}

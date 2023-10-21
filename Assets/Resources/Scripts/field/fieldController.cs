using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fieldController : MonoBehaviour
{
    // Start is called before the first frame update
    // �ж��Ƿ�����ֲ��
    public bool inFieldArea = false;
    // �ж��Ƿ�������ֲ
    public bool isField = false;
    // �ı�����ʾ����
    public bool showDialog = true;
    // ��ֲ��������
    public string name;
    // ��������
    public int circleDay;
    // ʣ�����ʱ��
    public int leaveTime;
    public float totalTime = 0;
    // ��ֲ����
    public int minute = 0;
    // ��ֲ������
    public int second = 0;
    // ����״̬���л�ͼƬ�������ڡ������ڡ������ڡ��Ѳ�ժ===>0,1,2,3������ˮ������ֹͣ������
    public int circleStatus=-1;
    // ÿ�콽ˮ����
    public int waterCount = 0;
    // ÿ����������
    public int landCount = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inFieldArea == true) {
            // ��ֲ
            Fielding();
        }
        // ��ȡ�Ի���Ԥ����
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == false)
        {
            showDialog = true;
        }
        else {
            showDialog = false;
        }
        // ������ֲ״̬
        updateFieldData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isField == false)
        {
            // δ��ֲ״̬����ʾ���������
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            // ��ֲ״̬��ʾ����ֲ����
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
            // δ��ֲ״̬����ʾ���������
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            // ��ֲ״̬��ʾ����ֲ����
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

    // ��д����������ֲ�Ͳ���
    public void Fielding() {
        // ��ȡ�Ի���Ԥ����
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        // �ж��Ƿ�δ��ֲ״̬
        if (isField == false)
        {
            // δ��ֲ״̬���ж��Ƿ�E���ҶԻ����ǹرյ�
            if (Input.GetKeyDown(KeyCode.E)&& showDialog==true) {
                // �ж��Ƿ�װ������
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                if (equip.GetComponent<Image>().sprite == null)
                {
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��δװ������" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    // �ж��Ƿ�װ����������
                    if (equip.GetComponent<Image>().sprite.name == "����")
                    {
                        // ������ֲ
                        System.Random random = new System.Random();
                        // ���������ֲ
                        string itemName = "";
                        // ��ȡ�����
                        int index = random.Next(0, 3);
                        // 
                        List<itemsCreations> seeds = new List<itemsCreations>();
                        itemsCreations ���տ� = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/�������տ�");
                        itemsCreations ���ܲ� = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/������ܲ�");
                        itemsCreations ��ݮ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/�����ݮ");
                        seeds.Add(���տ�);
                        seeds.Add(���ܲ�);
                        seeds.Add(��ݮ);
                        switch (index) {
                            case 0:
                                itemName = "�������տ�";
                                break;
                            case 1:
                                itemName = "������ܲ�";
                                break;
                            case 2:
                                itemName = "�����ݮ";
                                break;
                        }

                        // ������������һ
                        bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                        for (int i = 0;i<playerBag.bag.Count;i++) {
                            if (playerBag.bag[i].name == "����") {
                                // �ж������Ƿ�ֻ��һ����
                                if (playerBag.bag[i].itemNumber <= 1)
                                {
                                    // ֻ��һ������������Ʒ
                                    playerBag.bag.Remove(playerBag.bag[i]);
                                    // ����ui����������Ʒ
                                    // ��ȡ����ui
                                    GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
                                    // ��ȡ����UI���������µ�����������
                                    GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
                                    // ����
                                    for (int j = 0; j < bagGrid.transform.childCount; j++)
                                    {
                                        if (equip.GetComponent<Image>().sprite == bagGrid.transform.GetChild(j).GetComponent<Image>().sprite)
                                        {
                                            //����ui��Ʒ
                                            Destroy(bagGrid.transform.GetChild(j));
                                        }
                                    }
                                    // ����װ��ͼƬΪ��
                                    equip.GetComponent<Image>().sprite = null;
                                    //װ����ʶδδװ��״̬
                                    GridPrefab.clickItemIndex = -1;
                                    // ˢ�±���
                                    // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
                                    addToBag.RefreshItem();
                                }
                                else {
                                    // ������������������һ
                                    playerBag.bag[i].itemNumber -= 1;
                                }
                            }
                        }

                        // ����ֲ״̬���Ϊ����ֲ
                        isField = true;
                        // ��ʾ��Ϣ
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "����ֲ��"+itemName.Replace("����","") };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        // ����������ֲ�����Ϣ
                        name = itemName.Replace("����", "");
                        // ��������/��
                        circleDay = 7;
                        // ʣ��ʱ��
                        leaveTime = 7;
                        // ��ֲʱ��
                        // ��ȡʱ��ϵͳ�ķֺ���
                        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
                        minute = timeSystem.showMinute;
                        second = timeSystem.showSeconds;
                        // ����ͼƬ
                        string updateImageName = "";
                        switch (itemName) {
                            case "�������տ�":
                                updateImageName = "���տ�";
                                break;
                            case "������ܲ�":
                                updateImageName = "���ܲ�";
                                break;
                            case "�����ݮ":
                                updateImageName = "��ݮ";
                                break;
                        }
                        Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/"+updateImageName);
                        // �������״̬
                        circleStatus = 0;
                        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[circleStatus];
                    }
                    else {
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��װ������Ʒ��������" };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }
        else {
            // ��ˮʩ��
            TakeCareOfSeed();
        }
    }

    // ��ˮʩ��
    public void TakeCareOfSeed() {
        //�жϰ��°�����������ֲ״̬�����ǶԻ���Ϊ�ر�״̬
        if (Input.GetKeyDown(KeyCode.E) && isField == true && showDialog == true) {
            // �ж��Ƿ����
            if (circleStatus == 3)
            {
                // ���л�ȡ��Ʒ
                bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                // �жϱ����Ƿ��Ѿ��и���Ʒ
                int itemCount = 0;
                for (int i = 0; i<playerBag.bag.Count;i++) {
                    if (playerBag.bag[i].itemName == "����"+name) {
                        itemCount++;
                    }
                }
                // ��ȡ��Ҫ������ұ�������Ʒ
                itemsCreations item = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/"+"����"+name);
                if (itemCount == 0)
                {
                    playerBag.bag.Add(item);
                }
                else {
                    for (int i = 0; i < playerBag.bag.Count; i++)
                    {
                        if (playerBag.bag[i].itemName == "����" + name)
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
                // ˢ�±���
                addToBag.RefreshItem();
                // ���ͼƬ
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                // ���Ϊδ��ֲ״̬
                isField = false;
                circleStatus = -1;
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
                textContent = new string[] { "���ջ��˳���"+name };
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;

            }
            else {
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                // �ж��Ƿ�װ����ˮ�����߳�ͷ
                if (equip.GetComponent<Image>().sprite.name == null && equip.GetComponent<Image>().sprite.name != "ˮ��" && equip.GetComponent<Image>().sprite.name != "��ͷ")
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
                    textContent = new string[] { "��δװ��ˮ�����ͷ" };
                    dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                }
                else {
                    if (equip.GetComponent<Image>().sprite.name.Contains("ˮ��")) {
                        // �жϵ��ս�ˮ�����Ƿ�����
                        if (waterCount == 1)
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
                            textContent = new string[] { "�����ѽ�ˮ" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                        else {
                            // ��ˮ������1
                            waterCount += 1;
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
                            textContent = new string[] { "��ˮ�ɹ�!" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                    }
                    if (equip.GetComponent<Image>().sprite.name == "��ͷ")
                    {
                        // �жϵ������������Ƿ�����
                        if (landCount == 1)
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
                            textContent = new string[] { "����������" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                        else
                        {
                            // ����������1
                            landCount += 1;
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
                            textContent = new string[] { "�����ɹ�!" };
                            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                        }
                    }
                }
            }
        }
    }
    // ��д�������и�����ֲ�������
    public void updateFieldData() {
        // ����ֲ״̬�Ÿ���
        if (isField == false) {
            return;
        }
        // ����ʣ��ʱ��
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        if (timeSystem.showMinute == 0 && timeSystem.showSeconds == 0)
        {
            // ���½�ˮ����������
            waterCount = 0;
            landCount = 0;
            // �ж�ʣ��ʱ���Ƿ�Ϊ0
            if (leaveTime == 0)
            {
                // �ж�����״̬�Ƿ�Ϊ3��Ϊ3������
                if (circleStatus == 3)
                {
                    return;
                }
                // �жϷֺ����Ƿ����ֲ�ķֺ������
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // ����ʣ��ʱ��
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "�ѳ���";
                    // ��������״̬Ϊ��ժ��
                    circleStatus = 3;
                }
                else
                {
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = System.Math.Abs(timeSystem.showMinute - minute) + "Сʱ";
                }
            }
            else
            {
                totalTime += Time.deltaTime;
                if (totalTime >=0.7) {
                    leaveTime -= 1;
                    totalTime = 0;
                }
                
                // ����ʣ��ʱ��
                gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = leaveTime + "��";
                // ��������״̬
                switch (leaveTime)
                {
                    case 4:
                        // ������
                        circleStatus = 1;
                        break;
                    case 2:
                        // ������
                        circleStatus = 2;
                        break;
                }
                
            }
        }
        else {
            // �������
            // �ж�ʣ��ʱ���Ƿ�Ϊ0
            if (leaveTime == 0)
            {
                // �ж�����״̬�Ƿ�Ϊ3��Ϊ3������
                if (circleStatus == 3)
                {
                    return;
                }
                // �жϷֺ����Ƿ����ֲ�ķֺ������
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // ����ʣ��ʱ��
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "�ѳ���";
                    // ��������״̬Ϊ��ժ��
                    circleStatus = 3;
                }
                else
                {
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = System.Math.Abs(timeSystem.showMinute - minute) + "Сʱ";
                }
            }
            else
            {
                // ����ʣ��ʱ��
                gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = leaveTime + "��";
                // ��������״̬
                switch (leaveTime)
                {
                    case 4:
                        // ������
                        circleStatus = 1;
                        break;
                    case 2:
                        // ������
                        circleStatus = 2;
                        break;
                }
            }
        }
        // ��������
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = name;
        // ������������
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(7).GetComponent<Text>().text = circleDay + "��";
        // ���½�ˮ����������
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = waterCount + "/1";
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(5).GetComponent<Text>().text = landCount + "/1";
        Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/" + name);
        // ����ͼƬ
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

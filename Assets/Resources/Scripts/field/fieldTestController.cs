using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fieldTestController : MonoBehaviour
{
    // �ж��Ƿ�����ֲ��
    public bool isInField = false;
    // �ж��Ƿ�Ի�������ʾ
    public bool showDialog = true;
    // �ж��Ƿ��Ѿ���ֲ
    public bool isFielding = false;
    // ��ֲ��
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
    public int circleStatus = -1;
    // ÿ�콽ˮ����
    public int waterCount = 0;
    // ÿ����������
    public int landCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �Ի�������
        DialogShow();
        // ��ֲ
        field();
        // ������ֲ����
        updateFiledData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInField = true;
        // �����Ƿ���ֲ��ʾ���
        if (isFielding == false)
        {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInField = true;
        // �����Ƿ���ֲ��ʾ���
        if (isFielding == false)
        {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInField = false;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    // ���ƶԻ�������
    public void DialogShow() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).GetChild(0).gameObject;
        if (dialog.activeSelf == false)
        {
            showDialog = true;
        }
        else {
            showDialog = false;
        }
    }

    // ��ֲ
    public void field() {
        // ���°���������ֲ�����Ի���δ��ʾ
        if (Input.GetKeyDown(KeyCode.E)&& isInField == true && showDialog == true) {
            // �Ƿ���ֲ
            if (isFielding == false)
            {
                // ��װ������
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "����")
                {
                    // ��ȡ�Ի���Ԥ����
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��δװ������" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    // ����������
                    // ���ݱ�����������ȥ���ٻ�������
                    // ����ui�����Ӵ���
                    // ״̬uiװ����ͼƬ����
                    // װ��״̬����Ϊδװ��
                    // ������������һ// 
                    bagCreations playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
                    for (int i = 0; i < playerBag.bag.Count; i++)
                    {
                        if (playerBag.bag[i].name == "����")
                        {
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
                            else
                            {
                                // ������������������һ
                                playerBag.bag[i].itemNumber -= 1;
                            }
                        }
                    }

                    // ��ȡ��Ʒ������ֲ
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
                    switch (index)
                    {
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
                    // �����ݽ��и���
                    // ����ֲ״̬���Ϊ����ֲ
                    isFielding = true;
                    // ��ʾ��Ϣ
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "����ֲ��" + itemName.Replace("����", "") };
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
                    switch (itemName)
                    {
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
                    Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/" + updateImageName);
                    // �������״̬
                    circleStatus = 0;
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[circleStatus];
                }
            }
            else {
                // ��ˮ����
                waterAndLand();
            }
        }
    }

    // ��ˮ����
    public void waterAndLand() {
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        if (equip.GetComponent<Image>().sprite == null && equip.GetComponent<Image>().sprite.name != "��ͷ" && equip.GetComponent<Image>().sprite.name.Contains("ˮ��"))
        {
            // ��ȡ�Ի���Ԥ����
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
            dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "û��װ��ˮ�����ͷ" };
            dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else {
            // 
            if (equip.GetComponent<Image>().sprite.name == "��ͷ") {
                if (landCount == 1)
                {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "����������" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    landCount++;
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "�����ɹ�!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }

            if (equip.GetComponent<Image>().sprite.name.Contains("ˮ��"))
            {
                if (waterCount == 1)
                {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "�����ѽ�ˮ" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    waterCount++;
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��ˮ�ɹ�!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    // ��������
    public void updateFiledData() {
        if (isFielding == false) {
            return;
        }
        TimeSystemContoller timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        // ʣ��ʱ�������״̬
        if (timeSystem.showMinute == 0 && timeSystem.showSeconds == 0)
        {
            if (leaveTime == 0)
            {
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // ��ժ��
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "�ѳ���";
                    circleStatus = 3;
                }
                else {
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = System.Math.Abs(timeSystem.showMinute - minute) + "Сʱ";
                }
            }
            else {
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
            if (leaveTime == 0)
            {
                if (timeSystem.showMinute == minute && timeSystem.showSeconds == second)
                {
                    // ��ժ��
                    gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(9).GetComponent<Text>().text = "�ѳ���";
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

        // 
        // ��������
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = name;
        // ������������
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(7).GetComponent<Text>().text = circleDay + "��";
        // ���½�ˮ����������
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(3).GetComponent<Text>().text = waterCount + "/1";
        gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(5).GetComponent<Text>().text = landCount + "/1";
        Object[] sprites = Resources.LoadAll<Sprite>("Prefabs/bag/items/field/image/" + name);
        // ����ͼƬ
        switch (circleStatus)
        {
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

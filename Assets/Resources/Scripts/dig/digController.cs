using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class digController : MonoBehaviour
{
    // ��ǰ�Ѿ��ڿ�Ĵ���
    public int currentDigCount = 0;
    // ÿ����ڿ�Ĵ���
    public int maxDigCount = 5;
    // �ж��Ƿ����ڿ�Χ
    public bool isDigArea = false;
    // ���ƶԻ�����ʾ
    public bool showDialog = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDigArea = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // ��ȡ�Ի���
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true)
        {
            isDigArea = false;
        }
        else {
            isDigArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDigArea = false;
    }



    // Update is called once per frame
    void Update()
    {
        // �����ڿ����
        reSetDigCount();
        // �ڿ�
        diging();
        // ���ƶԻ�������
        // ��ȡ�Ի���
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == true)
        {
            showDialog = false;
        }
        else
        {
            showDialog = true;
        }
    }

    // 00:00�����ڿ����
    public void reSetDigCount() {
        // ��ȡʱ��ϵͳ
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        if (timeSystem.GetComponent<TimeSystemContoller>().showMinute == 0 && timeSystem.GetComponent<TimeSystemContoller>().showSeconds == 0) {
            // 00:00�����ڿ����
            currentDigCount = 0;
        }
    }

    // �ڿ�
    public void diging() {
        // �ж��Ƿ����ڿ�������
        if (isDigArea == false) {
            return;
        }
        // �ж��Ƿ���װ������
        // ��ȡװ����
        GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
        if (equip.GetComponent<Image>().sprite != null)
        {
            // �ж��Ƿ�Ϊ����
            if (equip.GetComponent<Image>().sprite.name == "��ͷ")
            {
                if (Input.GetKeyDown(KeyCode.E) && isDigArea == true && showDialog == true) {
                    // �жϴ����Ƿ�����
                    if (currentDigCount == maxDigCount)
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
                        textContent = new string[] { "�����ڿ�����Ѿ�����" };
                        dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                    }
                    else {
                        // �ڿ������1
                        currentDigCount++;
                        // ��ȡ�ڿ������Ʒ
                        List<itemsCreations> digList = new List<itemsCreations>();
                        itemsCreations ʯͷ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/ʯͷ");
                        itemsCreations �챦ʯ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/�챦ʯ");
                        itemsCreations �̱�ʯ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/�̱�ʯ");
                        itemsCreations ����ʯ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/����ʯ");
                        itemsCreations �Ʊ�ʯ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/�Ʊ�ʯ");
                        itemsCreations ��ʯ = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/��ʯ");
                        digList.Add(ʯͷ);
                        digList.Add(�챦ʯ);
                        digList.Add(�̱�ʯ);
                        digList.Add(����ʯ);
                        digList.Add(�Ʊ�ʯ);
                        digList.Add(��ʯ);
                        // �����ȡ��Ʒ
                        System.Random random = new System.Random();
                        // �趨��������
                        int rate = random.Next(1,101);
                        // �趨������Ʒ
                        string itemName = null;
                        if (rate <= 80)
                        {
                            // ʯͷ
                            itemName = "ʯͷ";
                        }
                        else if (rate > 80 && rate <= 85)
                        {
                            // �챦ʯ
                            itemName = "�챦ʯ";
                        }
                        else if (rate > 85 && rate <= 90)
                        {
                            // �̱�ʯ
                            itemName = "�̱�ʯ";
                        }
                        else if (rate > 90 && rate <= 95)
                        {
                            // �Ʊ�ʯ
                            itemName = "�Ʊ�ʯ";
                        }
                        else if (rate > 95 && rate <= 99)
                        {
                            // ����ʯ
                            itemName = "����ʯ";
                        }
                        else {
                            // ��ʯ
                            itemName = "��ʯ";
                        }
                        // 
                        // ����Ӧ����Ʒʵ����
                        // ����������ɵ�λ��

                        float x = random.Next(15, 20);
                        float y = random.Next(8,11);
                        GameObject item = Resources.Load<GameObject>("Prefabs/bag/items/dig/item/" + itemName);
                        Instantiate(item, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0));
                    }
                }
            }
            else {
                if (Input.GetKeyDown(KeyCode.E) && isDigArea == true && showDialog == true) {
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
                    textContent = new string[] { "��δװ������" };
                    dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
                }
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.E) && isDigArea == true && showDialog == true) {
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
                textContent = new string[] { "��δװ������" };
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigTestController : MonoBehaviour
{
    // Start is called before the first frame update
    // �����Ѿ��ڿ�Ĵ���
    public int currentCount = 0;
    // ÿ������ڿ����
    public int maxCount = 5;
    // �ж��Ƿ����ڿ���
    public bool isInDig = false;
    // ���ƶԻ�������
    public bool showDialog = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �����ڿ����
        reSetDigCount();
        // �ڿ�
        digging();
        // ���ƶԻ�������
        DialogControll();
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInDig = true;
    }
    // 
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInDig = false;
    }
    // ÿ��00:00�ָ��ڿ����
    public void reSetDigCount() {
        GameObject timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        if (timeSystem.GetComponent<TimeSystemContoller>().showMinute == 0 && timeSystem.GetComponent<TimeSystemContoller>().showSeconds == 0) {
            currentCount = 0;
        }
    }
    // 
    public void DialogControll() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).GetChild(0).gameObject;
        if (dialog.activeSelf == true)
        {
            showDialog = false;
        }
        else {
            showDialog = true;
        }

    }
    // �ڿ�ķ���
    public void digging() {
        if (Input.GetKeyDown(KeyCode.E)&& isInDig == true && showDialog == true) {
            // �Ƿ���װ����Ʒ
            GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
            // �ж��Ƿ�����ڿ�
            if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "��ͷ")
            {
                print(111);
                // ��ȡ�Ի���
                GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).gameObject;
                dialog.GetComponent<DialogPrefab>().message = new string[] {"��δװ����ͷ!" };
                dialog.transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                // �ڿ�
                if (currentCount == maxCount)
                {
                    GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab").transform.GetChild(0).gameObject;
                    dialog.GetComponent<DialogPrefab>().message = new string[] { "�㵱���ڿ�������þ�!" };
                    dialog.transform.GetChild(0).gameObject.SetActive(true);
                }
                else {
                    currentCount++;
                    // ��Ʒ����
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
                    int rate = random.Next(1, 101);
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
                    else
                    {
                        // ��ʯ
                        itemName = "��ʯ";
                    }
                    // ��Ʒʵ����

                    float x = random.Next(15, 20);
                    float y = random.Next(8, 11);
                    //Prefabs/bag/items/dig/item/�̱�ʯ
                    GameObject item = Resources.Load<GameObject>("Prefabs/bag/items/dig/item/" + itemName);
                    Instantiate(item, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0));

                }
            }
        }
    }
}

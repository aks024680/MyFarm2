using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class getItem : MonoBehaviour
{
    // ���������ڻ�ȡ��Ʒ

    // ��ȡ��ǰ���ؽű�������
    private Transform thisObject;
    // ��ǰ��ײ����Ʒ����
    private itemsCreations thisItem;
    // ��ӵ���Ӧ�ı���
    private bagCreations playerBag;
    // ��Ʒ���Ե��ӵ��������
    public static int maxCumulative = 10;


    void Start()
    {
        // ͨ�����ƻ�ȡ��Ӧ����Ʒ
        thisObject = GetComponent<Transform>();
        if (thisObject.name.Contains("(") && thisObject.name.Contains(" ")) {
            string[] strArr = thisObject.name.Split(new char[] { ' ' });
            thisObject.name = strArr[0];
        }
        if (thisObject.name.Contains("(") && !thisObject.name.Contains(" ")) {
            string[] strArr = thisObject.name.Split(new char[] { '(' });
            thisObject.name = strArr[0];
        }

        thisItem = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/" + thisObject.name);
        playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");

    }


    // ��ײ����⣬�����Ʒ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            AddNewItem(thisItem,playerBag);
        }
    }

    private void Update()
    {

    }


    // �������Ʒ����Ʒ����
    public void AddNewItem(itemsCreations thisItem,bagCreations playerBag) {
        // ��ֵͼƬ
        //thisItem.itemImage = thisObject.GetComponent<Image>().sprite;
        // ����û�и���Ʒ������ӣ������������
        // �����������ͬ��
        int sameItemCount = 0;
        for (int i = 0; i < playerBag.bag.Count; i++)
        {
            if (thisItem.name == playerBag.bag[i].itemName)
            {
                sameItemCount++;
            }
        }
        if (sameItemCount==0)
        {

            // ��ui�����Ʒ���жϱ��������Ƿ�����
            if (playerBag.bag.Count < 10)
            {
                // �����������Ʒ
                playerBag.bag.Add(thisItem);
                //addToBag.CreateNewItem(thisItem);
                Destroy(thisObject.gameObject);
            }
            else {
                // ��ʾ�ı��򱳰�����
                print("��������");
                //showBagFullMessage();
                ShowBagFull();
            }

        }
        else
        {
            // �жϱ����Ƿ�����
            if (playerBag.bag.Count < 10)
            {
                // �ж��Ƿ���Ե���
                if (thisItem.cumulative)
                {
                    // ÿ10����Ʒһ�� 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // �ۼ���������
                        // ��������
                        playerBag.bag.Add(thisItem);
                        // ������1
                        thisItem.itemNumber += 1;
                        Destroy(thisObject.gameObject);
                    }
                    else {
                        thisItem.itemNumber += 1;
                        Destroy(thisObject.gameObject);
                    }
                }
                else {
                    // ������ܵ��ӣ�����
                    playerBag.bag.Add(thisItem);
                    Destroy(thisObject.gameObject);
                }
            }
            else {
                // ���������˾Ͳ����������ж��Ƿ�����ۼ�
                if (thisItem.cumulative)
                {
                    // ÿ10����Ʒһ�� 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // �ۼ���������
                        // ������ʾ��������
                        //playerBag.bag.Add(thisItem);
                        print("��Ʒ�ۼ���������");
                        //showBagFullMessage();
                        ShowBagFull();
                    }
                    else
                    {
                        thisItem.itemNumber += 1;
                        Destroy(thisObject.gameObject);
                    }
                }
                else {
                    print("��������");
                    //showBagFullMessage();
                    ShowBagFull();
                }
            }
        }
        
        addToBag.RefreshItem();
    }

    // ��ӵ�����Ʒ

    // �������Ʒ����Ʒ����
    public int AddNewFishItem(itemsCreations thisItem, bagCreations playerBag)
    {
        // ��������жϱ����Ƿ����� Ϊ0δ����Ϊ1����
        int isFull = 0;
        // ��ֵͼƬ
        //thisItem.itemImage = thisObject.GetComponent<Image>().sprite;
        // ����û�и���Ʒ������ӣ������������
        // �����������ͬ��
        int sameItemCount = 0;
        for (int i = 0;i<playerBag.bag.Count;i++) {
            if (thisItem.name == playerBag.bag[i].itemName) {
                sameItemCount++;
            }
        }
        if (sameItemCount==0)
        {

            // ��ui�����Ʒ���жϱ��������Ƿ�����
            if (playerBag.bag.Count < 10)
            {
                // �����������Ʒ
                playerBag.bag.Add(thisItem);
                //addToBag.CreateNewItem(thisItem);
                //Destroy(thisObject.gameObject);
            }
            else
            {
                // ��ʾ�ı��򱳰�����
                print("��������1");
                //showBagFullMessage();
                //ShowBagFull();
                isFull = 1;
            }

        }
        else
        {
            // �жϱ����Ƿ�����
            if (playerBag.bag.Count < 10)
            {
                // �ж��Ƿ���Ե���
                if (thisItem.cumulative)
                {
                    // ÿ10����Ʒһ�� 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // �ۼ���������
                        // ��������
                        playerBag.bag.Add(thisItem);
                        // ������1
                        thisItem.itemNumber++;
                        //Destroy(thisObject.gameObject);
                    }
                    else
                    {
                        thisItem.itemNumber++; ;
                        //Destroy(thisObject.gameObject);
                    }
                }
                else
                {
                    // ������ܵ��ӣ�����
                    playerBag.bag.Add(thisItem);
                    //Destroy(thisObject.gameObject);
                }
            }
            else
            {
                // ���������˾Ͳ����������ж��Ƿ�����ۼ�
                if (thisItem.cumulative)
                {
                    // ÿ10����Ʒһ�� 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // �ۼ���������
                        // ������ʾ��������
                        //playerBag.bag.Add(thisItem);
                        print("��Ʒ�ۼ���������2");
                        //showBagFullMessage();
                        // ShowBagFull();
                        isFull = 1;
                    }
                    else
                    {
                        thisItem.itemNumber ++;
                        //Destroy(thisObject.gameObject);
                    }
                }
                else
                {
                    print("��������3");
                    //showBagFullMessage();
                    //ShowBagFull();
                    isFull = 1;
                }
            }
        }

        addToBag.RefreshItem();
        return isFull;
    }


    // ��ʾ��������
    public void showBagFullMessage() {
        // ��ȡ����������ʾ��
        GameObject bagFull = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(1).gameObject;
        bagFull.SetActive(true);
    }

    // ����������Ϣ��ʾ
    public void ShowBagFull() {
        // ��ȡ������������ʾ
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��������" };
        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

}

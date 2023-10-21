using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addToBag : MonoBehaviour
{
    // ���ű����ڽ���Ʒ��ӵ�����UI��
    static addToBag toBag;

    // ��ʼ��
    private void Awake()
    {
        if (toBag != null) {
            Destroy(this);
        }
        toBag = this;
    }

    private void Update()
    {
        // ������Ʒ������ʾ
        changeItemNumberShow();
        // ��ʾװ��ͼ��
         showEquipItem();
        //showEquipItemTest();
    }


    // ��������״̬
    private void OnEnable()
    {
        RefreshItem();
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        Grid.transform.GetChild(2).GetComponent<Text>().text = "";
        // �������ذ�ť�������Ƿ��ܹ�װ����ʹ�ã�������

    }
    public static void showItem(itemsCreations item,int index) {
        // ��ȡ��bag��ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        Grid.transform.GetChild(2).GetComponent<Text>().text = item.itemScription;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        for (int i = 0; i < bagGrid.transform.childCount; i++)
        {
            if (i == index)
            {
                if (item.equip == true)
                {
                    Grid.transform.GetChild(1).GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
                if (item.use == true) {
                    Grid.transform.GetChild(1).GetChild(i).GetChild(2).gameObject.SetActive(true);
                }
                Grid.transform.GetChild(1).GetChild(i).GetChild(3).gameObject.SetActive(true);
            }
            else {
                Grid.transform.GetChild(1).GetChild(i).GetChild(1).gameObject.SetActive(false);
                Grid.transform.GetChild(1).GetChild(i).GetChild(2).gameObject.SetActive(false);
                Grid.transform.GetChild(1).GetChild(i).GetChild(3).gameObject.SetActive(false);
            }
        }

            
        
    }


    // ��������Ʒ����,Ϊ���ñ������ã���Ҫ��̬static,����ʵ��������
    public static void CreateNewItem(itemsCreations item) {
        // ��ȡ������Ԥ����
        GridPrefab GridPrefabs = Resources.Load<GridPrefab>("Prefabs/bag/grid");
        // ��ȡ����ui��������
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // ��������Ʒ  ��Ҫ��������Ʒ��������Ʒ����������,�Ƕ�
        GridPrefab newItem = Instantiate(GridPrefabs, bagGrid.transform.position,Quaternion.identity);
        // �����´�����Ʒ�ĸ�������
        newItem.gameObject.transform.SetParent(bagGrid.transform);
        // ��ֵ��Ʒ
        newItem.item = item;
        newItem.itemImage.sprite = item.itemImage;
        newItem.itemNum.text = item.itemNumber.ToString();

    }

    // ˢ����Ʒ
    public static void RefreshItemTest() {
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ����UI����������������,��������������
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        
            for (int i = 0; i < bagGrid.transform.childCount; i++)
            {
            if (bagGrid.transform.childCount == 0)
            {
                break;
            }
            else {
                Destroy(bagGrid.transform.GetChild(i).gameObject);
            }

                
            }
        // ��ȡ��ұ���
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // ���������ٵ���������ӻ����ﵽˢ��Ŀ��
        for (int i =0; i< bagList.bag.Count;i ++) {
            print("��"+i+"��");
            CreateNewItem(bagList.bag[i]);
        }

        
    }





    // ������Ʒ������ʾ
    public static void changeItemNumberShow() {
        // ��ȡ��ұ���
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // ��ȡ��ȡ��Ʒ�������ۼ�����
        //getItem getItems = new getItem();
        // ���������ͬ����Ʒ���±�
        List<int> someItemIndex = new List<int>();
        // ���ڱ�����ͱ������ݵ�˳��һ������������
        for (int i = 0; i< bagList.bag.Count;i++) {
            if (bagList.bag.Count == 0)
            {
                break;
            }
            else {
                for (int j = 0; j< bagList.bag.Count;j++) {
                    // �����Ʒ��ͬ�����±���뼯��
                    if (bagList.bag[i] == bagList.bag[j]) {
                        someItemIndex.Add(j);
                    }
                }
                // �ж��ظ�����Ʒ�Ƿ�Ψһ
                if (someItemIndex.Count != 1) {
                    if (bagList.bag[i].cumulative == true)
                    {
                        for (int k = 0; k < someItemIndex.Count; k++)
                        {
                            // �������һ���ظ���Ʒ
                            if (k != someItemIndex.Count - 1)
                            {
                                bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                            }
                            else
                            {
                                // �ж������Ƿ�Ϊ0
                                if (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative == 0)
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                                }
                                else
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                }
                            }
                        }
                    }

                }

                // ���ü���
                someItemIndex.Clear();
            }
        }
    }
    public static void changeItemNumberShowTest()
    {
        // ��ȡ��ұ���
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // ��ȡ��ȡ��Ʒ�������ۼ�����
        //getItem getItems = new getItem();
        // ���������ͬ����Ʒ���±�
        List<int> someItemIndex = new List<int>();
        // ���ڱ�����ͱ������ݵ�˳��һ������������
        for (int i = 0; i < bagList.bag.Count; i++)
        {
            if (bagList.bag.Count == 0)
            {
                break;
            }
            else
            {
                for (int j = 0; j < bagList.bag.Count; j++)
                {
                    // �����Ʒ��ͬ�����±���뼯��
                    if (bagList.bag[i] == bagList.bag[j])
                    {
                        someItemIndex.Add(j);
                    }
                }
                // �ж��ظ�����Ʒ�Ƿ�Ψһ
                if (someItemIndex.Count != 1)
                {
                    if (bagList.bag[i].cumulative == true)
                    {
                        for (int k = 0; k < someItemIndex.Count; k++)
                        {
                            // �������һ���ظ���Ʒ
                            if (k != someItemIndex.Count - 1)
                            {
                                bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                            }
                            else
                            {
                                // �ж������Ƿ�Ϊ0
                                if (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative == 0)
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                                }
                                else
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                }
                            }
                        }
                    }

                }

                // ���ü���
                someItemIndex.Clear();
            }
        }
    }
    public static void RefreshItem()
    {
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ����UI����������������,��������������
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;

        for (int i = 0; i < bagGrid.transform.childCount; i++)
        {
            if (bagGrid.transform.childCount == 0)
            {
                break;
            }
            else
            {
                Destroy(bagGrid.transform.GetChild(i).gameObject);
            }


        }
        // ��ȡ��ұ���
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // ���������ٵ���������ӻ����ﵽˢ��Ŀ��
        for (int i = 0; i < bagList.bag.Count; i++)
        {
            CreateNewItem(bagList.bag[i]);
        }
        // ������������ʾ

        
    }

    // ����װ����Ʒ��װ����ʶ����ʾ
    public static void showEquipItem() {
        // ��ȡ����ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ״̬ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // �ж��±��Ƿ�Ϊ-1
        if (GridPrefab.clickItemIndex == -1)
        {
            
            // ��װ���ĵ�������null
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = null;
            // ʧ��װ����
            state.transform.GetChild(19).gameObject.SetActive(false);
        }
        else {
            // ��ʾװ����ʶ
            bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.SetActive(true);
            print("װ����ʶ:"+ bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.activeSelf);
            // ��װ���ĵ�����ӵ�״̬��
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetComponent<Image>().sprite;
            // ����װ����
            state.transform.GetChild(19).gameObject.SetActive(true);
        }
    }

    // ������ʾ-��װ����Ʒ��ʾ
    public static void showEquipItemTest() {
        // ��ȡ����ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ״̬ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 
        if (GridPrefab.clickItemIndex == -1)
        {
            // ��ʾ����װ����ʶ
            bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.SetActive(false);
            // ��ֵͼƬ
            state.transform.GetChild(19).GetComponent<Image>().sprite = null;
            // ��ʾװ��
            state.transform.GetChild(19).gameObject.SetActive(false);
        }
        else {
            // ��ʾ����װ����ʶ
            bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.SetActive(true);
            // ��ֵͼƬ
            state.transform.GetChild(19).GetComponent<Image>().sprite = bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetComponent<Image>().sprite;
            // ��ʾװ��
            state.transform.GetChild(19).gameObject.SetActive(true);

        }
    }
}

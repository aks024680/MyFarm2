using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridPrefab : MonoBehaviour
{
    // ���ű����ڽ�Ԥ���������Ϣ���и���
    // 
    // ��ȡitemsCreations���͵���Ʒ
    public itemsCreations item;
    // ��ȡ��Ҫ���µ�ͼƬ���
    public Image itemImage;
    // ��ȡ��Ҫ���µ��ı������µ���������Ʒ������
    public Text itemNum;
    // ���浱ǰװ����Ʒ���±�
    public static int clickItemIndex = -1;


    // Update is called once per frame
    void Update()
    {
        
    }

    // �����ʾ����
    public void clickItemToShowDetails() {
        // ��ȡ�������Ʒ���±�
        int index = transform.GetSiblingIndex();
        addToBag.showItem(item,index);
    }
    // ���װ����ť
    public void ItemEquip() {
        // ��ȡ�������Ʒ���±�
        int index = transform.GetSiblingIndex();
        
        // ��ȡ����ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ״̬ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // ��ʾװ����ʶ�����������Ʒ��װ����ʶ
        for (int i = 0; i<bagGrid.transform.childCount;i++) {
            // �ж��Ƿ�Ϊ�Լ��������Ʒ���Ҳ����ظ������װ������Ʒ
            if (i == index && i != clickItemIndex)
            {
                // ��ʾװ����ʶ
                bagGrid.transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                // �����±�
                clickItemIndex = index;
            }
            // �ж��Ƿ�Ϊ�Լ��������Ʒ�������ظ������װ������Ʒ
            else if (i == index && i== clickItemIndex) {
                // ����ʾ
                bagGrid.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                // �����±�
                clickItemIndex = -1;
            }
            else {
                // �ر�װ����ʶ
                bagGrid.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
            }
        }
        // �����±�����װ����Ʒ״̬
        if (clickItemIndex == -1)
        {
            // ��װ���ĵ�����ӵ�״̬��
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = null;
            // ����װ����
            state.transform.GetChild(19).gameObject.SetActive(false);
        }
        else {
            // ��װ���ĵ�����ӵ�״̬��
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = bagGrid.transform.GetChild(clickItemIndex).GetComponent<Image>().sprite;
            // ����װ����
            state.transform.GetChild(19).gameObject.SetActive(true);
        }
    }
    // ���ʹ�ð�ť
    public void ItemUse() {
        // ��ȡ�������Ʒ���±�
        int index = transform.GetSiblingIndex();
        // ���»�ɾ��������Ʒ����
        ItemDiscard();
        // ������Ʒ��Ѫ���ָ�ֵ�ͼ����Ȼָ�ֵ�ָ������򼢶���
        // ��ȡ��ұ���
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // ��ȡ������ϵĽű�
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").transform.gameObject.GetComponent<PlayerController>();
        print("�������ֵ:"+playerController.currentHealth);
        print("��Ҽ�����:" + playerController.currentHunger);
        // ��������������ʹ�õ���֮�������ֵ
        float useHealth = bagList.bag[index].health + playerController.currentHealth;
        // ��������������ʹ�õ���֮��ļ�����
        float useHunger = bagList.bag[index].hunger + playerController.currentHunger;

        // �жϵ�ǰ����ֵ�Ƿ�Ϊ�������ֵ
        if (playerController.currentHealth == playerController.maxHealth)
        {
            // ���ı�������ʾ����ֵ������ֱ���õ�ǰ����ֵ�����������ֵ
            playerController.currentHealth = playerController.maxHealth;
        }
        else {
            // �ж�ʹ�õ���֮�������ֵ�Ƿ�����������ֵ
            if (useHealth > playerController.maxHealth)
            {
                playerController.currentHealth = playerController.maxHealth;
            }
            else {
                // �����õ�ǰ����ֵ���ڼ�Ѫ���ֵ
                playerController.currentHealth = useHealth;
            }
        }

        // �жϵ�ǰ�������Ƿ�Ϊ��󼢶���
        if (playerController.currentHunger == playerController.maxHunger)
        {
            // ���ı�����ʾ��������������ֱ���õ�ǰ�����ȵ�����󼢶���
            playerController.currentHunger = playerController.maxHunger;
        }
        else {
            // �ж�ʹ�õ���֮��ļ������Ƿ������󼢶���
            if (useHunger > playerController.maxHunger)
            {
                playerController.currentHunger = playerController.maxHunger;
            }
            else {
                playerController.currentHunger = useHunger;
            }
        }
        print("------------------------------------------------------");
        print("�������ֵ-ʹ�õ��ߺ�:" + playerController.currentHealth);
        print("��Ҽ�����-ʹ�õ��ߺ�:" + playerController.currentHunger);
        print("------------------------------------------------------");

    }

    // �������
    public void ItemDiscard() {
        // ��ȡ�������Ʒ���±�
        int index = transform.GetSiblingIndex();
        // ��ȡ��ұ���
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // ��ȡ����ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // ��ȡ״̬ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // �жϵ�ǰ�������Ʒ�����Ƿ�Ϊ1
        if (bagList.bag[index].itemNumber == 1)
        {
            // �������Ϊ1��ݻ����壬���Ƴ���������
            // �ݻٱ���ui�ĵ������Ʒ
            Destroy(bagGrid.transform.GetChild(index).gameObject);
            // ����Ʒ�ӱ��������Ƴ�
            bagList.bag.Remove(bagList.bag[index]);
            // ��װ������Ʒ����ȡ��
            clickItemIndex = -1;
            // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
            addToBag.RefreshItem();
        }
        else {
            // ������Ʒ������������1����������ʾ
            bagList.bag[index].itemNumber -= 1;
            // Ϊ�˱ȶ��ı��ϵ������Ƿ�Ϊ��ʵ����������Ҫˢ���Խ��бȶ�
            addToBag.RefreshItem();
            if (bagList.bag[index].itemNumber % getItem.maxCumulative == 0)
            {
                // �жϵ�ǰ�����������ʾ�ı�Ϊ����ۼ�����,������Ҳ�Ϊ����ۼ���������ͬ���͵���Ʒ���дݻ�,����ֱ�Ӵݻٵ�ǰ�������Ʒ
                if (bagGrid.transform.GetChild(index).GetChild(0).GetComponent<Text>().text.Equals(getItem.maxCumulative.ToString()))
                {
                    // ���弯�Ͻ�����ͬ����Ʒ���±�
                    List<int> someItemIndexList = new List<int>();
                    for (int i = 0; i < bagList.bag.Count; i++)
                    {
                        if (bagList.bag[index] == bagList.bag[i])
                        {
                            someItemIndexList.Add(i);
                        }
                    }
                    // �������ҵ�ǰ�����Ʒ����ͬ��Ʒ
                    for (int i = 0; i < someItemIndexList.Count; i++)
                    {
                        if (!bagGrid.transform.GetChild(someItemIndexList[i]).GetChild(0).GetComponent<Text>().text.Equals(getItem.maxCumulative.ToString()))
                        {
                            // ����Ʒ�ӱ��������Ƴ�
                            bagList.bag.Remove(bagList.bag[someItemIndexList[i]]);
                            // �ݻٱ���ui�ĵ������Ʒ
                            Destroy(bagGrid.transform.GetChild(someItemIndexList[i]).gameObject);
                        }
                    }
                    clickItemIndex = -1;
                }
                else
                {
                    // ����Ʒ�ӱ��������Ƴ�
                    bagList.bag.Remove(bagList.bag[index]);
                    // �ݻٱ���ui�ĵ������Ʒ
                    Destroy(bagGrid.transform.GetChild(index).gameObject);
                    // ȡ��װ����Ʒ
                    clickItemIndex = -1;
                }
                // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
                addToBag.RefreshItem();
            }
            else {
                // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
                addToBag.RefreshItem();
            }
            
        }
    }

    // ������Ʒ
    public void ItemDiscardRedurce(bagCreations playerBag,itemsCreations item)
    {
        // ��ȡ����ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // ��ȡ����UI���������µ�����������
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // ��ȡ״̬ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // �жϵ�ǰ�������Ʒ�����Ƿ�Ϊ1
        if (item.itemNumber == 1)
        {
            // �������Ϊ1��ݻ����壬���Ƴ���������
            // ����Ʒ�ӱ��������Ƴ�
            playerBag.bag.Remove(item);
            // ��װ������Ʒ����ȡ��
            clickItemIndex = -1;
            // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
            addToBag.RefreshItem();
        }
        else
        {
            // ������Ʒ������������1����������ʾ
            item.itemNumber -= 1;
            // Ϊ�˱ȶ��ı��ϵ������Ƿ�Ϊ��ʵ����������Ҫˢ���Խ��бȶ�
            addToBag.RefreshItem();

        }
    }



    public void ItemDiscardTest(bagCreations playerBag,itemsCreations item)
    {
        if (item.itemNumber == 1)
        { 
            // ����Ʒ�ӱ��������Ƴ�
            playerBag.bag.Remove(item);
            // ��װ������Ʒ����ȡ��
            clickItemIndex = -1;
            // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
            addToBag.RefreshItem();
        }
        else
        {
            // ������Ʒ������������1����������ʾ
            item.itemNumber -= 1;
               // ���Ƴ��걳�����ݺ�ᵼ����ʾ�쳣����Ҫ��������ˢ����Ʒ�Ĵ���
           addToBag.RefreshItem();
        }
    }
}

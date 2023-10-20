using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����˵��� fileName :Ĭ���´�������Ʒ���ƣ� menuName �˵�����������
[CreateAssetMenu(fileName = "itemName",menuName = "bag/createNewItem")]
public class itemsCreations : ScriptableObject
{
    // ���ű�ר�����ڴ�����Ʒ���ݿ⣨��Ʒ��Ϣ��

    // ��Ʒ����
    public string itemName;
    // ��ƷͼƬ��Ϣ
    public Sprite itemImage;
    // ��Ʒ����
    public int itemNumber;
    // ��Ʒ��ֵ
    public int price;
    // ��Ʒ�ָ���Ѫ��ֵ
    public float health = 0;
    // ��Ʒ�ָ��ļ�����
    public float hunger = 0;
    // ��Ʒ����
    [TextArea]
    public string itemScription;
    // �ж��Ƿ���뵽ѭ���б�
    public bool isAddFishing;
    // �ɻ�ȡ�ľ���
    public int Exp;

    // �Ƿ����װ��
    public bool equip;
    // �Ƿ�����ۼ�
    public bool cumulative;
    // �Ƿ����ʹ��
    public bool use;
    // �øж�
    public int npcFavorNumber;

}

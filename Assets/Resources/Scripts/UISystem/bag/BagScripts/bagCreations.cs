using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� fileName: ���������ĳ�ʼ���� menuName �˵������������Ĺ���������
[CreateAssetMenu(fileName = "bagName" ,menuName ="bag/createNewBag")]
public class bagCreations : ScriptableObject
{
    // ��������\������ŵ�����ΪitemsCreations��������Ʒ����
    public List<itemsCreations> bag = new List<itemsCreations>();


}

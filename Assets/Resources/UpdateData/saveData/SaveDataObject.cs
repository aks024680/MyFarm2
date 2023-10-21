using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataObject : MonoBehaviour
{
}
// �ж��Ƿ���Ҫ���ش浵
public class NeedLoadData {
    // �Ƿ���Ҫ���ش浵
    public bool isNeedLoadData;
    // ��Ҫ���صĴ浵�ļ���Ϊ
    public string needLoadFileName;
    // �ж��Ƿ�ͨ��������ת����
    public bool isChangeScene;
    // ��תǰ�ĳ���Ϊʲô
    public string beforeChangeSceneName;
}
// ��Ҫ������������
public class DataForPlayer {
    // �����ٶ�
    public float speed;
    // �����ٶ�
    public float baseSpeed;
    // ��ǰ����ֵ
    public float currentHealth;
    // ��ǰ������
    public float currentHunger;
    // ����ֵ
    public float currentEndurance;
    // ���״̬ // 1����  2����  3����   4�Ҹ�  5����
    public int playerState;
    // ��ҵȼ�
    public int level;
    // ����ȼ�
    public int fishingLevel;
    // ��ֳ�ȼ�
    public int farmingLevel;
    // ����ȼ�
    public int fieldLevel;
    // �ڿ�ȼ�
    public int digLevel;
    //��ǰ���ﾭ��
    public int currentPlayerExp;
    // ��ǰ���㾭��
    public int currentFishExp;
    // ��ǰ��ֳ����
    public int currentFarmExp;
    // ��ǰ���ﾭ��
    public int currentFieldExp;
    // ��ǰ�ڿ���
    public int currentDigExp;
    // ��ǰ��������
    public string currentSceneName;
    // �������
    public float playerX;
    public float playerY;
    public float playerZ;
    // �浵����
    public string dataName;
    // �浵ʱ��
    public string dataSaveTime;

}
// ��Ҫ�����ʱ��ϵͳ����
public class DataForTimeSystem {
    // ����
    public int showMinute;
    // ����
    public int showSeconds;
    // �����л��ж�,1���죬2���죬3���죬4����
    public int seasonTime;
}
// ��Ҫ�����npc����
public class DataForNpc1 { }

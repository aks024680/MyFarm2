using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : MonoBehaviour
{
    // ��¼��Ҫ�������ݻ�ȡ�Ķ���
}
// ���帳ֵ������ԵĶ���(��ӿ����л�)

public class playerData
{
    // �����ٶ�
    public float speed;
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
    // ��תǰ�ĳ�������
    public string beforeSceneName;
}

// ���帳ֵʱ��ϵͳ������
[System.Serializable]
public class timeSystemData
{
    public int showMinute;
    // ����
    public int showSeconds;
    // �����л��ж�,1���죬2���죬3���죬4����
    public int seasonTime;
}
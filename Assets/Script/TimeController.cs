using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour
{
    public TMP_Text textTimeShow;
    public TMP_Text season;
    public TMP_Text day;
    //����8:00
    public int minute = 6;
    //����
    public int seconds = 0;
    //totalSecond 60��@�Ӵ`��
    private float totalSecond;
    //�u�` 1234 1�K�� 2�L�� 3��� 4�V��
    public int seasonType = 1;
    //���1-30
    public int dayTime = 30;

    // Start is called before the first frame update

     void Start()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        totalSecond += Time.fixedDeltaTime;
        //�P�_�ɶ��g�L�@��
        if (totalSecond >= 0.99f) {
            print(totalSecond);
            //�B��{�Ǥ]�ݭn�ɶ��A�ڭ̵L�k�ǽT���������A���F����A�ҥH�]�w0.99
            
            //��s�ɶ�
            UpdateTime();
            //�o�̬O��s�u�`�H�Τ��
            UpdateSeasonAndDay();
            totalSecond = 0;
        }
    }

    //��s�ɶ�
     void UpdateTime()
    {
        //23:59
        if (minute == 23) {
            //�Ҽ{����
        if (seconds == 59) { 
            //�����M�������O�s
            minute = 0;
                seconds = 0;
                //���s�奻��00:00
                textTimeShow.text = "00 : 00";
            }
            else
            {
                //�����ۼW
                seconds++;
                //�����O�@����٨���1,22,�@��ƻݭn�e����0
                //�Nint ��r�Ŧ�1/2/3 01 02 03

                if(seconds.ToString().Length ==1){
                //�����O23������0
                textTimeShow.text = "23 : 0"�@+ seconds.ToString();
                }
                else
                {
                    textTimeShow.text = "23 :" + seconds.ToString();
                }
            }
        }
        else
        {
            //�����b0~22�d��
            //�����O�_��59
          if(seconds == 59)
            {
                minute += 1;
                //second00
                seconds = 0;
                //�����O�_���@���
                string showMinute = minute.ToString();

                if(showMinute.Length == 1)
                {
                    showMinute = "0" + showMinute;
                }
                textTimeShow.text = showMinute+": 00";
            }
            else
            {
                seconds++;
                //�����M�������ݭn�i���ƪ��P�_
                string showMinute = minute.ToString();
                string showSecond = seconds.ToString();
                //�P�_�������
                if (showMinute.Length == 1)
                {
                    showMinute = "0" + showMinute;
                }
                //�P�_�������
                if (showSecond.Length == 1)
                {
                    showSecond = "0" + showSecond;
                }
                textTimeShow.text = showMinute + " : " + showSecond;
            }
        }
    }
    //��s�u�`�H�Τ��
    void UpdateSeasonAndDay()
    {
        //00:00
        if (minute ==0 && seconds == 0)
        {
            //�P�_�u�`,��e�ɨ�
            //�K��
            if(seasonType == 1) {
            //����O�_����30
            if(dayTime == 30)
                {
                    //����ܧ�1
                    dayTime = 0;
                    //�ܧ󬰮L��
                    seasonType = 2;
                    //�u�`�奻�ܰ�
                    season.text = "�L";
                   
                    season.color = new Color(129 / 255f, 169 / 255f, 169 / 255f, 255 / 255f);
                    //���
                    day.text = "1��";
                }
                else
                {
                    //dayTime�ۼW
                    dayTime+=1;
                    //��s������奻
                    day.text = dayTime.ToString() +"��";
                }
            }
            //�L��
            if (seasonType == 2)
            {
                //����O�_����30
                if (dayTime == 30)
                {
                    //����ܧ�1
                    dayTime = 0;
                    //�ܧ󬰬��
                    seasonType = 3;
                    //�u�`�奻�ܰ�
                    season.text = "��";
                    season.color = new Color(234 / 255f, 169 / 255f, 69 / 255f, 255 / 255f);
                    //���
                    day.text = "1��";
                }
                else
                {
                    //dayTime�ۼW
                    dayTime += 1;
                    //��s������奻
                    day.text = dayTime.ToString() + "��";
                }
            }
            //���
            if (seasonType == 3)
            {
                //����O�_����30
                if (dayTime == 30)
                {
                    //����ܧ�1
                    dayTime = 0;
                    //�ܧ󬰥V��
                    seasonType = 4;
                    //�u�`�奻�ܰ�
                    season.text = "�V";
                    season.color = new Color(200 / 255f, 9 / 255f, 149 / 255f, 199 / 255f);
                    //���
                    day.text = "1��";
                }
                else
                {
                    //dayTime�ۼW
                    dayTime += 1;
                    //��s������奻
                    day.text = dayTime.ToString() + "��";
                }
            }
            //�V��
            if (seasonType == 4)
            {
                //����O�_����30
                if (dayTime == 30)
                {
                    //����ܧ�1
                    dayTime = 0;
                    //�ܧ󬰬K��
                    seasonType = 1;
                    //�u�`�奻�ܰ�
                    season.text = "�K";
                    season.color = new Color(29 / 255f, 200 / 255f, 125 / 255f, 255 / 255f);
                    //���
                    day.text = "1��";
                }
                else
                {
                    //dayTime�ۼW
                    dayTime += 1;
                    //��s������奻
                    day.text = dayTime.ToString() + "��";
                }
            }
        }
    }
}

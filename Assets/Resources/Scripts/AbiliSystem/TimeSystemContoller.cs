using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystemContoller : MonoBehaviour
{
    // ʱ����ʾ // 24����Ϊһ��
    public Text TimeShow;
    // �����л�
    public Text season;
    // �����л�
    public Text holiday;
    // ����
    public Text weather;
    public Text day;
    // �ۼ�ʱ��
    public float totalSeconds = 0;
    // ����,��ʼ����ϷΪ08��00
    public int showMinute = 0;
    // ����
    public int showSeconds = 0;
    // �����л��ж�,1���죬2���죬3���죬4����
    public int seasonTime = 1;
    // ���ڣ�һ����30��һ����һ������
    public int dayTime = 30;
    // Start is called before the first frame update
    void Start()
    {
        TimeShow = gameObject.transform.GetChild(2).GetComponent<Text>();
        season = gameObject.transform.GetChild(0).GetComponent<Text>();
        holiday = gameObject.transform.GetChild(1).GetComponent<Text>();
        weather = gameObject.transform.GetChild(4).GetComponent<Text>();
        day = gameObject.transform.GetChild(3).GetComponent<Text>();
    }
    private void FixedUpdate()
    {
        // �ۼ�ʱ�䵽1�룬Ȼ����������Ϊ0��ÿ����ִ��һ�Σ�
        totalSeconds += Time.fixedDeltaTime;
        if (totalSeconds >= 0.98) {

            UpdateTime();
            UpdateDayAndSeason();
            totalSeconds = 0;
        }

    }

    private void Update()
    {
        UpdateHoliday();
    }

    // ����ʱ��
    public void UpdateTime() {
        // �жϷ����Ƿ�Ϊ23
        if (showMinute == 23)
        {
            // �ж������Ƿ�Ϊ59
            if (showSeconds == 59)
            {
                // ���÷��Ӻ�����Ϊ0
                showMinute = 0;
                showSeconds = 0;
                //������ʾʱ��Ϊ�賿
                TimeShow.text = "00:00";
            }
            else
            {
                // �����ۼ�
                showSeconds += 1;
                //�ж�����λ���Ƿ�Ϊ2��Ϊ1��ǰ�����
                if (showSeconds.ToString().Length == 1)
                {
                    string seconds = "0" + showSeconds.ToString();
                    TimeShow.text = "23:" + seconds;
                }
                else
                {
                    TimeShow.text = "23:" + showSeconds.ToString();
                }
            }
        }
        else {
            // print("����:"+showMinute+",����:"+showSeconds);
            // ��ʱ�䲻Ϊ23����ʱ
            //�ж������Ƿ�Ϊ59��
            if (showSeconds == 59)
            {
                // ��������Ϊ0��
                showSeconds = 0;
                // ����+1��
                showMinute += 1;
                // �жϷ����Ƿ�Ϊ2λ��1λ��0
                if (showMinute.ToString().Length == 1)
                {
                    // ��ʱ����ʾ�ڽ���
                    TimeShow.text = "0" + showMinute.ToString() + ":00";
                }
                else {
                    // ��ʱ����ʾ�ڽ���
                    TimeShow.text = showMinute.ToString() + ":00";
                }


            }
            else {
                // ������С��59
                // ����++
                showSeconds += 1;
                //�ж�����λ��

                // �������
                string minutes = showMinute.ToString();
                // ��������
                string seconds = showSeconds.ToString();

                // ����һλ��ʱ����
                if (minutes.Length == 1) {
                    minutes = "0" + showMinute.ToString();
                }
                // ����һλ��0
                if (seconds.Length == 1) {
                    seconds = "0" + showSeconds.ToString();
                }
                // ��ʱ����ʾ�ڽ���
                TimeShow.text = minutes + ":" + seconds;
            }
        }

    }

    // �������ںͼ���
    public void UpdateDayAndSeason() {
        // �ж��Ƿ�Ϊ00��00(������)���������ֻ�е��ڶ����ʱ��Ż�ִ��
        if (showMinute == 0 && showSeconds == 0) {
            // �ж��Ƿ��Ǵ���
            if (seasonTime == 1) {
                // �ж������Ƿ���30
                if (dayTime == 30)
                {
                    // �������ڣ�Ϊ�˵�����һ�����ڴ���׼��
                    dayTime = 0;
                    // ��������Ϊ����
                    seasonTime = 2;
                    // ���¼�����ʾ������
                    ColorUtility.TryParseHtmlString("#EE3838", out Color nowColor);
                    season.text = "��";
                    season.color = nowColor;
                    // �������ڵ����棬
                    day.text = "1��";
                }
                else {
                    // ����++
                    dayTime += 1;
                    // �������ڵ�����
                    day.text = dayTime.ToString() + "��";
                }
            }

            // �ж��Ƿ�������
            if (seasonTime == 2)
            {
                // �ж������Ƿ���30
                if (dayTime == 30)
                {
                    // ��������
                    dayTime = 0;
                    // ��������Ϊ����
                    seasonTime = 3;
                    // ���¼�����ʾ������
                    season.text = "��";
                    ColorUtility.TryParseHtmlString("#CD0000", out Color nowColor);
                    season.color = nowColor;
                    // �������ڵ�����
                    day.text = "1��";
                }
                else
                {
                    // ����++
                    dayTime += 1;
                    // �������ڵ�����
                    day.text = dayTime.ToString() + "��";
                }
            }

            // �ж��Ƿ�������
            if (seasonTime == 3)
            {
                // �ж������Ƿ���30
                if (dayTime == 30)
                {
                    // ��������
                    dayTime = 0;
                    // ��������Ϊ����
                    seasonTime = 4;
                    // ���¼�����ʾ������
                    season.text = "��";
                    ColorUtility.TryParseHtmlString("#FFFFFF", out Color nowColor);
                    season.color = nowColor;
                    // �������ڵ�����
                    day.text = "1��";
                }
                else
                {
                    // ����++
                    dayTime += 1;
                    // �������ڵ�����
                    day.text = dayTime.ToString() + "��";
                }
            }

            // �ж��Ƿ��Ƕ���
            if (seasonTime == 4)
            {
                // �ж������Ƿ���30
                if (dayTime == 30)
                {
                    // ��������
                    dayTime = 0;
                    // ��������Ϊ����
                    seasonTime = 1;
                    // ���¼�����ʾ������
                    season.text = "��";
                    ColorUtility.TryParseHtmlString("#5CACEE", out Color nowColor);
                    season.color = nowColor;
                    // �������ڵ�����
                    day.text = "1��";
                }
                else
                {
                    // ����++
                    dayTime += 1;
                    // �������ڵ�����
                    day.text = dayTime.ToString() + "��";
                }
            }
        }
        // ��ʱ���¼���
        if (seasonTime == 1)
        {
            season.text = "��";
            ColorUtility.TryParseHtmlString("#5CACEE", out Color nowColor);
            season.color = nowColor;
        }
        if (seasonTime == 2)
        {
            season.text = "��";
            ColorUtility.TryParseHtmlString("#EE3838", out Color nowColor);
            season.color = nowColor;
        }
        if (seasonTime == 3)
        {
            season.text = "��";
            ColorUtility.TryParseHtmlString("#CD0000", out Color nowColor);
            season.color = nowColor;
        }
        if (seasonTime == 4)
        {
            season.text = "��";
            ColorUtility.TryParseHtmlString("#FFFFFF", out Color nowColor);
            season.color = nowColor;
        }
    }

    // ���½���
    void UpdateHoliday() {
        // ��
        if (seasonTime == 1) {
            if (dayTime == 1)
            {
                holiday.text = "����";
            }
            else if (dayTime == 10)
            {
                holiday.text = "����";
            }
            else {
                holiday.text = "";
            }
        }


    }

}

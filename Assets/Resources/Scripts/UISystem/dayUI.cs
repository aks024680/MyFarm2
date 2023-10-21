using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dayUI : MonoBehaviour
{
    // Start is called before the first frame update
    // ��ȡ��������
    private Transform daySystem;
    // ��ȡʱ������ű�
    private TimeSystemContoller timeSystemContoller;


    /// <summary>
    /// ԭʼͼƬ��ɫ  (253,224,168)
    /// ԭʼ�ı���ɫ  (0,96,42)
    /// 
    /// �޸ĺ�ͼƬ��ɫ (0,255,255)
    /// �޸ĺ��ı���ɫ (255,0,0)
    /// 
    ///     ColorUtility.TryParseHtmlString("#5CACEE", out Color nowColor);
    ///     season.color = nowColor;
    /// </summary>
    void Start()
    {
        // ��ֵ�����
        daySystem = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDayUI();
        
    }


    // ��ʼ������UI��ʾ
    void UpdateDayUI() {
        // ��ȡʱ��ϵͳ����
        GameObject time = GameObject.FindGameObjectWithTag("uiSystem");
        // ��ȡʱ��ϵͳ�Ľű�
        timeSystemContoller = time.transform.GetChild(0).GetComponent<TimeSystemContoller>();
        // �����ı���ʾ�����ý���
        // ��
        if (timeSystemContoller.seasonTime == 1) {
            // ��ʼ��
            for (int i=1;i<=30;i++) {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1��\n����";
                }
                else if (i == 10)
                {
                    daySystem.Find("10").GetChild(0).GetComponent<Text>().text = "10��\n����";
                }
                else {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "��";
                }
            }

            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // ��ǰ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // �ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // �ָ�ԭ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // �ָ��ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }
        // ��
        if (timeSystemContoller.seasonTime == 2)
        {
            for (int i = 1; i <= 30; i++)
            {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1��\n����";
                }
                else if (i == 18)
                {
                    daySystem.Find("18").GetChild(0).GetComponent<Text>().text = "10��\n�����";
                }
                else
                {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "��";
                }
            }
            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // ��ǰ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // �ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // �ָ�ԭ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // �ָ��ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }
        // ��
        if (timeSystemContoller.seasonTime == 3)
        {
            for (int i = 1; i <= 30; i++)
            {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1��\n����";
                }
                else if (i == 15)
                {
                    daySystem.Find("15").GetChild(0).GetComponent<Text>().text = "10��\n���ս�";
                }
                else
                {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "��";
                }
            }
            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // ��ǰ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // �ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // �ָ�ԭ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // �ָ��ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }
        // ��
        if (timeSystemContoller.seasonTime == 4)
        {
            for (int i = 1; i <= 30; i++)
            {
                if (i == 1)
                {
                    daySystem.Find("1").GetChild(0).GetComponent<Text>().text = "1��\n����";
                }
                else if (i == 22)
                {
                    daySystem.Find("22").GetChild(0).GetComponent<Text>().text = "10��\n�ж���";
                }
                else
                {
                    daySystem.Find(i.ToString()).GetChild(0).GetComponent<Text>().text = i.ToString() + "��";
                }
            }
            foreach (Transform child in daySystem)
            {
                if (timeSystemContoller.dayTime.ToString().Equals(child.name))
                {
                    // ��ǰ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(0, 255, 255, 255);
                    // �ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(255, 0, 0, 255);
                }
                else
                {
                    // �ָ�ԭ����ͼƬ��ɫ
                    child.GetComponent<Image>().color = new Color(253, 224, 168, 255);
                    // �ָ��ı���ɫ
                    child.GetChild(0).GetComponent<Text>().color = new Color(0, 96, 42, 255);
                }
            }

        }




    }

}

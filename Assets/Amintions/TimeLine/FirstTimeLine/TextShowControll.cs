using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShowControll : MonoBehaviour
{
    // ����೤ʱ�����ı�
    public float textChangeTime = 0;
    // ����
    // �����һ�ξ�����ı�
    public string[] text1 =new string[] {"���㵽��","�ǵû�����������", "......","��ǰ���߰�", "" };
    // ����ڶ��ξ�����ı�
    public string[] text2 =new string[] {"�����·��","����","��������֪��·��","" };
    // �ı��任�±�
    public int index = -1;
    // ������վ��鲥�ŵ�ʱ��
    public float tatolPlayTime = 0;
    //�ж��Ƿ񲥷����
    public bool isPlay = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay == false) {
            textChangeTime += Time.deltaTime;
            tatolPlayTime += Time.deltaTime;
            if (textChangeTime >= 1.4)
            {
                // ��һ�ξ����ڵ��þ�����ı�
                if (tatolPlayTime <= 10)
                {
                    changeText1();

                }
                else if (tatolPlayTime >= 22.5)
                {
                    changeText2();

                }
                textChangeTime = 0;
            }
        }

    }

    // �ı��任
    public void changeText1() {
        // ��ȡ�ı���
        GameObject dialog = GetComponent<Transform>().GetChild(0).gameObject;

        
        // ���±�Ϊ-1��ʱ��
        if (index == -1)
        {
            dialog.SetActive(true);
        }
        else if (index == 5) {
            // �����йر�
            //GetComponent<Transform>().gameObject.GetComponent<TextShowControll>().enabled = false;
            //GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false) ;
            print("index"+dialog.name);
            dialog.SetActive(false);
            index = -2;
        } else {
            print("�±�:" + index);
            dialog.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = text1[index];
        }
        index++;
    }

    public void changeText2() {
        // ��ȡ�ı���
        GameObject dialog = GetComponent<Transform>().GetChild(0).gameObject;
        print("index,�ڶ���" + index);
        // ���±�Ϊ-1��ʱ��
        if (index == -1)
        {

            dialog.SetActive(true);
        }
        else if (index == 4)
        {
            // �����йر�
            GetComponent<Transform>().gameObject.GetComponent<TextShowControll>().enabled = false;
            GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false);
            index = -1;
            isPlay = true;
        }
        else
        {
            dialog.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = text2[index];
        }
        index++;
    }

}

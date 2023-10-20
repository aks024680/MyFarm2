using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour
{
    // ��
    public GameObject spring;
    // ��
    public GameObject summer;
    // ��
    public GameObject fall;
    // ��
    public GameObject winter;

    public GameObject timeSystem;

    // Update is called once per frame
    void Update()
    {
         ChangeSeasonMap();
    }

    // ���ݼ��ڱ����ͼ
    void ChangeSeasonMap() {
        // ��ȡʱ�����
        TimeSystemContoller timeSystemContoller = timeSystem.GetComponent<TimeSystemContoller>();
        // ����
        if (timeSystemContoller.seasonTime == 1) {
            spring.SetActive(true);
            summer.SetActive(false);
            fall.SetActive(false);
            winter.SetActive(false);

        }
        // �ļ�
        if (timeSystemContoller.seasonTime == 2) {
            spring.SetActive(false);
            summer.SetActive(true);
            fall.SetActive(false);
            winter.SetActive(false);
        }
        // �＾
        if (timeSystemContoller.seasonTime == 3)
        {
            spring.SetActive(false);
            summer.SetActive(false);
            fall.SetActive(true);
            winter.SetActive(false);
        }
        // ����
        if (timeSystemContoller.seasonTime == 4)
        {
            spring.SetActive(false);
            summer.SetActive(false);
            fall.SetActive(false);
            winter.SetActive(true);
        }
    }

}

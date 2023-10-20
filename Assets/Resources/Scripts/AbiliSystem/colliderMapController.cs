using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class colliderMapController : MonoBehaviour
{
    // ʱ��ϵͳ
    public GameObject timeSys;
    // ��ȡcinemachine��confiner
    private CinemachineConfiner confiner;

    // ��ȡ���غ�����
    private GameObject isSpring;
    private GameObject isSummer;
    private GameObject isFall;
    private GameObject isWinter;
    // Update is called once per frame

    private void Start()
    {
        // ��ֵʱ��ϵͳ
        timeSys = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
    }
    void Update()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "fishingMap") {
            SeasonMapChange();
        }

    }

    // �ļ���ͼ�л���ֻ���ڵ������ʱ��
    public void SeasonMapChange() {
        TimeSystemContoller timeSystemContoller = timeSys.GetComponent<TimeSystemContoller>();
        // ��ǩȥ��ͼ
        isSpring = GameObject.FindGameObjectWithTag("springMap");
        isSummer = GameObject.FindGameObjectWithTag("summerMap");
        isFall = GameObject.FindGameObjectWithTag("fallMap");
        isWinter = GameObject.FindGameObjectWithTag("winterMap");
        // �жϼ���
        print("����:"+ timeSystemContoller.seasonTime);
        print("��:"+isSpring.name);
        // ����
        if (timeSystemContoller.seasonTime == 1)
        {
            if (!confiner.m_BoundingShape2D) {
                if (isSpring)
                {
                    print(isSpring.transform.GetChild(5).GetComponent<PolygonCollider2D>());
                    confiner.m_BoundingShape2D = isSpring.transform.GetChild(5).GetComponent<PolygonCollider2D>();
                }
            }

        }
        // �ļ�
        if (timeSystemContoller.seasonTime == 2)
        {
            if (isSummer)
            {
                confiner.m_BoundingShape2D = isSummer.GetComponent<PolygonCollider2D>();
            }
        }
        // �＾
        if (timeSystemContoller.seasonTime == 3)
        {
            if (isFall)
            {
                confiner.m_BoundingShape2D = isFall.GetComponent<PolygonCollider2D>();
            }
        }
        // ����
        // �＾
        if (timeSystemContoller.seasonTime == 4)
        {
            if (isWinter)
            {
                confiner.m_BoundingShape2D = isWinter.GetComponent<PolygonCollider2D>();
            }
        }
    }
}

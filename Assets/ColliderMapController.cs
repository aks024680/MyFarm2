using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ColliderMapController : MonoBehaviour
{
    //�ɶ��t��
    public GameObject timeSys;
    //���cinemachain��confiner
    private CinemachineConfiner confiner;

    //����[�J�Z���ե�
    private GameObject isSpring;
    private GameObject isSummer;
    private GameObject isFall;
    private GameObject isWinter;
    // Start is called before the first frame update
    void Start()
    {
        confiner = GetComponent<CinemachineConfiner>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeController timeSystemController = timeSys.GetComponent<TimeController>();
        //���ҥh�a��
        isSpring = GameObject.FindGameObjectWithTag("springMap");
        isSummer = GameObject.FindGameObjectWithTag("summerMap");
        isFall = GameObject.FindGameObjectWithTag("fallMap");
        isWinter = GameObject.FindGameObjectWithTag("winterMap");

        //�P�_�u�`
        //�K�u
        if (timeSystemController.seasonType == 1)
        {
            if (isSpring)
            {
                confiner.m_BoundingShape2D = isSpring.GetComponent<PolygonCollider2D>();
            }
        }
        if (timeSystemController.seasonType == 2)
        {
            if (isSummer)
            {
                confiner.m_BoundingShape2D = isSummer.GetComponent<PolygonCollider2D>();
            }
        }
        if (timeSystemController.seasonType == 3)
        {
            if (isFall)
            {
                confiner.m_BoundingShape2D = isFall.GetComponent<PolygonCollider2D>();
            }
        }
        if (timeSystemController.seasonType == 4)
        {
            if (isWinter)
            {
                confiner.m_BoundingShape2D = isWinter.GetComponent<PolygonCollider2D>();
            }
        }
    }
}
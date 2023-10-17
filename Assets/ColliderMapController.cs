using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ColliderMapController : MonoBehaviour
{
    //時間系統
    public GameObject timeSys;
    //獲取cinemachain的confiner
    private CinemachineConfiner confiner;

    //獲取加仔后的組件
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
        //標籤去地圖
        isSpring = GameObject.FindGameObjectWithTag("springMap");
        isSummer = GameObject.FindGameObjectWithTag("summerMap");
        isFall = GameObject.FindGameObjectWithTag("fallMap");
        isWinter = GameObject.FindGameObjectWithTag("winterMap");

        //判斷季節
        //春季
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
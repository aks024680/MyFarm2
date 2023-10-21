using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class colliderMapController : MonoBehaviour
{
    // 时间系统
    public GameObject timeSys;
    // 获取cinemachine得confiner
    private CinemachineConfiner confiner;

    // 获取加载后的组件
    private GameObject isSpring;
    private GameObject isSummer;
    private GameObject isFall;
    private GameObject isWinter;
    // Update is called once per frame

    private void Start()
    {
        // 赋值时间系统
        timeSys = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
    }
    void Update()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "fishingMap") {
            SeasonMapChange();
        }

    }

    // 四季地图切换（只有在钓鱼湖的时候）
    public void SeasonMapChange() {
        TimeSystemContoller timeSystemContoller = timeSys.GetComponent<TimeSystemContoller>();
        // 标签去地图
        isSpring = GameObject.FindGameObjectWithTag("springMap");
        isSummer = GameObject.FindGameObjectWithTag("summerMap");
        isFall = GameObject.FindGameObjectWithTag("fallMap");
        isWinter = GameObject.FindGameObjectWithTag("winterMap");
        // 判断季节
        print("季节:"+ timeSystemContoller.seasonTime);
        print("春:"+isSpring.name);
        // 春季
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
        // 夏季
        if (timeSystemContoller.seasonTime == 2)
        {
            if (isSummer)
            {
                confiner.m_BoundingShape2D = isSummer.GetComponent<PolygonCollider2D>();
            }
        }
        // 秋季
        if (timeSystemContoller.seasonTime == 3)
        {
            if (isFall)
            {
                confiner.m_BoundingShape2D = isFall.GetComponent<PolygonCollider2D>();
            }
        }
        // 冬季
        // 秋季
        if (timeSystemContoller.seasonTime == 4)
        {
            if (isWinter)
            {
                confiner.m_BoundingShape2D = isWinter.GetComponent<PolygonCollider2D>();
            }
        }
    }
}

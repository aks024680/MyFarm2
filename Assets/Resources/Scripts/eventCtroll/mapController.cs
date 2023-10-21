using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour
{
    // 春
    public GameObject spring;
    // 夏
    public GameObject summer;
    // 秋
    public GameObject fall;
    // 冬
    public GameObject winter;

    public GameObject timeSystem;

    // Update is called once per frame
    void Update()
    {
         ChangeSeasonMap();
    }

    // 根据季节变更地图
    void ChangeSeasonMap() {
        // 获取时间组件
        TimeSystemContoller timeSystemContoller = timeSystem.GetComponent<TimeSystemContoller>();
        // 春季
        if (timeSystemContoller.seasonTime == 1) {
            spring.SetActive(true);
            summer.SetActive(false);
            fall.SetActive(false);
            winter.SetActive(false);

        }
        // 夏季
        if (timeSystemContoller.seasonTime == 2) {
            spring.SetActive(false);
            summer.SetActive(true);
            fall.SetActive(false);
            winter.SetActive(false);
        }
        // 秋季
        if (timeSystemContoller.seasonTime == 3)
        {
            spring.SetActive(false);
            summer.SetActive(false);
            fall.SetActive(true);
            winter.SetActive(false);
        }
        // 冬季
        if (timeSystemContoller.seasonTime == 4)
        {
            spring.SetActive(false);
            summer.SetActive(false);
            fall.SetActive(false);
            winter.SetActive(true);
        }
    }

}

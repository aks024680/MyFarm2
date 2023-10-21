using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapControllByPrefabs : MonoBehaviour
{
    // 春季地图预制体
    public GameObject springMap;
    // 夏季地图预制体
    public GameObject summerMap;
    // 秋季
    public GameObject fallMap;
    // 冬季
    public GameObject winterMap;
    // 获取时间组件
    public GameObject timeSystem;

    

    // 
    private GameObject isSpring;
    //
    private GameObject isSummer;
    //
    private GameObject isFall;
    // 
    private GameObject isWinter;

    private void Start()
    {

        

    }
    // Update is called once per frame
    void Update()
    {
        ChangeMap();
    }

    // 根据不同的季节切换地图
    void ChangeMap() {
        timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        TimeSystemContoller timeSystemContoller = timeSystem.GetComponent<TimeSystemContoller>();
        // 获取到预制体，然后才能够使用代码去加载预制体到场景
        springMap = Resources.Load<GameObject>("Prefabs/map/spring");
        summerMap = Resources.Load<GameObject>("Prefabs/map/summer");
        fallMap = Resources.Load<GameObject>("Prefabs/map/fall");
        winterMap = Resources.Load<GameObject>("Prefabs/map/winter");

        // 获取场景有没有加载预制体到场景里
        isSpring = GameObject.FindGameObjectWithTag("springMap");

        isSummer = GameObject.FindGameObjectWithTag("summerMap");

        isFall = GameObject.FindGameObjectWithTag("fallMap");

        isWinter = GameObject.FindGameObjectWithTag("winterMap");
        // 春季
        if (timeSystemContoller.seasonTime == 1) {
            
            // 销毁冬季地图
            if (isWinter) {
                Destroy(isWinter);
            }
            // 判断说预制体有没有加载到地图
            if (!isSpring) {
                // 如果不存在
                Instantiate(springMap);
            }
        }
        // 夏季
        if (timeSystemContoller.seasonTime == 2) {
            // 夏季销毁的是春季的地图
            if (isSpring) {
                Destroy(isSpring);
            }
            if (!isSummer)
            {
                Instantiate(summerMap);
            }

        }

        // 秋季
        if (timeSystemContoller.seasonTime == 3)
        {
            //销毁夏季的地图
            if (isSummer)
            {
                Destroy(isSummer);
            }
            // 创建秋季地图
            if (!isFall)
            {
                Instantiate(fallMap);
            }
        }

        // 冬季
        if (timeSystemContoller.seasonTime == 4)
        {
            //销毁秋季的地图
            if (isFall)
            {
                Destroy(isFall);
            }
            // 创建冬季地图
            if (!isWinter)
            {
                Instantiate(winterMap);
            }
        }
    }
}

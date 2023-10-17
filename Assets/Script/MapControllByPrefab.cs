using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllByPrefab : MonoBehaviour
{
    //春季預制圖
    public GameObject springMap;
    //夏季預置圖
    public GameObject summerMap;
    //秋季預置圖
    public GameObject fallMap;
    //冬季預置圖
    public GameObject winterMap;
    //獲取時間系統組件，季節變換和時間輪轉的C#腳本
    public GameObject timeSystem;
    // Start is called before the first frame update
    private GameObject isSpring;
    private GameObject isSummer;
    private GameObject isFall;
    private GameObject isWinter;
    // Update is called once per frame
    void Update()
    {
        ChangeMap();
    }
    //根據不同的季節更換地圖
    void ChangeMap() { 
    TimeController timeSystemControll = timeSystem.GetComponent<TimeController>();

        //獲取到預置體 然後才能使用代碼取加載預制體到場景
        springMap = Resources.Load<GameObject>("Prefabs/Map/springMap");
        summerMap = Resources.Load<GameObject>("Prefabs/Map/summerMap");
        fallMap = Resources.Load<GameObject>("Prefabs/Map/fallMap");
        winterMap = Resources.Load<GameObject>("Prefabs/Map/winterMap");
        //獲取場景有沒有加載預制體到場景里
        isSpring = GameObject.FindGameObjectWithTag("springMap");
        isSummer = GameObject.FindGameObjectWithTag("summerMap");
        isFall = GameObject.FindGameObjectWithTag("fallMap");
        isWinter = GameObject.FindGameObjectWithTag("winterMap");

        //春季
        if (timeSystemControll.seasonType == 1) {
            //銷毀冬季地圖
            if (isWinter)
            {
                Destroy(isWinter);
            }
            //判斷說預置體有沒有加載到地圖
            //創建春季地圖
            if (!isSpring)
            {
                //如果不存在
                Instantiate(springMap);
            }
        }
        //夏季
        if (timeSystemControll.seasonType == 2)
        {
            //判斷說預置體有沒有加載到地圖
            //夏季銷毀的是春季的地圖
            
            if (isSpring) {
            Destroy(isSpring);
            }
            //創建夏季地圖
            if (!isSummer)
            {
                Instantiate(summerMap);
            }
        }
        //秋季
        if (timeSystemControll.seasonType == 3)
        {
            //判斷說預置體有沒有加載到地圖
            //秋季銷毀的是夏季地圖
            
            if (isSummer)
            {
                Destroy(isSummer);
            }
            // 創建秋季地圖
            if (!isFall)
            {
                Instantiate(fallMap);
            }
        }
        //冬季
        if (timeSystemControll.seasonType == 4)
        {
            //判斷說預置體有沒有加載到地圖
            //冬季銷毀的是秋季地圖
            if (isFall)
            {
                Destroy(isFall);
            }
            // 創建冬季地圖
            if (!isWinter)
            {
                Instantiate(winterMap);
            }
        }

    }
}

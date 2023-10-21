using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapControllByPrefabs : MonoBehaviour
{
    // ������ͼԤ����
    public GameObject springMap;
    // �ļ���ͼԤ����
    public GameObject summerMap;
    // �＾
    public GameObject fallMap;
    // ����
    public GameObject winterMap;
    // ��ȡʱ�����
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

    // ���ݲ�ͬ�ļ����л���ͼ
    void ChangeMap() {
        timeSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).gameObject;
        TimeSystemContoller timeSystemContoller = timeSystem.GetComponent<TimeSystemContoller>();
        // ��ȡ��Ԥ���壬Ȼ����ܹ�ʹ�ô���ȥ����Ԥ���嵽����
        springMap = Resources.Load<GameObject>("Prefabs/map/spring");
        summerMap = Resources.Load<GameObject>("Prefabs/map/summer");
        fallMap = Resources.Load<GameObject>("Prefabs/map/fall");
        winterMap = Resources.Load<GameObject>("Prefabs/map/winter");

        // ��ȡ������û�м���Ԥ���嵽������
        isSpring = GameObject.FindGameObjectWithTag("springMap");

        isSummer = GameObject.FindGameObjectWithTag("summerMap");

        isFall = GameObject.FindGameObjectWithTag("fallMap");

        isWinter = GameObject.FindGameObjectWithTag("winterMap");
        // ����
        if (timeSystemContoller.seasonTime == 1) {
            
            // ���ٶ�����ͼ
            if (isWinter) {
                Destroy(isWinter);
            }
            // �ж�˵Ԥ������û�м��ص���ͼ
            if (!isSpring) {
                // ���������
                Instantiate(springMap);
            }
        }
        // �ļ�
        if (timeSystemContoller.seasonTime == 2) {
            // �ļ����ٵ��Ǵ����ĵ�ͼ
            if (isSpring) {
                Destroy(isSpring);
            }
            if (!isSummer)
            {
                Instantiate(summerMap);
            }

        }

        // �＾
        if (timeSystemContoller.seasonTime == 3)
        {
            //�����ļ��ĵ�ͼ
            if (isSummer)
            {
                Destroy(isSummer);
            }
            // �����＾��ͼ
            if (!isFall)
            {
                Instantiate(fallMap);
            }
        }

        // ����
        if (timeSystemContoller.seasonTime == 4)
        {
            //�����＾�ĵ�ͼ
            if (isFall)
            {
                Destroy(isFall);
            }
            // ����������ͼ
            if (!isWinter)
            {
                Instantiate(winterMap);
            }
        }
    }
}

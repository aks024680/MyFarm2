using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllByPrefab : MonoBehaviour
{
    //�K�u�w���
    public GameObject springMap;
    //�L�u�w�m��
    public GameObject summerMap;
    //��u�w�m��
    public GameObject fallMap;
    //�V�u�w�m��
    public GameObject winterMap;
    //����ɶ��t�βե�A�u�`�ܴ��M�ɶ����઺C#�}��
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
    //�ھڤ��P���u�`�󴫦a��
    void ChangeMap() { 
    TimeController timeSystemControll = timeSystem.GetComponent<TimeController>();

        //�����w�m�� �M��~��ϥΥN�X���[���w��������
        springMap = Resources.Load<GameObject>("Prefabs/Map/springMap");
        summerMap = Resources.Load<GameObject>("Prefabs/Map/summerMap");
        fallMap = Resources.Load<GameObject>("Prefabs/Map/fallMap");
        winterMap = Resources.Load<GameObject>("Prefabs/Map/winterMap");
        //����������S���[���w����������
        isSpring = GameObject.FindGameObjectWithTag("springMap");
        isSummer = GameObject.FindGameObjectWithTag("summerMap");
        isFall = GameObject.FindGameObjectWithTag("fallMap");
        isWinter = GameObject.FindGameObjectWithTag("winterMap");

        //�K�u
        if (timeSystemControll.seasonType == 1) {
            //�P���V�u�a��
            if (isWinter)
            {
                Destroy(isWinter);
            }
            //�P�_���w�m�馳�S���[����a��
            //�ЫجK�u�a��
            if (!isSpring)
            {
                //�p�G���s�b
                Instantiate(springMap);
            }
        }
        //�L�u
        if (timeSystemControll.seasonType == 2)
        {
            //�P�_���w�m�馳�S���[����a��
            //�L�u�P�����O�K�u���a��
            
            if (isSpring) {
            Destroy(isSpring);
            }
            //�ЫخL�u�a��
            if (!isSummer)
            {
                Instantiate(summerMap);
            }
        }
        //��u
        if (timeSystemControll.seasonType == 3)
        {
            //�P�_���w�m�馳�S���[����a��
            //��u�P�����O�L�u�a��
            
            if (isSummer)
            {
                Destroy(isSummer);
            }
            // �Ыج�u�a��
            if (!isFall)
            {
                Instantiate(fallMap);
            }
        }
        //�V�u
        if (timeSystemControll.seasonType == 4)
        {
            //�P�_���w�m�馳�S���[����a��
            //�V�u�P�����O��u�a��
            if (isFall)
            {
                Destroy(isFall);
            }
            // �ЫإV�u�a��
            if (!isWinter)
            {
                Instantiate(winterMap);
            }
        }

    }
}

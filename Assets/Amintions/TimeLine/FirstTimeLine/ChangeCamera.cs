using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCamera : MonoBehaviour
{
    // ������������˶���ͷ��ʱ��
    private float total = 0;
    void Start()
    {
        ChangeStatus();
    }

    // Update is called once per frame
    void Update()
    {
        total += Time.deltaTime;
        if (total >= 10) {
            changeAllStausBack();
        }
    }

    // ���ƽ�ɫ�����ͱ��״̬
    public void ChangeStatus() {
        // ���״̬
        // ��ȡcineMachine�����
        GameObject cm = GameObject.FindGameObjectWithTag("cineMachine").transform.gameObject;
        // ��ȡ�˶��Ŀ�����
        Transform changeCameraObject = GetComponent<Transform>();
        print(changeCameraObject.name);
        print(cm.name);
        cm.GetComponent<CinemachineVirtualCamera>().Follow = changeCameraObject;

        // ��ȡ���
        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        player.GetComponent<PlayerController>().enabled = false;

    }

    // �л���ͷ��������
    public void changeAllStausBack() {
        // ��ȡcineMachine�����
        GameObject cm = GameObject.FindGameObjectWithTag("cineMachine").transform.gameObject;
        // ��ȡ���
        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        // �л���ͷ��������Ϊ����
        cm.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        if (total >= 41) {
            player.GetComponent<PlayerController>().enabled = true;
            // ��ȡfishingMap�����������ֽ��в���
            GameObject mainAudio = GameObject.Find("audioPlay").transform.gameObject;
            mainAudio.transform.GetChild(0).gameObject.SetActive(true);
        }


    }
}

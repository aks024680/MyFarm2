using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHelpControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameHelpPanelControll();
    }
    // ������Ϸ������ʾ������
    public void gameHelpPanelControll() {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf==true) {
            // �ر���Ϸ����
            gameObject.SetActive(false);
            // ��ϵͳui
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

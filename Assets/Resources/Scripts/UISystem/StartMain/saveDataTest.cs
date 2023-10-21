using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataTest : MonoBehaviour
{
   /// <summary>
   /// �浵
   /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CloseSaveDataUI();
    }

    // 
    public void CloseSaveDataUI() {
        // ESC
        if (Input.GetKey(KeyCode.Escape)) {
            // ���˵�
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
            // �浵����
            GameObject saveDataUI = GameObject.FindGameObjectWithTag("SaveDataUI").transform.gameObject;
            //
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);
            //
            saveDataUI.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // �浵1
    public void Data1() {
        print("�浵1");
    }
    // �浵2
    public void Data2()
    {
        print("�浵2");
    }
    // �浵3
    public void Data3()
    {
        print("�浵3");
    }
}

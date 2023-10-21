using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    // 获取ui系统
    private Transform uISystem;
    // 定义面板值
    private int Panelvalue = 0;
    void Start()
    {
        uISystem = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        showUI();
    }

    void showUI() {
        // 左切换
        if (Input.GetKeyDown(KeyCode.Q)) {
            switch (Panelvalue) {
                case 0:
                    uISystem.GetChild(0).gameObject.SetActive(false);
                    uISystem.GetChild(5).gameObject.SetActive(true);
                    Panelvalue = 5;
                    break;
                case 1:
                    uISystem.GetChild(1).gameObject.SetActive(false);
                    uISystem.GetChild(0).gameObject.SetActive(true);
                    Panelvalue = 0;
                    break;
                case 2:
                    uISystem.GetChild(2).gameObject.SetActive(false);
                    uISystem.GetChild(1).gameObject.SetActive(true);
                    Panelvalue = 1;
                    break;
                case 3:
                    uISystem.GetChild(3).gameObject.SetActive(false);
                    uISystem.GetChild(2).gameObject.SetActive(true);
                    Panelvalue = 2;
                    break;
                case 4:
                    uISystem.GetChild(4).gameObject.SetActive(false);
                    uISystem.GetChild(3).gameObject.SetActive(true);
                    Panelvalue = 3;
                    break;
                case 5:
                    uISystem.GetChild(5).gameObject.SetActive(false);
                    uISystem.GetChild(4).gameObject.SetActive(true);
                    Panelvalue = 4;
                    break;
            }
        }
        // 右切换
        if (Input.GetKeyDown(KeyCode.R)) {
            switch (Panelvalue)
            {
                case 0:
                    uISystem.GetChild(0).gameObject.SetActive(false);
                    uISystem.GetChild(1).gameObject.SetActive(true);
                    Panelvalue = 1;
                    break;
                case 1:
                    uISystem.GetChild(1).gameObject.SetActive(false);
                    uISystem.GetChild(2).gameObject.SetActive(true);
                    Panelvalue = 2;
                    break;
                case 2:
                    uISystem.GetChild(2).gameObject.SetActive(false);
                    uISystem.GetChild(3).gameObject.SetActive(true);
                    Panelvalue = 3;
                    break;
                case 3:
                    uISystem.GetChild(3).gameObject.SetActive(false);
                    uISystem.GetChild(4).gameObject.SetActive(true);
                    Panelvalue = 4;
                    break;
                case 4:
                    uISystem.GetChild(4).gameObject.SetActive(false);
                    uISystem.GetChild(5).gameObject.SetActive(true);
                    Panelvalue = 5;
                    break;
                case 5:
                    uISystem.GetChild(5).gameObject.SetActive(false);
                    uISystem.GetChild(0).gameObject.SetActive(true);
                    Panelvalue = 0;
                    break;
            }
        }
    }
}

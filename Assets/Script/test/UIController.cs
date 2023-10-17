using UnityEngine;

namespace MyFarm
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIController : MonoBehaviour
    {
        //獲取ui的父元素
        private Transform UITest;
        //面板屬性
        private int PanelValue = 0;

        void Start ()
        {
            UITest = GetComponent<Transform>();
        }

        private void Update()
        {
            ChangeUIPanel();
        }

        //切換ui面板
        void ChangeUIPanel()
        {
            //往左切換Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                switch (PanelValue)
                {
                    case 0:
                        UITest.GetChild(0).gameObject.SetActive(false);
                        UITest.GetChild(4).gameObject.SetActive(true);
                        PanelValue = 4;
                        break;
                    case 1:
                        UITest.GetChild(1).gameObject.SetActive(false);
                        UITest.GetChild(0).gameObject.SetActive(true);
                        PanelValue = 0;
                        break;
                    case 2:
                        UITest.GetChild(2).gameObject.SetActive(false);
                        UITest.GetChild(1).gameObject.SetActive(true);
                        PanelValue = 1;
                        break;
                    case 3:
                        UITest.GetChild(3).gameObject.SetActive(false);
                        UITest.GetChild(2).gameObject.SetActive(true);
                        PanelValue = 2;
                        break;
                    case 4:
                        UITest.GetChild(4).gameObject.SetActive(false);
                        UITest.GetChild(3).gameObject.SetActive(true);
                        PanelValue = 3;
                        break;
                }
            }
            //往右切換E
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (PanelValue)
                {
                    case 0:
                        UITest.GetChild(0).gameObject.SetActive(false);
                        UITest.GetChild(1).gameObject.SetActive(true);
                        PanelValue = 1;
                        break;
                    case 1:
                        UITest.GetChild(1).gameObject.SetActive(false);
                        UITest.GetChild(2).gameObject.SetActive(true);
                        PanelValue = 2;
                        break;
                    case 2:
                        UITest.GetChild(2).gameObject.SetActive(false);
                        UITest.GetChild(3).gameObject.SetActive(true);
                        PanelValue = 3;
                        break;
                    case 3:
                        UITest.GetChild(3).gameObject.SetActive(false);
                        UITest.GetChild(4).gameObject.SetActive(true);
                        PanelValue = 4;
                        break;
                    case 4:
                        UITest.GetChild(4).gameObject.SetActive(false);
                        UITest.GetChild(0).gameObject.SetActive(true);
                        PanelValue = 0;
                        break;
                }
            }
        }
    }
}


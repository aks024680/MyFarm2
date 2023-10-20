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
    // 控制游戏帮助显示和隐藏
    public void gameHelpPanelControll() {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf==true) {
            // 关闭游戏帮助
            gameObject.SetActive(false);
            // 打开系统ui
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

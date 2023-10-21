using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuTest : MonoBehaviour
{
    /// <summary>
    /// 主菜单功能
    /// 
    /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 开始游戏
    public void StartGame() {
        // LoadScene 加载的是场景下标
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 继续游戏（开启存档读取界面）
    public void ContinueGame() {
        // 主菜单
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
        // 存档界面
        GameObject saveDataUI = GameObject.FindGameObjectWithTag("SaveDataUI").transform.gameObject;
        //
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
        //
        saveDataUI.transform.GetChild(0).gameObject.SetActive(true);
    }
    // 游戏设置 （开启游戏设置界面）
    public void OpenGameSettingUI() {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").gameObject;
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
        gameSettingUI.transform.GetChild(0).gameObject.SetActive(true);
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
    }

    // 退出游戏
    public void ExitGame() {
        // 
        Application.Quit();
    }

}

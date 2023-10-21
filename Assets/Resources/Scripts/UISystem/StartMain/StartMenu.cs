using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // 编写主界面的方法
    // 开始游戏方法（重新开始游戏）
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // 继续游戏（点击保存游戏后继续游戏）
    public void LoadGame() {
        GameObject saveDataMenu = GameObject.FindGameObjectWithTag("SaveDataUI").gameObject;
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
        saveDataMenu.transform.GetChild(0).gameObject.SetActive(true);
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
    }
    // 游戏设置
    public void GameSettingUI() {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").gameObject;
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
        gameSettingUI.transform.GetChild(0).gameObject.SetActive(true);
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
    }

    // 退出游戏
    public void ExitGame() {
        Application.Quit();
    }

}

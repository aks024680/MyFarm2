using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // ��д������ķ���
    // ��ʼ��Ϸ���������¿�ʼ��Ϸ��
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // ������Ϸ�����������Ϸ�������Ϸ��
    public void LoadGame() {
        GameObject saveDataMenu = GameObject.FindGameObjectWithTag("SaveDataUI").gameObject;
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
        saveDataMenu.transform.GetChild(0).gameObject.SetActive(true);
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
    }
    // ��Ϸ����
    public void GameSettingUI() {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").gameObject;
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
        gameSettingUI.transform.GetChild(0).gameObject.SetActive(true);
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
    }

    // �˳���Ϸ
    public void ExitGame() {
        Application.Quit();
    }

}

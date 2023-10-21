using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuTest : MonoBehaviour
{
    /// <summary>
    /// ���˵�����
    /// 
    /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��ʼ��Ϸ
    public void StartGame() {
        // LoadScene ���ص��ǳ����±�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // ������Ϸ�������浵��ȡ���棩
    public void ContinueGame() {
        // ���˵�
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
        // �浵����
        GameObject saveDataUI = GameObject.FindGameObjectWithTag("SaveDataUI").transform.gameObject;
        //
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
        //
        saveDataUI.transform.GetChild(0).gameObject.SetActive(true);
    }
    // ��Ϸ���� ��������Ϸ���ý��棩
    public void OpenGameSettingUI() {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").gameObject;
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").gameObject;
        gameSettingUI.transform.GetChild(0).gameObject.SetActive(true);
        mainMenu.transform.GetChild(0).gameObject.SetActive(false);
    }

    // �˳���Ϸ
    public void ExitGame() {
        // 
        Application.Quit();
    }

}

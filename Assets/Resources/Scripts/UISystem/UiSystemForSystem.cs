using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UiSystemForSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeShowUI();
    }
    // 关闭存读档界面、打开系统界面
    public void ChangeShowUI() {
        // 当按下E并且存读档界面打开
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.activeSelf == true) {
            // 关闭存读档界面,
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // 打开系统界面
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        }
    }

    public void SaveDataButton()
    {
        // 点击执行代码
        // 变更状态
        UiSystemForData forData = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetComponent<UiSystemForData>();
        forData.dataType = 1;
        //关闭系统界面
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
        // 打开存读档界面
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(true);
    }
    // 读档按钮
    public void LoadDataButton()
    {
        // 变更状态
        UiSystemForData forData = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetComponent<UiSystemForData>();
        forData.dataType = 2;
        //关闭系统界面
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
        // 打开存读档界面
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(true);
    }
    // 游戏帮助
    public void GameHelp() {
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
        // 打开游戏帮助
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(3).gameObject.SetActive(true);
    }
    // 回主菜单
    public void BackMenuButton()
    {
        //
        SceneManager.LoadScene(0);
    }

    // 查看文件
    public void ShowFile() {
        string path = Application.dataPath + "/UpdateData/saveData/";
        string name = "Data2.txt";
        DirectoryInfo directory = new DirectoryInfo(path);
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        directory.Refresh();
        FileInfo[] files = directory.GetFiles("*",SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            gameObject.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = "未生成文件";
        }
        else {
            string text = "";
            text = PublicMethods.ReadFile(path + name);
/*            for (int i = 0;i<files.Length;i++) {
                if (files[i].Name.EndsWith(".meta")) {
                    continue;
                }
                
            }*/
            gameObject.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = text;
            print(text);
        }
    }
    // 创建文件
    public void CreateFile() {
        string path = Application.dataPath+ "/UpdateData/saveData";
        string fileName = "test.txt";
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        CreateOrOPenFile(path,fileName,"测试");
    }
    public void CreateOrOPenFile(string path, string name, string info)
    {          //路径、文件名、写入内容
        // 打开文件流
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //直接重新写入，如果要在原文件后面追加内容，应用fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
}

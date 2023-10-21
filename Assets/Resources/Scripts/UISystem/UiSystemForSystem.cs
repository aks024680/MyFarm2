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
    // �رմ�������桢��ϵͳ����
    public void ChangeShowUI() {
        // ������E���Ҵ���������
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.activeSelf == true) {
            // �رմ��������,
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(false);
            // ��ϵͳ����
            GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        }
    }

    public void SaveDataButton()
    {
        // ���ִ�д���
        // ���״̬
        UiSystemForData forData = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetComponent<UiSystemForData>();
        forData.dataType = 1;
        //�ر�ϵͳ����
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
        // �򿪴��������
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(true);
    }
    // ������ť
    public void LoadDataButton()
    {
        // ���״̬
        UiSystemForData forData = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).GetChild(0).GetComponent<UiSystemForData>();
        forData.dataType = 2;
        //�ر�ϵͳ����
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
        // �򿪴��������
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject.SetActive(true);
    }
    // ��Ϸ����
    public void GameHelp() {
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).gameObject.SetActive(false);
        // ����Ϸ����
        GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(3).gameObject.SetActive(true);
    }
    // �����˵�
    public void BackMenuButton()
    {
        //
        SceneManager.LoadScene(0);
    }

    // �鿴�ļ�
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
            gameObject.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = "δ�����ļ�";
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
    // �����ļ�
    public void CreateFile() {
        string path = Application.dataPath+ "/UpdateData/saveData";
        string fileName = "test.txt";
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        CreateOrOPenFile(path,fileName,"����");
    }
    public void CreateOrOPenFile(string path, string name, string info)
    {          //·�����ļ�����д������
        // ���ļ���
        StreamWriter sw;
        FileInfo fi = new FileInfo(path + "//" + name);
        sw = fi.CreateText();        //ֱ������д�룬���Ҫ��ԭ�ļ�����׷�����ݣ�Ӧ��fi.AppendText()
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }
}

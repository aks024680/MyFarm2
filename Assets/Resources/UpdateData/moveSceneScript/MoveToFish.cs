using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MoveToFish : MonoBehaviour
{
    // Start is called before the first frame update
    private TextAsset loadDataFile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // ��ͨ����ת������Ҫ���ص����ݽ��б���
             LoadDataToOtherScene();
            // ����Ϊ��Ҫͨ����ת�������и�ֵ
            PublicMethods.NeedLoadDataSave(true,"town",false,"");
        }
    }
    // 
    public void LoadDataToOtherScene()
    {
        // ɾ���ļ�
        //loadDataFile = Resources.Load<TextAsset>(PublicMethods.moveSceneDataResourcePath + "/" + PublicMethods.moveSceneResourceFileName);
        //deleteOneFile(path + "/" + name);
        //�����ļ�д������
        // ����д�������
        string loadJsonData = PublicMethods.ChangeSceneDataSave();
        // �����ļ�
        PublicMethods.createFile(PublicMethods.moveScenePath, PublicMethods.moveSceneFileName, loadJsonData);
        // ��ת����
        SceneManager.LoadScene(1);
    }
}

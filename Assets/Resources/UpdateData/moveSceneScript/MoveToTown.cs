using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MoveToTown : MonoBehaviour
{
    /// <summary>
    /// ͨ����ȡ�ļ������ݽ��п糡����ֵ
    /// </summary>
    // 
    private TextAsset loadDataFile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            // �������ݲ���ת����
            LoadDataToOtherScene();
            // ���Ƿ���ת�����ݸ��µ��ļ���
            PublicMethods.NeedLoadDataSave(true,"fishingMap",false,"");
        }
    }
    // 
    public void LoadDataToOtherScene() {
        // ���ж��Ƿ������Ҫ�������ݵ��ļ�
        //loadDataFile = Resources.Load<TextAsset>(PublicMethods.moveSceneDataResourcePath+"/"+PublicMethods.moveSceneResourceFileName);
        //deleteOneFile(path + "/" + name);
        // ����/�����ļ�д������
        // ����д�������
        string loadJsonData = PublicMethods.ChangeSceneDataSave();
        // �����ļ�
        PublicMethods.createFile(PublicMethods.moveScenePath,PublicMethods.moveSceneFileName,loadJsonData);
        // ��ת����
        SceneManager.LoadScene(2);
        
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MoveToTown : MonoBehaviour
{
    /// <summary>
    /// 通过读取文件的数据进行跨场景赋值
    /// </summary>
    // 
    private TextAsset loadDataFile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            // 更新数据并跳转场景
            LoadDataToOtherScene();
            // 将是否跳转的数据更新到文件夹
            PublicMethods.NeedLoadDataSave(true,"fishingMap",false,"");
        }
    }
    // 
    public void LoadDataToOtherScene() {
        // 先判断是否存在需要加载数据的文件
        //loadDataFile = Resources.Load<TextAsset>(PublicMethods.moveSceneDataResourcePath+"/"+PublicMethods.moveSceneResourceFileName);
        //deleteOneFile(path + "/" + name);
        // 创建/覆盖文件写入数据
        // 定义写入的数据
        string loadJsonData = PublicMethods.ChangeSceneDataSave();
        // 创建文件
        PublicMethods.createFile(PublicMethods.moveScenePath,PublicMethods.moveSceneFileName,loadJsonData);
        // 跳转场景
        SceneManager.LoadScene(2);
        
    }
}



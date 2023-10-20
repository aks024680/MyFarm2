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
            // 将通过跳转场景需要加载的数据进行保存
             LoadDataToOtherScene();
            // 设置为需要通过跳转场景进行赋值
            PublicMethods.NeedLoadDataSave(true,"town",false,"");
        }
    }
    // 
    public void LoadDataToOtherScene()
    {
        // 删除文件
        //loadDataFile = Resources.Load<TextAsset>(PublicMethods.moveSceneDataResourcePath + "/" + PublicMethods.moveSceneResourceFileName);
        //deleteOneFile(path + "/" + name);
        //创建文件写入数据
        // 定义写入的数据
        string loadJsonData = PublicMethods.ChangeSceneDataSave();
        // 创建文件
        PublicMethods.createFile(PublicMethods.moveScenePath, PublicMethods.moveSceneFileName, loadJsonData);
        // 跳转场景
        SceneManager.LoadScene(1);
    }
}

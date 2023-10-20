using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameVolumeControll : MonoBehaviour
{
    // 主菜单音量
    private AudioSource MenuAudio;
    // 主菜单音量滑动条
    private Slider volumeSlider;
    private void Start()
    {
        MenuAudio = GameObject.FindGameObjectWithTag("mainMenu").transform.GetComponent<AudioSource>();
        volumeSlider = GetComponent<Slider>();
       // StartCoroutine(LoadScene());
        //test();
    }

    // 申明协程
    IEnumerator LoadScene() {
        // 申明异步协程储存需要异步加载的场景
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        // 获取需要的物体
        Scene needScene = SceneManager.GetSceneByBuildIndex(1);
        GameObject[] all = needScene.GetRootGameObjects();
        for (int i = 0; i < all.Length; i++)
        {
            print("物体名称:" + all[i].name);
        }
        asyncLoadScene.allowSceneActivation = false;
        yield return asyncLoadScene;
    }


    // 
    private void Update()
    {

         volumeControll();
        // 控制关闭游戏设置界面
        closeGameSettingUI();
    }

    public void test() {
        // 获取当前场景的下标
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        print("当前激活的下标:"+currentIndex);
        string path=SceneUtility.GetScenePathByBuildIndex(1);
        print("场景:"+path);
        Scene aa = SceneManager.GetSceneByPath(path);
        print("cc:"+aa.name);
/*        GameObject[] all = needScene.GetRootGameObjects();
        for (int i = 0; i < all.Length; i++)
        {
            print("物体名称:" + all[i].name);
        }*/

    }

    // 控制游戏音量
    public void volumeControll() {
        // 让音乐的音量的大小等于滑动条大小
        MenuAudio.volume = volumeSlider.value;
        // 如果要同时控制多个，则获取到需要控制的声音
    }

    // 关闭游戏设置界面
    public void closeGameSettingUI() {
        // ESC
        if (Input.GetKey(KeyCode.Escape))
        {
            // 主菜单
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
            // 游戏设置界面
            GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").transform.gameObject;
            //
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);
            //
            gameSettingUI.transform.GetChild(0).gameObject.SetActive(false);

            //


        }
    }
}

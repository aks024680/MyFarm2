using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class menuDontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameObjectValue();
        GetGameObjectValue();

    }

    // 进行赋值设置的方法
    public void UpdateGameObjectValue() {
        // 获取激活的场景名称
        Scene activeScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("beforeSceneName",activeScene.name);
        // 获取滑动条的值
        if (activeScene.name == "menu") {
            float audioSliderValue = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Slider>().value;
            PlayerPrefs.SetFloat("audioSliderValue", audioSliderValue);
            //PlayerPrefs.Save();
        }

    }


    // 检测状态并赋值
    public void GetGameObjectValue() {
        // 获取激活的场景名称
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "fishingMap") {
            GameObject.Find("audioPlay").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("audioSliderValue");
            print("音量:"+ GameObject.Find("audioPlay").GetComponent<AudioSource>().volume);
        }
        

    }
}

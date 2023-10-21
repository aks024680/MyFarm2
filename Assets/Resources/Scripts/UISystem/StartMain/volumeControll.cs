using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeControll : MonoBehaviour
{
    // 需要控制的声音是什么
    private AudioSource menuAudio;
    // 获取到滑动条
    private Slider auidoSlider;
    void Start()
    {
        menuAudio = GameObject.FindGameObjectWithTag("mainMenu").transform.GetComponent<AudioSource>();
        auidoSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        VolumeControll();
        CloseGameSettingUI();
    }

    // 控制声音音效
    public void VolumeControll() {
        // 控制声音 
        menuAudio.volume = auidoSlider.value;
        // 同时控制多个声音
        //获取到需要控制的声音，把声音的音量和滑动条挂钩
    }

    // 关闭游戏设置界面
    public void CloseGameSettingUI() {
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
        }
    }

}

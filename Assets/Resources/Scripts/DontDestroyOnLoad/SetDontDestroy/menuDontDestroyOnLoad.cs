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

    // ���и�ֵ���õķ���
    public void UpdateGameObjectValue() {
        // ��ȡ����ĳ�������
        Scene activeScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("beforeSceneName",activeScene.name);
        // ��ȡ��������ֵ
        if (activeScene.name == "menu") {
            float audioSliderValue = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Slider>().value;
            PlayerPrefs.SetFloat("audioSliderValue", audioSliderValue);
            //PlayerPrefs.Save();
        }

    }


    // ���״̬����ֵ
    public void GetGameObjectValue() {
        // ��ȡ����ĳ�������
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "fishingMap") {
            GameObject.Find("audioPlay").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("audioSliderValue");
            print("����:"+ GameObject.Find("audioPlay").GetComponent<AudioSource>().volume);
        }
        

    }
}

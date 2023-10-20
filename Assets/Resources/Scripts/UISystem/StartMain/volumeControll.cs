using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeControll : MonoBehaviour
{
    // ��Ҫ���Ƶ�������ʲô
    private AudioSource menuAudio;
    // ��ȡ��������
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

    // ����������Ч
    public void VolumeControll() {
        // �������� 
        menuAudio.volume = auidoSlider.value;
        // ͬʱ���ƶ������
        //��ȡ����Ҫ���Ƶ��������������������ͻ������ҹ�
    }

    // �ر���Ϸ���ý���
    public void CloseGameSettingUI() {
        // ESC
        if (Input.GetKey(KeyCode.Escape))
        {
            // ���˵�
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
            // ��Ϸ���ý���
            GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").transform.gameObject;
            //
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);
            //
            gameSettingUI.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

}

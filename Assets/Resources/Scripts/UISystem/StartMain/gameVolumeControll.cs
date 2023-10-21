using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameVolumeControll : MonoBehaviour
{
    // ���˵�����
    private AudioSource MenuAudio;
    // ���˵�����������
    private Slider volumeSlider;
    private void Start()
    {
        MenuAudio = GameObject.FindGameObjectWithTag("mainMenu").transform.GetComponent<AudioSource>();
        volumeSlider = GetComponent<Slider>();
       // StartCoroutine(LoadScene());
        //test();
    }

    // ����Э��
    IEnumerator LoadScene() {
        // �����첽Э�̴�����Ҫ�첽���صĳ���
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        // ��ȡ��Ҫ������
        Scene needScene = SceneManager.GetSceneByBuildIndex(1);
        GameObject[] all = needScene.GetRootGameObjects();
        for (int i = 0; i < all.Length; i++)
        {
            print("��������:" + all[i].name);
        }
        asyncLoadScene.allowSceneActivation = false;
        yield return asyncLoadScene;
    }


    // 
    private void Update()
    {

         volumeControll();
        // ���ƹر���Ϸ���ý���
        closeGameSettingUI();
    }

    public void test() {
        // ��ȡ��ǰ�������±�
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        print("��ǰ������±�:"+currentIndex);
        string path=SceneUtility.GetScenePathByBuildIndex(1);
        print("����:"+path);
        Scene aa = SceneManager.GetSceneByPath(path);
        print("cc:"+aa.name);
/*        GameObject[] all = needScene.GetRootGameObjects();
        for (int i = 0; i < all.Length; i++)
        {
            print("��������:" + all[i].name);
        }*/

    }

    // ������Ϸ����
    public void volumeControll() {
        // �����ֵ������Ĵ�С���ڻ�������С
        MenuAudio.volume = volumeSlider.value;
        // ���Ҫͬʱ���ƶ�������ȡ����Ҫ���Ƶ�����
    }

    // �ر���Ϸ���ý���
    public void closeGameSettingUI() {
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

            //


        }
    }
}

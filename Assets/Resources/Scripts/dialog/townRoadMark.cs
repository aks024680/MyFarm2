using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class townRoadMark : MonoBehaviour
{
    // �ж��Ƿ���ײ��·��
    private bool isEnterRoadMark = false;
    // �����ı������ʾ������
    private bool showDialog = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnterRoadMark == true)
        {
            FishingRoadMark();
        }

        //controllDialogJs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnterRoadMark = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnterRoadMark = false;
    }
    // �����·��ָʾ
    public void FishingRoadMark()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        // ��ȡԤ����
        GameObject dialog = Resources.Load<GameObject>("Dialog/DialogPrefab");
        // �ж��Ƿ���ʵ�����ı���
        if (!dialogGameObject)
        {
            Instantiate(dialog);
        }
        // Ϊ��ֹ��ʼ��ʱ���ȡ����ʵ���������壬���¸�ֵ
        dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        // �ж��Ƿ���E��
        if (Input.GetKeyDown(KeyCode.E))
        {
            //showDialog = !showDialog;
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // ����ı�
            string[] textContent = { "ǰ��С��" };

            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            //dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "ǰ��С��";


        }
    }

    // �����ı�����Ľű������ʧЧ
    public void controllDialogJs()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (isEnterRoadMark == true)
            {
                // ��������·���ʱ�򣬱����Ѿ�����ʾ���صĹ���,����Ҫ�ı����Դ������ı���Ĺ���
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = false;

            }
            else
            {
                dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().enabled = true;

            }
        }

    }
    
}

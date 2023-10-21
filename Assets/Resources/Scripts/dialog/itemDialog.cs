using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemDialog : MonoBehaviour
{
    private bool isEnterTrigger = false;
    // �����ı������ʾ������
    private bool showDialog = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnterTrigger == true)
        {
            triggerDialog();
        }

        //controllDialogJs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnterTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnterTrigger = false;
    }

    public void triggerDialog()
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
            string[] textContent = { "�̵�\nӪҵʱ��:09:00-18:00" };

            dialogGameObject.transform.GetChild(0).GetComponent<DialogPrefab>().message = textContent;
            //dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "�̵�\nӪҵʱ��:09:00-18:00";
        }
    }

    // �����ı�����Ľű������ʧЧ
    public void controllDialogJs()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (isEnterTrigger == true)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveToTran : MonoBehaviour
{
    // �ж��Ƿ���ײ��·��
    private bool isEnterTran = false;
    // �����ı������ʾ������
    private bool showDialog = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnterTran == true)
        {
            
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEnterTran = true;
            FishingRoadMark();
            controllDialogJs();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnterTran = false;
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
            dialogGameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // ����ı�
            dialogGameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "���ڲ���Ҫȥ��վ";
        
    }

    // �����ı�����Ľű������ʧЧ
    public void controllDialogJs()
    {
        // ��ȡʵ�����������
        GameObject dialogGameObject = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialogGameObject)
        {
            if (isEnterTran == true)
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

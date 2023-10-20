using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animalEatingController : MonoBehaviour
{
    // Start is called before the first frame update
    // �ж��Ƿ���ιʳ��
    public bool isPlayerInEatingArea = false;
    // �������ιʳ��
    public bool isAnimalInEatingArea = false;
    // �ж϶Ի����Ƿ��
    public bool showDialog = true;
    // ��������
    public int count = 0;
    // ͣ��ʱ��
    public float totalTime = 0;
    // �����Ƿ���Գ�����
    public bool canEating = true;
    // �ָ��ɳ�����
    public float eatingTotalTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �Ի���
        ChangeData();
        // ��Ҳ����ķ���
        PlayerMethod();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ҽ����ʱ��
        if (collision.tag == "Player") {
            isPlayerInEatingArea = true;
        }

        if (collision.tag == "animal")
        {
            isAnimalInEatingArea = true;
            // �ж��Ƿ�������
            if (count !=0) {
                // �ж϶���ļ������Ƿ����0
                if (collision.gameObject.GetComponent<chickenController>().hunger ==0) {
                    // �����Ͼͻָ����Ｂ����
                    collision.gameObject.GetComponent<chickenController>().hunger = 100;
                    count --;
                }
                
            }
            
        }

        // ��ҽ�����ʾ���
        if (isPlayerInEatingArea == true) {
            gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = count.ToString() + "/100";
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            isPlayerInEatingArea = false;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }

        if (collision.tag == "animal") {
            isAnimalInEatingArea = false;
        }
        
    }

    // ����Ի������
    public void ChangeData() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == false)
        {
            showDialog = true;
        }
        else {
            showDialog = false;
        }
        // ������������
        gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = count.ToString() + "/100";
    }

    // ��д��Ҳ����ķ���
    public void PlayerMethod() {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInEatingArea == true && showDialog == true) {
            // �ж��Ƿ�װ������
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
            // �Ƿ���װ����Ʒ
            GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
            if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "����")
            {
                dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��δװ������!" };
                dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else {

                // �ж������Ƿ�100(��ֵ)
                if (count == 100)
                {
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��������!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    count = 100;
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "�Ѳ�������!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    // Ϊ�˷��㣬�����в���������Ʒ
                }
            }
        }
    }
}

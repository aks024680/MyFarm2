using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cowEating : MonoBehaviour
{
    // Start is called before the first frame update
    // ��ҽ���
    public bool playerEnter = false;
    // ��ţ����
    public bool cowEnter = false;
    // ʣ����������
    public int currentEatCount = 100;
    // 
    public int maxEatCount = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerEnterMethod();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            playerEnter = true;
        }
        if (collision.tag == "cow")
        {
            cowEnter = true;
            // �Ƿ�������
            if (currentEatCount!=0) {
                collision.gameObject.GetComponent<cowControler>().currentHunger = 100;
                currentEatCount--;
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerEnter = false;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        if (collision.tag == "cow")
        {
            cowEnter = false;
        }
    }

    // ��ҽ���
    public void playerEnterMethod() {
        gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = currentEatCount + "/100";
        if (playerEnter== true) {
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // �Ƿ�E
            if (Input.GetKeyDown(KeyCode.E)) {
                // �ж��Ƿ���װ������
                // �ı���
                GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                // �Ƿ���װ����Ʒ
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "����")
                {
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��δװ������!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    if (currentEatCount == maxEatCount)
                    {
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "��������,����Ҫ����!" };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    }
                    else {
                        currentEatCount = 100;
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "���������!" };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        // ���ٻ���ٱ���ʹ�õ�������Ʒ
                    }
                }
            }
        }
    }
}
